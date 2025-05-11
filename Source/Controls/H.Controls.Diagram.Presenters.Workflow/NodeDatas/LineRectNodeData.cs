// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.Diagram.Presenters.Workflow.NodeDatas;

[Display(Name = "子流程", GroupName = "基本流程图形状", Order = 2, Description = "子流程")]

public class LineRectNodeData : WorkflowNodeBase
{
    private double _lineMargin = 10;
    /// <summary> 说明  </summary>
    public double LineMargin
    {
        get { return _lineMargin; }
        set
        {
            _lineMargin = value;
            RaisePropertyChanged();
        }
    }

    protected override Geometry GetGeometry()
    {
        return GeometryFactory.CreateLineRect(this.Width, this.Height, this.LineMargin);
    }
}
