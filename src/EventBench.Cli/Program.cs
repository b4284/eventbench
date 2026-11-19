using System.Globalization;
using System.Text.Json.Nodes;
using EventBench.Core.Config;
using EventBench.Core.Model;
using EventBench.Core.Reporting;
using EventBench.Core.Runner;
using EventBench.Kafka;
using EventBench.Mongo;
using EventBench.Nats;
using EventBench.Postgres;
using EventBench.RabbitMq;
using EventBench.Redis;
using EventBench.SqlServer;
using Microsoft.Extensions.Configuration;

CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

void Log(string msg) => Console.WriteLine($"{DateTime.Now:HH:mm:ss.fff} {msg}");

if (args.Length == 0 || args[0] is "-h" or "--help" or "help")
{
    PrintHelp();
    return 0;
}

var command = args[0];
var rest = args.Skip(1).ToArray();

try
{
    switch (command)
    {
        case "run":
        {
            var (cfg, raw) = LoadConfig(rest);
            var registry = CreateRegistry();
            using var cts = new CancellationTokenSource();
            Console.CancelKeyPress += (_, e) =>
            {
                if (cts.IsCancellationRequested) return; // second Ctrl+C: hard exit
                e.Cancel = true;
                Log("Ctrl+C: stopping (press again to force exit)...");
                cts.Cancel();
            };
            var result = await new BenchmarkRunner(registry, Log).RunAsync(cfg, raw, cts.Token);
            var dir = ReportWriter.Write(result);
            Console.WriteLine();
            Console.WriteLine(File.ReadAllText(Path.Combine(dir, "summary.md")));
            Log($"results written to {Path.GetFullPath(dir)}");
            return result.Passed ? 0 : 1;
        }
        case "validate":
        {
            var (cfg, raw) = LoadConfig(rest);
            var topo = Topology.FromConfig(cfg);
            Console.WriteLine(ReportWriter.ConfigToJson(raw).ToJsonString(new() { WriteIndented = true }));
            Console.WriteLine();
            Console.WriteLine($"{topo.Groups.Count} groups, {topo.TotalSources:N0} sources, {topo.Groups.Sum(g => g.Workers):N0} workers");
            Console.WriteLine($"offered load: {topo.TotalRate:N1} events/s  (~{topo.TotalRate * cfg.Workload.DurationSeconds:N0} events over {cfg.Workload.DurationSeconds}s)");
            if (topo.Groups.Count <= 40)
                foreach (var g in topo.Groups)
                    Console.WriteLine($"  {g.Name,-16} sources {g.SourceCount,6}  rate/source {g.RatePerSource * 60,8:F1}/min  rate/group {g.RatePerGroup,9:F1}/s  " +
                                      $"workers {g.Workers,3}  payload {g.PayloadBytes ?? cfg.Workload.PayloadBytes,6} B{(g.LargeEventFraction > 0 ? $"  LARGE x{g.LargeEventFraction}" : "")}");
            PrintDistribution(cfg, topo);
            var reg = CreateRegistry();
            Console.WriteLine($"brokers: {string.Join(", ", reg.BrokerTypes)}; stores: {string.Join(", ", reg.StoreTypes)}");
            return 0;
        }
        case "compare":
            return Compare(rest);
        default:
            Console.Error.WriteLine($"unknown command '{command}'");
            PrintHelp();
            return 2;
    }
}
catch (Exception ex)
{
    Console.Error.WriteLine($"error: {ex.Message}");
    if (Environment.GetEnvironmentVariable("EB_DEBUG") == "1") Console.Error.WriteLine(ex);
    return 2;
}

static ComponentRegistry CreateRegistry() => ComponentRegistry.WithBuiltIns()
    .Register(new PgmqBrokerFactory())
    .Register(new KafkaBrokerFactory())
    .Register(new RabbitMqBrokerFactory())
    .Register(new NatsBrokerFactory())
    .Register(new PostgresStoreFactory())
    .Register(new MongoStoreFactory())
    .Register(new NatsKvStoreFactory())
    .Register(new SqlQueueBrokerFactory())
    .Register(new SqlServerStoreFactory())
    .Register(new RedisStreamsBrokerFactory())
    .Register(new RedisStoreFactory());

static (BenchConfig, IConfiguration) LoadConfig(string[] args)
{
    var files = new List<string>();
    var overrides = new List<string>();
    for (int i = 0; i < args.Length; i++)
    {
        if (args[i] is "-c" or "--config")
        {
            if (i + 1 >= args.Length) throw new ArgumentException("--config needs a path");
            files.Add(args[++i]);
        }
        else overrides.Add(args[i]);
    }
    if (files.Count == 0) throw new ArgumentException("at least one --config <file.json> is required");

    var builder = new ConfigurationBuilder();
    foreach (var f in files)
    {
        if (!File.Exists(f)) throw new FileNotFoundException($"config file not found: {f}");
        builder.AddJsonFile(Path.GetFullPath(f), optional: false);
    }
    builder.AddEnvironmentVariables("EB_");   // e.g. EB_Broker__ConnectionString=...
    builder.AddCommandLine(overrides.ToArray()); // e.g. --Workload:DurationSeconds=30
    var raw = builder.Build();
    var cfg = raw.Get<BenchConfig>(o => o.ErrorOnUnknownConfiguration = false) ?? new BenchConfig();
    return (cfg, raw);
}

static int Compare(string[] paths)
{
    var files = paths.SelectMany(p => Directory.Exists(p)
            ? Directory.GetFiles(p, "summary.json", SearchOption.AllDirectories)
            : [p])
        .Where(File.Exists).Order().ToList();
    if (files.Count == 0)
    {
        Console.Error.WriteLine("no summary.json files found");
        return 2;
    }
    Console.WriteLine($"{"run",-44} {"verdict",-7} {"offered/s",10} {"applied/s",10} {"e2e p50",9} {"e2e p99",9} {"e2e max",9} {"qwait p99",9} {"dup",7} {"err",7}");
    foreach (var f in files)
    {
        var j = JsonNode.Parse(File.ReadAllText(f))!;
        var e2e = j["latencyMs"]!.AsArray().FirstOrDefault(x => (string?)x!["metric"] == "e2e");
        var qw = j["latencyMs"]!.AsArray().FirstOrDefault(x => (string?)x!["metric"] == "queue_wait");
        var name = Path.GetFileName(Path.GetDirectoryName(Path.GetFullPath(f)))!;
        Console.WriteLine($"{name,-44} {(string?)j["verdict"],-7} {(double?)j["topology"]!["offeredRatePerSec"],10:F0} " +
                          $"{(double?)j["throughput"]!["steadyAppliedPerSec"],10:F0} {(double?)e2e?["p50"],9:F1} {(double?)e2e?["p99"],9:F1} " +
                          $"{(double?)e2e?["max"],9:F1} {(double?)qw?["p99"],9:F1} {(long?)j["throughput"]!["duplicates"],7} {(long?)j["throughput"]!["errors"],7}");
    }
    return 0;
}

static void PrintDistribution(BenchConfig cfg, Topology topo)
{
    static string Pct(IEnumerable<double> values, string fmt)
    {
        var v = values.Order().ToArray();
        double P(double p) => v[(int)Math.Min(v.Length - 1, Math.Floor(p * v.Length))];
        return $"min {v[0].ToString(fmt)}  p25 {P(0.25).ToString(fmt)}  median {P(0.5).ToString(fmt)}  p75 {P(0.75).ToString(fmt)}  p95 {P(0.95).ToString(fmt)}  max {v[^1].ToString(fmt)}";
    }
    var g = topo.Groups;
    Console.WriteLine();
    Console.WriteLine($"group sizes (sources):     {Pct(g.Select(x => (double)x.SourceCount), "F0")}");
    Console.WriteLine($"   single-digit groups: {g.Count(x => x.SourceCount < 10)}, groups with >= 100 sources: {g.Count(x => x.SourceCount >= 100)}");
    Console.WriteLine($"base rate (ev/min/source): {Pct(g.Select(x => x.RatePerSource * 60), "F1")}");
    var buckets = new[] { 10.0, 25, 50, 100, 150, 201 };
    Console.Write("   groups by base rate:");
    double lo = 0;
    foreach (var b in buckets)
    {
        Console.Write($"  [{lo:F0},{b:F0}): {g.Count(x => x.RatePerSource * 60 >= lo && x.RatePerSource * 60 < b)}");
        lo = b;
    }
    Console.WriteLine();
    Console.WriteLine($"group load (ev/s):         {Pct(g.Select(x => x.RatePerGroup), "F1")}");
    Console.WriteLine($"workers per group:         {Pct(g.Select(x => (double)x.Workers), "F0")}  (total {g.Sum(x => x.Workers):N0})");

    // Expected payload bandwidth. Uploads ride only on Create/Update events.
    var mix = cfg.Workload.OpMix;
    double cuShare = (mix.CreateWeight + mix.UpdateWeight) / Math.Max(1e-9, mix.CreateWeight + mix.GetWeight + mix.UpdateWeight + mix.CompleteWeight);
    double small = 0, large = 0;
    foreach (var x in g)
    {
        double largeRate = x.RatePerGroup * x.LargeEventFraction * cuShare;
        int lmin = x.LargeMinBytes ?? cfg.Workload.LargePayload.MinBytes, lmax = x.LargeMaxBytes ?? cfg.Workload.LargePayload.MaxBytes;
        large += largeRate * (lmin + lmax) / 2.0;
        small += (x.RatePerGroup - largeRate) * (x.PayloadBytes ?? cfg.Workload.PayloadBytes);
    }
    var largeGroups = g.Where(x => x.LargeEventFraction > 0).ToList();
    if (largeGroups.Count > 0)
        Console.WriteLine($"large-upload groups:       {largeGroups.Count} ({string.Join(", ", largeGroups.Select(x => $"{x.Name}: {x.SourceCount} src, {x.RatePerGroup:F1} ev/s"))})");
    Console.WriteLine($"payload bandwidth (approx): small {small / 1048576:F1} MB/s, large uploads {large / 1048576:F1} MB/s " +
                      $"(x~1.33 on the wire when Inline/base64; WAL roughly doubles it again)");
}

static void PrintHelp()
{
    Console.WriteLine("""
        eventbench — ordered event-processing benchmark with a correctness oracle

        usage:
          eventbench run      -c <config.json> [-c <override.json> ...] [--Section:Key=value ...]
          eventbench validate -c <config.json> [...]        print resolved config and derived load
          eventbench compare  <results-dir | summary.json> ...

        Later -c files override earlier ones; command-line --Key=value overrides files;
        environment variables prefixed EB_ (e.g. EB_Broker__ConnectionString) override files too.

        exit code: 0 = PASS, 1 = FAIL (oracle), 2 = error
        """);
}
