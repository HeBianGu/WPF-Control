using System.Diagnostics;

namespace H.Common;

public class Stopwatchable : IDisposable
{
    private readonly Stopwatch _stopwatch;
    private readonly IStopwatchable _timespan;
    public Stopwatchable(IStopwatchable timespan)
    {
        _timespan = timespan;
        _stopwatch = Stopwatch.StartNew();
    }
    public void Dispose()
    {
        _stopwatch.Stop();
        _timespan.TimeSpan = _stopwatch.Elapsed;
    }
}
