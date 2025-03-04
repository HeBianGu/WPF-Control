using System;

namespace H.Mvvm.ViewModels.Base
{
    public interface IStopwatchable
    {
        public TimeSpan TimeSpan { get; set; }
    }

}
