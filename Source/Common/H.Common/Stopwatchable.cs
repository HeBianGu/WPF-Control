// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

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
