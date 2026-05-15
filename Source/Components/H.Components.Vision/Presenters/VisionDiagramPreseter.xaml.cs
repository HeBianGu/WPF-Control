// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Controls.Diagram.Presenter.DiagramDatas;
using H.Extensions.Mvvm.ViewModels.Base;

namespace H.Components.Vision.Presenters;

[Display(Name = "机器视觉流程", GroupName = "机器视觉", Order = 0)]
public class VisionDiagramData : NodeDataGroupsDiagramDataBase
{
    protected override IEnumerable<INodeDataGroup> CreateNodeGroups()
    {
        return base.CreateNodeGroups();
    }
}


[Icon(FontIcons.Flow)]
[Display(Name = "运行机器视觉流程", GroupName = "机器视觉", Order = 0)]
public class VisionDiagramPreseter : DisplayBindableBase
{
    public VisionDiagramPreseter()
    {
        this.DiagramData = this.CreateNodeDataGroupsDiagramData();
    }
    private INodeDataGroupsDiagramData _DiagramData;
    public INodeDataGroupsDiagramData DiagramData
    {
        get { return _DiagramData; }
        set
        {
            _DiagramData = value;
            RaisePropertyChanged();
        }
    }


    protected virtual INodeDataGroupsDiagramData CreateNodeDataGroupsDiagramData()
    {
        return new VisionDiagramData();
    }

}
