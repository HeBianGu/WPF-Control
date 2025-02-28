// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

global using H.Controls.Diagram.Flowables;

namespace H.Controls.Diagram.Datas;

public class DynamicLinkData : DefaultLinkData, IFlowable
{
    public bool UseInfoLogger { get; set; }

    public FlowableState State { get; set; }

    public void Clear()
    {

    }
    public virtual void Dispose()
    {

    }
}
