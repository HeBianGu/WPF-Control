// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Cascade;

[Display(Name = "LBP", GroupName = "人脸检测", Description = "LBP (局部二值模式) 是 OpenCV 中另一种高效的物体检测方法，相比 HAAR 特征具有更快的速度和更好的光照不变性", Order = 0)]
public class LbpCascade : CascadeClassifierOpenCVNodeDataBase
{
    protected override FlowableResult<Mat> Invoke(ISrcImageNodeData srcImageNodeData, IOpenCVNodeData from, IFlowableDiagramData diagram)
    {
        Mat src = from.Mat;
        // Load the cascades
        using CascadeClassifier lbpCascade = new CascadeClassifier(TextPath.LbpCascade);

        // Detect faces
        Mat lbpResult = DetectFace(lbpCascade, src);
        return this.OK(lbpResult);
    }
}
