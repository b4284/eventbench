using System.IO.Hashing;
using System.Text;
using EventBench.Postgres;
using Npgsql;
using NpgsqlTypes;

namespace EventBench.Dispatch;

/// <summary>
/// Active-active group ownership without a leader.
/// <list type="bullet">
/// <item>Every instance heartbeats into <c>ebd_member</c>. The live members are those seen within the lease TTL.</item>
/// <item>Each instance computes, from the same live list, the owner of every group by rendezvous hashing
/// (highest hash of member|group), so N instances split the groups about evenly and a membership change moves
/// only the groups of the member that came or went.</item>
/// <item>Ownership is a lease row (<c>ebd_lease</c>) with an expiry on the database clock. An instance claims a
/// group only if the row is free or expired, renews its rows every tick, and stops serving a group the moment
/// it cannot prove its lease (renewal failed for a whole TTL).</item>
/// <item>A group that should move (a member joined) is drained first: no new hand-outs, wait for the handed-out
/// messages to commit (up to DrainMs), then the row is deleted and the new owner claims it.</item>
/// <item>A crashed instance's leases simply expire: the others claim its groups within about one TTL.</item>
/// </list>
/// Correctness does not depend on the leases: a commit applies only if the source cursor is at seq-1, and a
/// message is deleted in the same transaction, so a brief double owner can only cause a duplicate delivery
/// (applied once), never a lost or reordered event.
/// </summary>
internal sealed class LeaseManager : IAsyncDisposable
{
    private readonly DispatcherOptions _o;
    private readonly PgQueueBroker _broker;
    private readonly NpgsqlDataSource _db;
    private readonly Action<string> _log;
    private readonly string _addr;
    private readonly HashSet<int> _owned = new();
    private readonly Dictionary<int, long> _draining = new();   // group -> drain start (ms)
    private readonly object _gate = new();
    private string[] _live = [];
    private int[] _desired = [];
    private long _lastOkMs;
    private long _claims, _releases, _lost, _tickErrors;

    public LeaseManager(DispatcherOptions o, PgQueueBroker broker, string connectionString, string addr, Action<string> log)
    {
        _o = o;
        _broker = broker;
        _db = NpgsqlDataSource.Create(connectionString);
        _addr = addr;
        _log = log;
    }

    public int OwnedCount { get { lock (_gate) return _owned.Count; } }
    public int DrainingCount { get { lock (_gate) return _draining.Count; } }
    public int LiveMembers => _live.Length;
    public (long Claims, long Releases, long Lost, long TickErrors) Counters => (_claims, _releases, _lost, _tickErrors);

    public async Task EnsureSchemaAsync(CancellationToken ct)
    {
        await using var conn = await _db.OpenConnectionAsync(ct);
        await using var cmd = new NpgsqlCommand("""
            CREATE TABLE IF NOT EXISTS ebd_member (
                member_id text PRIMARY KEY,
                addr      text NOT NULL,
                heartbeat timestamptz NOT NULL);
            CREATE TABLE IF NOT EXISTS ebd_lease (
                group_id   int PRIMARY KEY,
                owner      text NOT NULL,
                epoch      bigint NOT NULL DEFAULT 1,
                expires_at timestamptz NOT NULL);
            """, conn);
        try { await cmd.ExecuteNonQueryAsync(ct); }
        catch (PostgresException e) when (e.SqlState is "23505" or "42P07") { /* another instance created it concurrently */ }
    }

    public async Task RunAsync(CancellationToken ct)
    {
        _lastOkMs = Environment.TickCount64;
        using var timer = new PeriodicTimer(TimeSpan.FromMilliseconds(_o.TickMs));
        while (!ct.IsCancellationRequested)
        {
            try
            {
                await TickAsync(ct);
                _lastOkMs = Environment.TickCount64;
            }
            catch (OperationCanceledException) when (ct.IsCancellationRequested) { return; }
            catch (Exception e)
            {
                Interlocked.Increment(ref _tickErrors);
                _log($"lease tick failed: {e.GetType().Name}: {e.Message}");
                if (Environment.TickCount64 - _lastOkMs > _o.LeaseTtlMs) DropAllLocally("leases not renewed for a whole TTL");
            }
            try { await timer.WaitForNextTickAsync(ct); }
            catch (OperationCanceledException) { return; }
        }
    }

    private async Task TickAsync(CancellationToken ct)
    {
        await using var conn = await _db.OpenConnectionAsync(ct);
        string ttl = $"{_o.LeaseTtlMs} milliseconds";

        // 1. heartbeat, and the live member list
        await using (var cmd = new NpgsqlCommand(
            "INSERT INTO ebd_member (member_id, addr, heartbeat) VALUES ($1, $2, now()) " +
            "ON CONFLICT (member_id) DO UPDATE SET addr = excluded.addr, heartbeat = excluded.heartbeat", conn))
        {
            cmd.Parameters.Add(new NpgsqlParameter { Value = _o.Id });
            cmd.Parameters.Add(new NpgsqlParameter { Value = _addr });
            await cmd.ExecuteNonQueryAsync(ct);
        }
        var live = new List<string>();
        await using (var cmd = new NpgsqlCommand(
            $"SELECT member_id FROM ebd_member WHERE heartbeat > now() - interval '{ttl}' ORDER BY member_id", conn))
        await using (var rd = await cmd.ExecuteReaderAsync(ct))
            while (await rd.ReadAsync(ct)) live.Add(rd.GetString(0));
        if (!live.Contains(_o.Id)) live.Add(_o.Id);
        if (!live.SequenceEqual(_live))
        {
            _live = live.ToArray();
            _desired = Enumerable.Range(0, _broker.GroupCount).Where(g => OwnerOf(g, _live) == _o.Id).ToArray();
            _log($"members: {string.Join(", ", _live)} -> this instance should own {_desired.Length} of {_broker.GroupCount} groups");
        }
        var desired = new HashSet<int>(_desired);

        // 2. renew what this instance serves (rows left over from an earlier life of this id just expire);
        //    anything not renewed was taken over meanwhile
        var renewed = new HashSet<int>();
        int[] mine;
        lock (_gate) mine = _owned.ToArray();
        await using (var cmd = new NpgsqlCommand(
            $"UPDATE ebd_lease SET expires_at = now() + interval '{ttl}' WHERE owner = $1 AND group_id = ANY($2) RETURNING group_id", conn))
        {
            cmd.Parameters.Add(new NpgsqlParameter { Value = _o.Id });
            cmd.Parameters.Add(new NpgsqlParameter { Value = mine, NpgsqlDbType = NpgsqlDbType.Array | NpgsqlDbType.Integer });
            await using var rd = await cmd.ExecuteReaderAsync(ct);
            while (await rd.ReadAsync(ct)) renewed.Add(rd.GetInt32(0));
        }
        lock (_gate)
        {
            foreach (var g in _owned.Where(g => !renewed.Contains(g)).ToList())
            {
                _broker.StopServing(g);
                _broker.DropGroup(g);
                _owned.Remove(g);
                _draining.Remove(g);
                _lost++;
                _log($"lease for group {g} lost");
            }
        }

        // 3. groups that should move elsewhere: stop handing out, drain, then hand over
        var release = new List<int>();
        long now = Environment.TickCount64;
        lock (_gate)
        {
            foreach (var g in _owned)
            {
                if (desired.Contains(g))
                {
                    if (_draining.Remove(g)) _broker.ResumeServing(g);
                    continue;
                }
                if (!_draining.ContainsKey(g))
                {
                    _broker.StopServing(g);
                    _draining[g] = now;
                }
                if (_broker.InFlightCount(g) == 0 || now - _draining[g] >= _o.DrainMs) release.Add(g);
            }
        }
        if (release.Count > 0)
        {
            await using (var cmd = new NpgsqlCommand("DELETE FROM ebd_lease WHERE owner = $1 AND group_id = ANY($2)", conn))
            {
                cmd.Parameters.Add(new NpgsqlParameter { Value = _o.Id });
                cmd.Parameters.Add(new NpgsqlParameter { Value = release.ToArray(), NpgsqlDbType = NpgsqlDbType.Array | NpgsqlDbType.Integer });
                await cmd.ExecuteNonQueryAsync(ct);
            }
            lock (_gate)
                foreach (var g in release)
                {
                    _broker.DropGroup(g);
                    _owned.Remove(g);
                    _draining.Remove(g);
                    _releases++;
                }
            _log($"handed over {release.Count} groups");
        }

        // 4. claim the groups this instance should own that are free or expired
        int[] want;
        lock (_gate) want = _desired.Where(g => !_owned.Contains(g)).ToArray();
        if (want.Length > 0)
        {
            var got = new List<int>();
            await using (var cmd = new NpgsqlCommand(
                "INSERT INTO ebd_lease AS l (group_id, owner, epoch, expires_at) " +
                $"SELECT g, $1, 1, now() + interval '{ttl}' FROM unnest($2::int[]) AS g " +
                "ON CONFLICT (group_id) DO UPDATE SET owner = excluded.owner, epoch = l.epoch + 1, expires_at = excluded.expires_at " +
                "WHERE l.expires_at < now() OR l.owner = excluded.owner RETURNING group_id", conn))
            {
                cmd.Parameters.Add(new NpgsqlParameter { Value = _o.Id });
                cmd.Parameters.Add(new NpgsqlParameter { Value = want, NpgsqlDbType = NpgsqlDbType.Array | NpgsqlDbType.Integer });
                await using var rd = await cmd.ExecuteReaderAsync(ct);
                while (await rd.ReadAsync(ct)) got.Add(rd.GetInt32(0));
            }
            lock (_gate)
                foreach (var g in got)
                    if (_owned.Add(g))
                    {
                        _broker.StartServing(g);
                        _claims++;
                    }
            if (got.Count > 0) _log($"claimed {got.Count} groups (now {OwnedCount})");
        }
    }

    private void DropAllLocally(string why)
    {
        lock (_gate)
        {
            if (_owned.Count == 0) return;
            _log($"dropping {_owned.Count} groups locally: {why}");
            foreach (var g in _owned)
            {
                _broker.StopServing(g);
                _broker.DropGroup(g);
            }
            _owned.Clear();
            _draining.Clear();
        }
    }

    /// <summary>Rendezvous (highest random weight) hashing: the same live list gives every member the same answer.</summary>
    internal static string OwnerOf(int group, IReadOnlyList<string> members)
    {
        string best = members[0];
        ulong bestH = 0;
        foreach (var m in members)
        {
            ulong h = XxHash64.HashToUInt64(Encoding.UTF8.GetBytes($"{m}|{group}"));
            if (h > bestH || (h == bestH && string.CompareOrdinal(m, best) < 0)) { best = m; bestH = h; }
        }
        return best;
    }

    /// <summary>group → (member, addr) for every unexpired lease.</summary>
    public async Task<List<(int Group, string Member, string Addr)>> RoutesAsync(CancellationToken ct)
    {
        var r = new List<(int, string, string)>();
        await using var conn = await _db.OpenConnectionAsync(ct);
        await using var cmd = new NpgsqlCommand(
            "SELECT l.group_id, l.owner, m.addr FROM ebd_lease l JOIN ebd_member m ON m.member_id = l.owner WHERE l.expires_at > now()", conn);
        await using var rd = await cmd.ExecuteReaderAsync(ct);
        while (await rd.ReadAsync(ct)) r.Add((rd.GetInt32(0), rd.GetString(1), rd.GetString(2)));
        return r;
    }

    /// <summary>Graceful leave: give every group back at once and drop out of the member list.</summary>
    public async Task LeaveAsync()
    {
        DropAllLocally("shutting down");
        try
        {
            await using var conn = await _db.OpenConnectionAsync();
            await using var cmd = new NpgsqlCommand("DELETE FROM ebd_lease WHERE owner = $1; DELETE FROM ebd_member WHERE member_id = $1", conn);
            cmd.Parameters.Add(new NpgsqlParameter { Value = _o.Id });
            await cmd.ExecuteNonQueryAsync();
            _log("left the cluster (leases released)");
        }
        catch (Exception e) { _log($"leave failed: {e.Message}"); }
    }

    public ValueTask DisposeAsync() => _db.DisposeAsync();
}
