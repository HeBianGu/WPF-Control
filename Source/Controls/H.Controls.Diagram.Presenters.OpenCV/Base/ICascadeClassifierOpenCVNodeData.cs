// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.Diagram.Presenters.OpenCV.Base;

public interface ICascadeClassifierOpenCVNodeData : INodeData, IDisplayBindable
{
    HaarDetectionTypes Flags { get; set; }
    System.Windows.Size MaxSize { get; set; }
    int MinNeighbors { get; set; }
    System.Windows.Size MinSize { get; set; }
    double ScaleFactor { get; set; }
}