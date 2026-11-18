using System.Text.Json;
using System.Text.Json.Serialization;

namespace EventBench.Core.Model;

public enum OpType : byte
{
    /// <summary>Create a new record (opens a lifecycle).</summary>
    Create = 1,
    /// <summary>Read the record context; no mutation.</summary>
    Get = 2,
    /// <summary>Read the record context, then update it.</summary>
    Update = 3,
    /// <summary>Close the record lifecycle (record ends in Completed state).</summary>
    Complete = 4,
}

[Flags]
public enum EventFlags : byte
{
    None = 0,
    /// <summary>Emitted during the closing phase to complete still-open records (excluded from latency stats).</summary>
    Closing = 1,
    /// <summary>Carries a multi-megabyte payload.</summary>
    Large = 2,
}

public enum RecordStatus : short { Open = 1, Completed = 2 }

/// <summary>
/// The event as produced by a source. It carries the expected pre-state of the record it targets
/// so that workers can detect ordering / lost-update anomalies at the moment they happen.
/// </summary>
public sealed class EventEnvelope
{
    [JsonPropertyName("g")] public int Group { get; set; }
    [JsonPropertyName("s")] public int SourceId { get; set; }
    /// <summary>Per-source sequence number, starting at 1, gapless.</summary>
    [JsonPropertyName("q")] public long Seq { get; set; }
    [JsonPropertyName("o")] public OpType Op { get; set; }
    [JsonPropertyName("r")] public long RecordId { get; set; }
    /// <summary>Record version the generator expects before this event is applied.</summary>
    [JsonPropertyName("ev")] public int ExpectedVersion { get; set; }
    /// <summary>Record hash the generator expects before this event is applied.</summary>
    [JsonPropertyName("eh")] public long ExpectedHash { get; set; }
    [JsonPropertyName("d")] public long Delta { get; set; }
    /// <summary>When the event was due according to the arrival process (coordinated-omission-free start time).</summary>
    [JsonPropertyName("ts")] public long ScheduledAtUs { get; set; }
    /// <summary>When the producer handed it to the broker.</summary>
    [JsonPropertyName("tp")] public long SentAtUs { get; set; }
    [JsonPropertyName("f")] public EventFlags Flags { get; set; }
    [JsonPropertyName("pn")] public int PayloadSize { get; set; }
    [JsonPropertyName("pc")] public long PayloadChecksum { get; set; }
    /// <summary>Inline payload bytes (base64 on the wire).</summary>
    [JsonPropertyName("p")] public byte[]? Payload { get; set; }
    /// <summary>Claim-check reference when the payload was stored out of band.</summary>
    [JsonPropertyName("b")] public long? BlobId { get; set; }

    [JsonIgnore] public bool IsLarge => (Flags & EventFlags.Large) != 0;
    [JsonIgnore] public bool IsClosing => (Flags & EventFlags.Closing) != 0;

    public string ToJson() => JsonSerializer.Serialize(this, EventJsonContext.Default.EventEnvelope);
    public static EventEnvelope FromJson(string json) => JsonSerializer.Deserialize(json, EventJsonContext.Default.EventEnvelope)!;
    public static EventEnvelope FromJson(ReadOnlySpan<byte> utf8) => JsonSerializer.Deserialize(utf8, EventJsonContext.Default.EventEnvelope)!;

    public override string ToString() => $"src={SourceId} seq={Seq} op={Op} rec={RecordId} ev={ExpectedVersion}";
}

[JsonSourceGenerationOptions(DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull)]
[JsonSerializable(typeof(EventEnvelope))]
public partial class EventJsonContext : JsonSerializerContext;
