// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using H.Common.Interfaces;
using System;

namespace H.Controls.Diagram.Flowables;

public interface IFlowable : IDisposable, IMessageable, IStopwatchable
{
    bool UseInfoLogger { get; set; }
    void Clear();
}
