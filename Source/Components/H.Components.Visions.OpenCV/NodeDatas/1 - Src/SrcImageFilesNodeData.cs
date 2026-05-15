// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

global using H.Components.Vision.NodeGroups.SrcImages;

namespace H.Components.Visions.OpenCV.NodeDatas.Src;

[Icon(FontIcons.Photo2)]
[Display(Name = "本地图像源", GroupName = "数据源", Order = 0)]
public class SrcImageFilesNodeData : OpenCVSrcFilesNodeDataBase, ISrcImageGroupableNodeData
{

}