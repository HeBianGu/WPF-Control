// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Components.VisionDiagram.NodeDatas;

/// <summary>
/// 图像从继承的节点数据基类，表示该节点的数据是从其他节点继承的图像数据，而不是自己生成或拥有图像资源。
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class InhertImageVisionNodeDataBase : ResultDisplayVisionNodeDataBase<IVisionImage>
{
    protected override FlowableResult<IVisionImage> Invoke(IStartVisionNodeData srcImageNodeData, IVisionNodeData from, IFlowableDiagramData diagram)
    {
        var result = this.InvokeInhert();
        return new FlowableResult<IVisionImage>(from.VisionImage, result.Message)
        {
            State = result.State
        };
    }

    protected abstract IFlowableResult InvokeInhert();

    protected override void ResultImageDisopse()
    {
        // 从继承图像的节点不需要释放图像资源，因为它们不拥有图像资源的所有权
        //base.DisopseResultImage();
    }
}

