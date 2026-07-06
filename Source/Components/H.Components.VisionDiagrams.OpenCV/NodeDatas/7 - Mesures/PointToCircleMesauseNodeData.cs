// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Components.VisionDiagram.NodeDatas.Measures;

namespace H.Components.VisionDiagrams.OpenCV.NodeDatas.Detector;
[Icon(FontIcons.Location)]
[Display(Name = "点圆测量", GroupName = "卡尺测量", Order = 1, Description = "用于检测图像中圆形的函数")]
public class PointToCircleMesauseNodeData : PointToCircleMeasureNodeDataBase<IMatImage>, IMesureGroupableNodeData, IOpenCVNodeData
{

}
