using EventBench.Core.Abstractions;
using EventBench.Core.InMemory;
using Microsoft.Extensions.Configuration;

namespace EventBench.Core.Runner;

/// <summary>Maps the "Type" of the Broker/Store config sections to factories. Add new implementations here.</summary>
public sealed class ComponentRegistry
{
    private readonly Dictionary<string, IBrokerFactory> _brokers = new(StringComparer.OrdinalIgnoreCase);
    private readonly Dictionary<string, IStateStoreFactory> _stores = new(StringComparer.OrdinalIgnoreCase);

    public static ComponentRegistry WithBuiltIns()
        => new ComponentRegistry().Register(new InMemoryBrokerFactory()).Register(new InMemoryStoreFactory());

    public ComponentRegistry Register(IBrokerFactory f) { _brokers[f.Type] = f; return this; }
    public ComponentRegistry Register(IStateStoreFactory f) { _stores[f.Type] = f; return this; }

    public IEnumerable<string> BrokerTypes => _brokers.Keys;
    public IEnumerable<string> StoreTypes => _stores.Keys;

    public IBroker CreateBroker(IConfigurationSection section, BenchContext ctx)
    {
        var type = section["Type"] ?? "";
        return _brokers.TryGetValue(type, out var f)
            ? f.Create(section, ctx)
            : throw new InvalidOperationException($"Unknown broker type '{type}'. Known: {string.Join(", ", _brokers.Keys)}");
    }

    public IStateStore CreateStore(IConfigurationSection section, BenchContext ctx)
    {
        var type = section["Type"] ?? "";
        return _stores.TryGetValue(type, out var f)
            ? f.Create(section, ctx)
            : throw new InvalidOperationException($"Unknown store type '{type}'. Known: {string.Join(", ", _stores.Keys)}");
    }
}
