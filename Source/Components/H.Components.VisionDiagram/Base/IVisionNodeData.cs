// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

global using H.Components.VisionDiagram.NodeDatas;
using H.Common.Interfaces;

namespace H.Components.VisionDiagram.Base;

public interface IVisionNodeData : IFlowableNodeData, IResultPresenterNodeData, IResultImageSourceNodeData, IHelpNodeData, IOrderable
{
    bool UseInvokedPart { get; set; }
    IVisionImage VisionImage { get; }
}

public interface IVideoCaptureNodeData : IVisionNodeData
{

}

public interface IVisionNodeData<T> : IVisionNodeData
{
    public T ResultImage { get; }
}
