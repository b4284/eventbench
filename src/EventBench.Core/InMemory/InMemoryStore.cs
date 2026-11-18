using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using EventBench.Core.Abstractions;
using EventBench.Core.Model;
using Microsoft.Extensions.Configuration;

namespace EventBench.Core.InMemory;

/// <summary>Reference state store (process memory). Commit semantics mirror the SQL implementation.</summary>
public sealed class InMemoryStore : IStateStore
{
    private sealed class SourceRow
    {
        public long LastSeq;
        public readonly List<UploadRow> Uploads = new();
    }

    private sealed class Record
    {
        public int SourceId;
        public RecordStatus Status;
        public int Version;
        public long Hash;
        public byte[]? Context;
    }

    private readonly Topology _topology;
    private SourceRow[] _sources = [];
    private readonly ConcurrentDictionary<long, Record> _records = new();
    private readonly ConcurrentDictionary<long, byte[]> _blobs = new();

    public string Name => "inmemory";

    public InMemoryStore(Topology topology) => _topology = topology;

    public Task SetupAsync(CancellationToken ct)
    {
        _sources = Enumerable.Range(0, _topology.TotalSources).Select(_ => new SourceRow()).ToArray();
        _records.Clear();
        _blobs.Clear();
        return Task.CompletedTask;
    }

    public ValueTask<WorkContext> LoadAsync(EventEnvelope ev, CancellationToken ct)
    {
        var src = _sources[ev.SourceId];
        long last;
        lock (src) last = src.LastSeq;
        if (_records.TryGetValue(ev.RecordId, out var r) && r.SourceId == ev.SourceId)
        {
            lock (r)
                return ValueTask.FromResult(new WorkContext
                {
                    LastSeq = last, RecordExists = true, Version = r.Version, Hash = r.Hash, Status = r.Status,
                    Context = r.Context,
                });
        }
        return ValueTask.FromResult(new WorkContext { LastSeq = last });
    }

    public ValueTask<CommitStatus> CommitAsync(CommitRequest req, CancellationToken ct)
    {
        var ev = req.Event;
        var src = _sources[ev.SourceId];
        lock (src)
        {
            if (src.LastSeq >= ev.Seq) return ValueTask.FromResult(CommitStatus.Duplicate);
            if (src.LastSeq != ev.Seq - 1) return ValueTask.FromResult(CommitStatus.Gap);

            switch (req.Kind)
            {
                case MutationKind.Insert:
                    var rec = new Record
                    {
                        SourceId = ev.SourceId, Status = req.NewStatus, Version = req.NewVersion, Hash = req.NewHash,
                        Context = req.NewContext,
                    };
                    if (!_records.TryAdd(ev.RecordId, rec)) return ValueTask.FromResult(CommitStatus.RecordConflict);
                    break;
                case MutationKind.Update:
                    if (!_records.TryGetValue(ev.RecordId, out var r)) return ValueTask.FromResult(CommitStatus.RecordConflict);
                    lock (r)
                    {
                        if (r.Version != req.PrevVersion) return ValueTask.FromResult(CommitStatus.RecordConflict);
                        r.Version = req.NewVersion;
                        r.Hash = req.NewHash;
                        r.Status = req.NewStatus;
                        if (req.NewContext != null) r.Context = req.NewContext;
                    }
                    break;
            }

            if (req.Upload is { } up) src.Uploads.Add(new UploadRow(ev.SourceId, ev.Seq, up.Size, up.Checksum));
            src.LastSeq = ev.Seq;
            return ValueTask.FromResult(CommitStatus.Applied);
        }
    }

    public ValueTask PutBlobAsync(long blobId, int sourceId, ReadOnlyMemory<byte> data, CancellationToken ct)
    {
        _blobs[blobId] = data.ToArray();
        return ValueTask.CompletedTask;
    }

    public ValueTask<byte[]?> GetBlobAsync(long blobId, CancellationToken ct)
        => ValueTask.FromResult(_blobs.TryGetValue(blobId, out var b) ? b : null);

    public async IAsyncEnumerable<SourceCursor> ScanCursorsAsync([EnumeratorCancellation] CancellationToken ct)
    {
        await Task.Yield();
        for (int i = 0; i < _sources.Length; i++)
            yield return new SourceCursor(i, Volatile.Read(ref _sources[i].LastSeq));
    }

    public async IAsyncEnumerable<RecordRow> ScanRecordsAsync([EnumeratorCancellation] CancellationToken ct)
    {
        await Task.Yield();
        foreach (var (id, r) in _records)
            yield return new RecordRow(r.SourceId, id, r.Status, r.Version, r.Hash);
    }

    public async IAsyncEnumerable<UploadRow> ScanUploadsAsync([EnumeratorCancellation] CancellationToken ct)
    {
        await Task.Yield();
        foreach (var s in _sources)
            foreach (var u in s.Uploads)
                yield return u;
    }

    public string Describe() => "inmemory";

    public ValueTask DisposeAsync() => ValueTask.CompletedTask;
}

public sealed class InMemoryStoreFactory : IStateStoreFactory
{
    public string Type => "inmemory";
    public IStateStore Create(IConfigurationSection section, BenchContext context) => new InMemoryStore(context.Topology);
}
