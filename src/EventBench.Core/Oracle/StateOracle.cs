using EventBench.Core.Abstractions;
using EventBench.Core.Metrics;
using EventBench.Core.Model;
using EventBench.Core.Processing;
using EventBench.Core.Workload;

namespace EventBench.Core.Oracle;

public sealed class OracleCheck
{
    public required string Name { get; init; }
    public required bool Passed { get; init; }
    public required string Detail { get; init; }
}

public sealed class OracleReport
{
    public bool Passed => Checks.All(c => c.Passed);
    public List<OracleCheck> Checks { get; } = new();
    public List<string> MismatchSamples { get; } = new();
    public long SourcesVerified { get; set; }
    public long RecordsVerified { get; set; }
    public long UploadsVerified { get; set; }
    public long ExpectedEvents { get; set; }
    public double VerifySeconds { get; set; }
}

/// <summary>
/// Compares the expected end state (derived from the deterministic source scripts) with the actual
/// content of the state store, and folds in the violations observed by the workers at runtime.
/// </summary>
public static class StateOracle
{
    private const int MaxSamples = 50;

    /// <summary>Violation kinds that make a run incorrect.</summary>
    public static readonly string[] FatalViolationKinds =
        ["ordering_gap", "pre_state", "record_conflict", "payload_corrupt", "context_corrupt", "blob_missing"];

    private sealed class Actual
    {
        public long LastSeq = -1;
        public long Records, Completed, Open, Fold, Uploads, UploadFold;
    }

    public static async Task<OracleReport> VerifyAsync(WorkloadPlan plan, IStateStore store, MetricsHub metrics,
        long brokerBacklog, CancellationToken ct, Func<int, bool>? owns = null)
    {
        owns ??= _ => true;
        var sw = System.Diagnostics.Stopwatch.StartNew();
        var report = new OracleReport();
        var scripts = plan.Scripts;
        var actual = new Actual[scripts.Length];
        for (int i = 0; i < actual.Length; i++) actual[i] = new Actual();
        long unknownCursors = 0, unknownRecords = 0, unknownUploads = 0;

        await foreach (var c in store.ScanCursorsAsync(ct))
        {
            if ((uint)c.SourceId >= (uint)actual.Length) { unknownCursors++; continue; }
            actual[c.SourceId].LastSeq = c.LastSeq;
        }
        await foreach (var r in store.ScanRecordsAsync(ct))
        {
            report.RecordsVerified++;
            if ((uint)r.SourceId >= (uint)actual.Length) { unknownRecords++; continue; }
            var a = actual[r.SourceId];
            a.Records++;
            if (r.Status == RecordStatus.Completed)
            {
                a.Completed++;
                a.Fold += StateMachine.RecordFold(r.RecordId, r.Version, r.Hash);
            }
            else
            {
                a.Open++;
                Sample(report, $"source {r.SourceId}: record {r.RecordId} left in status {r.Status} (version {r.Version})");
            }
        }
        await foreach (var u in store.ScanUploadsAsync(ct))
        {
            report.UploadsVerified++;
            if ((uint)u.SourceId >= (uint)actual.Length) { unknownUploads++; continue; }
            actual[u.SourceId].Uploads++;
            actual[u.SourceId].UploadFold += StateMachine.UploadFold(u.Seq, u.Size, u.Checksum);
        }

        long cursorMismatch = 0, missingCursor = 0, countMismatch = 0, foldMismatch = 0, openRecords = 0, uploadMismatch = 0;
        long expectedEvents = 0, expectedRecords = 0;
        long ownedSources = 0;
        for (int sid = 0; sid < scripts.Length; sid++)
        {
            if (!owns(sid)) continue;   // another shard's source
            ownedSources++;
            var e = scripts[sid];
            var a = actual[sid];
            expectedEvents += e.LastSeq;
            expectedRecords += e.RecordsCreated;
            if (a.LastSeq < 0)
            {
                missingCursor++;
                Sample(report, $"source {sid}: cursor row missing");
                continue;
            }
            if (a.LastSeq != e.LastSeq)
            {
                cursorMismatch++;
                Sample(report, $"source {sid}: cursor at seq {a.LastSeq}, expected {e.LastSeq} ({e.LastSeq - a.LastSeq} events not applied)");
            }
            if (a.Records != e.RecordsCreated || a.Completed != e.RecordsCompleted)
            {
                countMismatch++;
                Sample(report, $"source {sid}: {a.Records} records ({a.Completed} completed), expected {e.RecordsCreated} ({e.RecordsCompleted} completed)");
            }
            else if (a.Fold != e.CompletedFold)
            {
                foldMismatch++;
                Sample(report, $"source {sid}: record content hash fold differs (an event was applied out of order, twice, or with wrong data)");
            }
            openRecords += a.Open;
            if (a.Uploads != e.Uploads || a.UploadFold != e.UploadFold)
            {
                uploadMismatch++;
                Sample(report, $"source {sid}: {a.Uploads} uploads, expected {e.Uploads} (or checksum mismatch)");
            }
        }
        report.SourcesVerified = ownedSources;
        report.ExpectedEvents = expectedEvents;

        Add(report, "cursors", missingCursor == 0 && cursorMismatch == 0 && unknownCursors == 0,
            $"{ownedSources - missingCursor - cursorMismatch}/{ownedSources} source cursors at the expected last sequence"
            + (missingCursor > 0 ? $", {missingCursor} missing" : "") + (unknownCursors > 0 ? $", {unknownCursors} unknown" : ""));
        Add(report, "records_complete", openRecords == 0,
            openRecords == 0 ? $"all {report.RecordsVerified:N0} records are Completed" : $"{openRecords:N0} records not Completed");
        Add(report, "record_counts", countMismatch == 0 && unknownRecords == 0,
            $"{report.RecordsVerified:N0} records found, {expectedRecords:N0} expected; {countMismatch} sources with count mismatch"
            + (unknownRecords > 0 ? $", {unknownRecords} records of unknown sources" : ""));
        Add(report, "record_content", foldMismatch == 0, $"{foldMismatch} sources with record content hash mismatch");
        Add(report, "uploads", uploadMismatch == 0 && unknownUploads == 0,
            $"{report.UploadsVerified} uploads verified, {uploadMismatch} sources mismatched");

        long applied = Interlocked.Read(ref metrics.Total.Applied);
        Add(report, "applied_equals_generated", applied == expectedEvents,
            $"{applied:N0} events applied by workers, {expectedEvents:N0} generated");
        Add(report, "broker_drained", brokerBacklog == 0, $"{brokerBacklog:N0} messages left in the broker");

        foreach (var kind in FatalViolationKinds)
        {
            long n = metrics.ViolationCounts.GetValueOrDefault(kind);
            Add(report, $"runtime_{kind}", n == 0, n == 0 ? "none observed" : $"{n:N0} observed by workers");
        }

        long blocked = metrics.ViolationCounts.GetValueOrDefault("cross_source_blocking");
        var bs = metrics.Blocked.Summarize();
        string bdetail = (blocked == 0 ? "none" : $"{blocked:N0} events")
            + $" waited > {metrics.BlockingThresholdUs / 1000.0:F0} ms while a worker slot of their group was idle"
            + $" ({metrics.Blocking.SlotsPerWorker} slot(s)/worker; blocked p99 {bs.P99Ms:F0} ms, max {metrics.MaxBlockedUs / 1000.0:F0} ms)"
            + (metrics.FailOnBlocking ? "" : " [not enforced]");
        Add(report, "runtime_cross_source_blocking", blocked == 0 || !metrics.FailOnBlocking, bdetail);

        report.VerifySeconds = sw.Elapsed.TotalSeconds;
        return report;
    }

    private static void Add(OracleReport r, string name, bool ok, string detail)
        => r.Checks.Add(new OracleCheck { Name = name, Passed = ok, Detail = detail });

    private static void Sample(OracleReport r, string s)
    {
        if (r.MismatchSamples.Count < MaxSamples) r.MismatchSamples.Add(s);
    }
}
