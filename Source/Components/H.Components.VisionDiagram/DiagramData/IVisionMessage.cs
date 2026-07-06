// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Components.VisionDiagram.DiagramData;
/// <summary>
/// 表示视觉消息的接口。
/// </summary>
public interface IVisionMessage
{
    /// <summary>
    /// 时间。
    /// </summary>
    DateTime DateTime { get; set; }
    /// <summary>
    /// 获取或设置时间跨度。
    /// </summary>
    TimeSpan TimeSpan { get; set; }

    /// <summary>
    /// 获取或设置索引。
    /// </summary>
    int Index { get; set; }

    /// <summary>
    /// 获取或设置消息内容。
    /// </summary>
    string Message { get; set; }

    /// <summary>
    /// 获取或设置源文件路径。
    /// </summary>
    string SrcFilePath { get; set; }

    /// <summary>
    /// 获取或设置消息类型。
    /// </summary>
    string Type { get; set; }

    FlowableState State { get; set; }

    /// <summary>
    /// 获取或设置结果图像源。
    /// </summary>
    ImageSource ResultImageSource { get; set; }

    IResultPresenterNodeData ResultNodeData { get; set; }
}
