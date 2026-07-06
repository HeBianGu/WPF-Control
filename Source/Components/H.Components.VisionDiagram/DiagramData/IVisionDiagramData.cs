// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Components.VisionDiagram.DiagramData;
/// <summary>
/// 表示一个包含OpenCV结果图像和消息的接口。
/// </summary>
public interface IVisionDiagramData : INodeDataGroupsDiagramData, IFlowableDiagramData, IResultImageSourceDiagramData, IDisposable
{
    /// <summary>
    /// 获取或设置消息的集合。
    /// </summary>
    ObservableCollection<IVisionMessage> Messages { get; set; }

    void Stop();

    Task InvokeOnce();

    Task<bool?> StartOnceAsync();
}
