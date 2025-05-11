// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Basic;

[Display(Name = "图像掩膜", GroupName = "基础函数", Description = "用于图像掩膜（Mask）操作、区域提取和像素级逻辑运算", Order = 10)]
public class BitwiseAnd : BasicOpenCVNodeDataBase
{
    protected override FlowableResult<Mat> Invoke(ISrcImageNodeData srcImageNodeData, IOpenCVNodeData from, IFlowableDiagramData diagram)
    {
        // 将掩膜应用到原图中
        var img = srcImageNodeData.Mat;
        //var r = base.Invoke(srcImageNodeData, from, diagram);
        Mat mask = from.Mat;
        Mat foreground = new Mat();
        //扣除mask区域
        Cv2.BitwiseAnd(img, img, foreground, mask);
        //// 保存结果
        //Cv2.ImWrite(outputPath, foreground);
        //return foreground;
        return this.OK(foreground);
    }
}
