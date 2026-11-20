using System.Buffers;
using System.Text.Json;
using EventBench.Core.Abstractions;
using EventBench.Core.Model;
using EventBench.Core.Util;
using EventBench.Postgres;
using Google.Protobuf;
using Microsoft.Extensions.Configuration;
using Octopus.V1;

namespace EventBench.Octopus;

/// <summary>
/// State store for the Octopus broker. The bench's tables (eb_source, eb_record, eb_upload) are created and read by
/// a local <see cref="PostgresStore"/>; commits go to the dispatchers, which call eb_commit through the apply hook
/// (infra/octopus/hooks.sql) in the same transaction as Octopus's cursor check and queue delete.
/// </summary>
public sealed class OctopusStore(PostgresStore local) : IStateStore
{
    public string Name => "octopus-store";

    public Task SetupAsync(CancellationToken ct) => local.SetupAsync(ct);

    public ValueTask<WorkContext> LoadAsync(EventEnvelope ev, CancellationToken ct)
        => PreloadedContexts.TryTake(ev.SourceId, ev.Seq, out var ctx) ? ValueTask.FromResult(ctx) : local.LoadAsync(ev, ct);

    public bool SupportsTransactionalAck(IBroker broker) => broker is OctopusBroker;

    public async ValueTask<CommitStatus> CommitAsync(CommitRequest req, CancellationToken ct)
    {
        if (req.TransactionalAck is not BenchDelivery d)
            throw new InvalidOperationException("octopus store: commits must carry their delivery (AckMode=InTransaction)");
        var status = await d.Worker.CompleteAsync(d.Raw, ByteString.CopyFrom(Effect(req)), ct);
        return status switch
        {
            ResultStatus.ResultApplied => CommitStatus.Applied,
            ResultStatus.ResultDuplicate => CommitStatus.Duplicate,
            ResultStatus.ResultGap => CommitStatus.Gap,
            _ => throw new IOException($"octopus commit {req.Event}: {status}"),
        };
    }

    /// <summary>The worker's state change, as the apply hook reads it (JSON).</summary>
    internal static byte[] Effect(CommitRequest r)
    {
        var buf = new ArrayBufferWriter<byte>(256);
        using (var w = new Utf8JsonWriter(buf))
        {
            w.WriteStartObject();
            w.WriteNumber("k", (int)r.Kind);
            w.WriteNumber("r", r.Event.RecordId);
            w.WriteNumber("pv", r.PrevVersion);
            w.WriteNumber("v", r.NewVersion);
            w.WriteNumber("h", r.NewHash);
            w.WriteNumber("s", (int)r.NewStatus);
            if (r.NewContext != null) w.WriteBase64String("c", r.NewContext);
            if (r.Upload is { } u)
            {
                w.WriteNumber("us", u.Size);
                w.WriteNumber("uc", u.Checksum);
                if (u.Data != null) w.WriteBase64String("ud", u.Data);
            }
            w.WriteEndObject();
        }
        return buf.WrittenSpan.ToArray();
    }

    /// <summary>The context the load hook returns (JSON: l = last seq, v/h/s/c = record, absent if none).</summary>
    internal static WorkContext ParseContext(ReadOnlySpan<byte> json)
    {
        var r = new Utf8JsonReader(json);
        long last = 0; int? version = null; long hash = 0; short status = 0; byte[]? ctx = null;
        while (r.Read())
        {
            if (r.TokenType != JsonTokenType.PropertyName) continue;
            string name = r.GetString()!;
            r.Read();
            if (r.TokenType == JsonTokenType.Null) continue;
            switch (name)
            {
                case "l": last = r.GetInt64(); break;
                case "v": version = r.GetInt32(); break;
                case "h": hash = r.GetInt64(); break;
                case "s": status = r.GetInt16(); break;
                case "c": ctx = r.GetBytesFromBase64(); break;
            }
        }
        return version is null
            ? new WorkContext { LastSeq = last }
            : new WorkContext { LastSeq = last, RecordExists = true, Version = version.Value, Hash = hash, Status = (RecordStatus)status, Context = ctx };
    }

    public ValueTask PutBlobAsync(long blobId, int sourceId, ReadOnlyMemory<byte> data, CancellationToken ct) => local.PutBlobAsync(blobId, sourceId, data, ct);
    public ValueTask<byte[]?> GetBlobAsync(long blobId, CancellationToken ct) => local.GetBlobAsync(blobId, ct);
    public IAsyncEnumerable<SourceCursor> ScanCursorsAsync(CancellationToken ct) => local.ScanCursorsAsync(ct);
    public IAsyncEnumerable<RecordRow> ScanRecordsAsync(CancellationToken ct) => local.ScanRecordsAsync(ct);
    public IAsyncEnumerable<UploadRow> ScanUploadsAsync(CancellationToken ct) => local.ScanUploadsAsync(ct);
    public IStatsProvider? Stats => local.Stats;
    public string Describe() => $"bench tables via Postgres ({local.Describe()}); commits through Octopus's apply hook";
    public ValueTask DisposeAsync() => local.DisposeAsync();
}

public sealed class OctopusStoreFactory : IStateStoreFactory
{
    public string Type => "octopus";

    public IStateStore Create(IConfigurationSection section, BenchContext context)
    {
        var o = section.Get<PostgresStoreOptions>() ?? new();
        o.BatchCommits = false; // commits are batched by the dispatchers
        return new OctopusStore(new PostgresStore(o, context.Topology, context.Log));
    }
}
