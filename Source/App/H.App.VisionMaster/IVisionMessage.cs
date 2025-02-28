using System;

namespace H.App.VisionMaster;
public interface IVisionMessage
{
    TimeSpan TimeSpan { get; set; }
    int Index { get; set; }
    string Message { get; set; }
}