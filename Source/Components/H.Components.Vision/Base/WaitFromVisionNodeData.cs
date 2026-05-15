// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Components.Vision.Base;

public abstract class WaitFromVisionNodeData<T> : ScalerSelectableVisionNodeData<T>, IVisionNodeData<T> where T : class, IVisionImage
{

    private bool _useWaitFrom = false;
    [DefaultValue(false)]
    [Display(Name = "启用等待输入", GroupName = VisionPropertyGroupNames.FlowParameters, Description = "等待所有输入节点执行完再执行次节点")]
    public bool UseWaitFrom
    {
        get { return _useWaitFrom; }
        set
        {
            _useWaitFrom = value;
            RaisePropertyChanged();
        }
    }

    private List<IVisionNodeData<T>> _waitFromCache = new List<IVisionNodeData<T>>();
    protected override FlowableResult<T> Invoke(IStartVisionNodeData<T> srcImageNodeData, IVisionNodeData<T> from, IFlowableDiagramData diagram)
    {
        if (this.UseWaitFrom)
        {
            int count = this.FromNodeDatas.Count();
            this._waitFromCache.Add(from);
            if (count > 1 && this._waitFromCache.Count < count)
            {
                return this.Continue(from.ResultImage, "启用等待输入,等待所有输入节点执行完毕");
            }
            else
            {
                this._waitFromCache.Clear();
            }
        }
        return base.Invoke(srcImageNodeData, from, diagram);
    }
}

