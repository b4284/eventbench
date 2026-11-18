namespace EventBench.RabbitMq;

/// <summary>
/// Publisher-confirm bookkeeping for one channel (AMQP confirm mode: basic.ack / basic.nack with an optional
/// "multiple" flag, plus basic.return for unroutable mandatory messages). Thread-safe: the confirms arrive on
/// the client's reader loop while the publisher awaits <see cref="WhenSettledAsync"/>.
/// </summary>
public sealed class ConfirmTracker
{
    private readonly object _gate = new();
    private readonly SortedSet<ulong> _outstanding = new();
    private TaskCompletionSource? _settled;
    private string? _failure;

    public int Outstanding { get { lock (_gate) return _outstanding.Count; } }

    /// <summary>Registers a publish sequence number before the message is sent.</summary>
    public void Add(ulong seq)
    {
        lock (_gate) _outstanding.Add(seq);
    }

    public void Ack(ulong tag, bool multiple) => Settle(tag, multiple, null);

    public void Nack(ulong tag, bool multiple) => Settle(tag, multiple, $"broker nacked publish #{tag}{(multiple ? " (multiple)" : "")}");

    /// <summary>Records a failure that is not tied to a sequence number (basic.return).</summary>
    public void Fail(string reason)
    {
        lock (_gate) _failure ??= reason;
    }

    /// <summary>The channel is gone: nothing outstanding will ever be confirmed.</summary>
    public void Abort(string reason)
    {
        lock (_gate)
        {
            if (_outstanding.Count > 0) _failure ??= reason;
            _outstanding.Clear();
            Signal();
        }
    }

    /// <summary>Completes once every registered sequence number has been acked or nacked.</summary>
    public Task WhenSettledAsync()
    {
        lock (_gate)
        {
            if (_outstanding.Count == 0) return Task.CompletedTask;
            _settled ??= new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
            return _settled.Task;
        }
    }

    /// <summary>Returns and clears the first failure recorded since the last call.</summary>
    public string? TakeFailure()
    {
        lock (_gate)
        {
            var f = _failure;
            _failure = null;
            return f;
        }
    }

    private void Settle(ulong tag, bool multiple, string? failure)
    {
        lock (_gate)
        {
            if (multiple)
            {
                while (_outstanding.Count > 0 && _outstanding.Min <= tag) _outstanding.Remove(_outstanding.Min);
            }
            else
            {
                _outstanding.Remove(tag);
            }
            if (failure != null) _failure ??= failure;
            Signal();
        }
    }

    private void Signal()
    {
        if (_outstanding.Count == 0 && _settled != null)
        {
            _settled.TrySetResult();
            _settled = null;
        }
    }
}
