// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Components.VisionDiagram.Base;

namespace H.Components.VisionDiagram.NodeDatas;
/// <summary>
/// 等待所有并行节点执行完再执行后续逻辑
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class WaitAllParallelNodeData<T> : WaitFromVisionNodeData<T> where T : class, IVisionImage
{
    private int _resultCount = 0;
    protected virtual void OnParallelFromNodeDataInvoke(IStartVisionNodeData srcImageNodeData, IVisionNodeData from, IFlowableDiagramData diagram)
    {

    }
    protected virtual FlowableResult<T> OnAllFrommParallelsInvoked(IStartVisionNodeData srcImageNodeData, IVisionNodeData from, IFlowableDiagramData diagram)
    {
        var viimage = this.GetVisionImage(from);
        return this.OK(viimage);
    }

    protected override FlowableResult<T> Invoke(IStartVisionNodeData srcImageNodeData, IVisionNodeData from, IFlowableDiagramData diagram)
    {
        this.OnParallelFromNodeDataInvoke(srcImageNodeData, from, diagram);
        this._resultCount++;
        //  Do ：等待所有多线程节点执行完再执行
        if (this._resultCount == this.FromNodeDatas.OfType<IFlowableNodeData>().Where(x => x.InvokeMode == FlowableInvokeMode.Parallel).Count())
        {
            //  Do ：前序节点都执行完了
            this._resultCount = 0;
            return this.OnAllFrommParallelsInvoked(srcImageNodeData, from, diagram);
        }
        else
        {
            var vimage = this.GetVisionImage(from);
            return this.Continue(vimage);
        }
    }
}

