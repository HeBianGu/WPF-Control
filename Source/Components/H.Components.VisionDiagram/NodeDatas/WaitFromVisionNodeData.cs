// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Components.VisionDiagram.NodeDatas;

public abstract class WaitFromVisionNodeData : DemoNodeDataBase
{
    private bool _useWaitFrom = false;
    [DefaultValue(false)]
    [Tab(VisionTabNames.FlowParameters)]
    [Display(Name = "启用等待输入", GroupName = VisionTabNames.FlowParameters, Description = "等待所有输入节点执行完再执行次节点")]
    public bool UseWaitFrom
    {
        get { return _useWaitFrom; }
        set
        {
            _useWaitFrom = value;
            RaisePropertyChanged();
        }
    }

    private List<IFlowableLinkData> _waitFromCache = new List<IFlowableLinkData>();
    public override IFlowableResult Invoke(IFlowableLinkData previors, IFlowableDiagramData diagram)
    {
        if (this.UseWaitFrom)
        {
            int count = this.FromNodeDatas.Count();
            this._waitFromCache.Add(previors);
            if (count > 1 && this._waitFromCache.Count < count)
                return this.Continue("启用等待输入,等待所有输入节点执行完毕");
            else
            {
                this._waitFromCache.Clear();
            }
        }
        return base.Invoke(previors, diagram);
    }
}

