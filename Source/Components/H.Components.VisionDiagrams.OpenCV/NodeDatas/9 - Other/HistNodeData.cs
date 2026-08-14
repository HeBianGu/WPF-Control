// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Components.VisionDiagrams.OpenCV.NodeDatas.Other;
[Display(Name = "直方图计算", GroupName = "其他", Description = "统计图像灰度分布并绘制直方图", Order = 0)]
public class HistNodeData : OpenCVNodeDataBase, IOtherGroupableNodeData
{
    protected override FlowableResult<IMatImage> Invoke(Mat fromImage)
    {
        using Mat src = fromImage.CvtColor(ColorConversionCodes.BGR2GRAY);

        // Histogram view
        const int Width = 260, Height = 200;
        Mat render = new Mat(new OpenCvSharp.Size(Width, Height), MatType.CV_8UC3, Scalar.All(255));

        // Calculate histogram
        Mat hist = new Mat();
        int[] hdims = { 256 }; // Histogram size for each dimension
        Rangef[] ranges = { new Rangef(0, 256), }; // min/max 
        Cv2.CalcHist(
            new Mat[] { src },
            new int[] { 0 },
            null,
            hist,
            1,
            hdims,
            ranges);

        // Get the max value of histogram
        Cv2.MinMaxLoc(hist, out _, out double maxVal);

        Scalar color = Scalar.All(100);
        // Scales and draws histogram
        hist = hist * (maxVal != 0 ? Height / maxVal : 0.0);
        for (int j = 0; j < hdims[0]; ++j)
        {
            int binW = (int)((double)Width / hdims[0]);
            render.Rectangle(
                new OpenCvSharp.Point(j * binW, render.Rows - (int)hist.Get<float>(j)),
                new OpenCvSharp.Point((j + 1) * binW, render.Rows),
                color,
                -1);
        }
        return this.OK(render);
    }
}
