using NATS.Client.Core;
using NATS.Client.JetStream;

namespace EventBench.Nats;

/// <summary>A few NATS connections shared round-robin, so one multi-MB message does not head-of-line block everyone.</summary>
internal sealed class NatsConnectionPool : IAsyncDisposable
{
    private readonly NatsConnection[] _conns;
    private readonly NatsJSContext[] _js;
    private int _next = -1;

    public NatsConnectionPool(string url, string name, int size, TimeSpan requestTimeout)
    {
        size = Math.Max(1, size);
        _conns = new NatsConnection[size];
        _js = new NatsJSContext[size];
        for (int i = 0; i < size; i++)
        {
            _conns[i] = new NatsConnection(NatsOpts.Default with
            {
                Url = url,
                Name = $"{name}-{i}",
                RequestTimeout = requestTimeout,
                CommandTimeout = requestTimeout,
                ConnectTimeout = TimeSpan.FromSeconds(10),
            });
            _js[i] = new NatsJSContext(_conns[i]);
        }
    }

    public NatsConnection First => _conns[0];
    public NatsJSContext FirstJs => _js[0];

    public async Task ConnectAsync()
    {
        foreach (var c in _conns) await c.ConnectAsync();
    }

    public int NextIndex() => (int)((uint)Interlocked.Increment(ref _next) % (uint)_conns.Length);
    public NatsJSContext Js(int index) => _js[index % _js.Length];
    public NatsJSContext NextJs() => _js[NextIndex()];

    public async ValueTask DisposeAsync()
    {
        foreach (var c in _conns)
        {
            try { await c.DisposeAsync(); } catch { /* best effort */ }
        }
    }
}
