// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Extract;

[Obsolete("验证不通过")]
[Display(Name = "背景减除", GroupName = "提取", Description = "适合视频或动态场景", Order = 0)]
public class ExtractForegroundByBackgroundSubtraction : ExtractOpenCVNodeDataBase
{
    protected override FlowableResult<Mat> Invoke(ISrcImageNodeData srcImageNodeData, IOpenCVNodeData from, IFlowableDiagramData diagram)
    {
        //Mat img = Cv2.ImRead(inputPath);
        Mat img = from.Mat;

        if (img.Empty())
            throw new Exception("无法加载图像！");

        // 创建背景减除器
        var backSub = BackgroundSubtractorMOG2.Create();

        // 应用背景减除
        Mat fgMask = new Mat();
        backSub.Apply(img, fgMask);

        // 提取前景
        Mat foreground = new Mat();
        Cv2.BitwiseAnd(img, img, foreground, fgMask);

        // 保存结果
        //Cv2.ImWrite(outputPath, foreground);
        //return foreground;
        return this.OK(foreground);
    }

}
