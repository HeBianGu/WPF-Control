// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Components.Vision.Presenters;
using H.Components.Visions.OpenCV.Diagrams;

namespace H.Components.Visions.OpenCV.Presenters;

public class OpenCVVisionDiagramPreseter : VisionDiagramPreseter
{
    protected override INodeDataGroupsDiagramData CreateNodeDataGroupsDiagramData()
    {
        return new OpenCVVisionDiagramData();
    }
}
