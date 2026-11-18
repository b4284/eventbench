using HdrHistogram;

namespace EventBench.Core.Metrics;

/// <summary>Thread-safe latency recorder (microseconds) with interval + cumulative histograms.</summary>
public sealed class LatencyMetric
{
    public const long MaxTrackableUs = 3_600_000_000L; // 1h

    private readonly Recorder _recorder;
    private readonly LongHistogram _cumulative;
    private HistogramBase? _interval;
    private readonly object _rotateLock = new();

    public string Name { get; }
    public string Description { get; }

    public LatencyMetric(string name, string description, int significantDigits = 3)
    {
        Name = name;
        Description = description;
        _recorder = new Recorder(1, MaxTrackableUs, significantDigits, (id, low, high, digits) => new LongConcurrentHistogram(id, low, high, digits));
        _cumulative = new LongHistogram(1, MaxTrackableUs, significantDigits);
    }

    public void Record(long micros)
    {
        if (micros < 1) micros = 1;
        else if (micros > MaxTrackableUs) micros = MaxTrackableUs;
        _recorder.RecordValue(micros);
    }

    /// <summary>Swaps the interval histogram, folds it into the cumulative one and returns a snapshot of the interval.</summary>
    public IntervalSnapshot Rotate()
    {
        lock (_rotateLock)
        {
            // Multi-writer recorder (factory produces concurrent histograms); recycle the previous interval.
            _interval = _recorder.GetIntervalHistogram(_interval);
            _cumulative.Add(_interval);
            return IntervalSnapshot.From(_interval);
        }
    }

    public LatencySummary Summarize()
    {
        lock (_rotateLock)
        {
            var h = _cumulative;
            if (h.TotalCount == 0) return new LatencySummary(Name, Description, 0, 0, 0, 0, 0, 0, 0, 0);
            return new LatencySummary(Name, Description, h.TotalCount,
                h.GetMean() / 1000.0,
                h.GetValueAtPercentile(50) / 1000.0,
                h.GetValueAtPercentile(90) / 1000.0,
                h.GetValueAtPercentile(99) / 1000.0,
                h.GetValueAtPercentile(99.9) / 1000.0,
                h.GetValueAtPercentile(99.99) / 1000.0,
                h.GetValueAtPercentile(100) / 1000.0);
        }
    }

    public void WritePercentileDistribution(TextWriter writer)
    {
        lock (_rotateLock)
        {
            // Values in milliseconds.
            _cumulative.OutputPercentileDistribution(writer, outputValueUnitScalingRatio: 1000.0);
        }
    }
}

public readonly record struct IntervalSnapshot(long Count, double P50Ms, double P99Ms, double MaxMs)
{
    public static IntervalSnapshot From(HistogramBase h) => h.TotalCount == 0
        ? default
        : new IntervalSnapshot(h.TotalCount, h.GetValueAtPercentile(50) / 1000.0, h.GetValueAtPercentile(99) / 1000.0, h.GetValueAtPercentile(100) / 1000.0);
}

/// <summary>Latency percentiles in milliseconds.</summary>
public sealed record LatencySummary(
    string Name, string Description, long Count,
    double MeanMs, double P50Ms, double P90Ms, double P99Ms, double P999Ms, double P9999Ms, double MaxMs);
