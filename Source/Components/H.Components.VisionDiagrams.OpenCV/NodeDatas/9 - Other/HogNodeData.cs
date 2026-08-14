// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Components.VisionDiagrams.OpenCV.NodeDatas.Other;
[Display(Name = "行人检测", GroupName = "基础检测", Description = "使用 HOG 特征和默认 SVM 分类器检测图像中的行人", Order = 21)]
public class HogNodeData : OpenCVNodeDataBase, IOtherGroupableNodeData
{
    protected override FlowableResult<IMatImage> Invoke(Mat fromImage)
    {
        Mat result = fromImage.Clone();
        //using Mat img = Cv2.ImRead(this.SrcFilePath, ImreadModes.Color);
        using HOGDescriptor hog = new HOGDescriptor();
        hog.SetSVMDetector(HOGDescriptor.GetDefaultPeopleDetector());
        bool b = hog.CheckDetectorSize();
        // run the detector with default parameters. to get a higher hit-rate
        // (and more false alarms, respectively), decrease the hitThreshold and
        // groupThreshold (set groupThreshold to 0 to turn off the grouping completely).
        OpenCvSharp.Rect[] found = hog.DetectMultiScale(result, 0, new OpenCvSharp.Size(8, 8), new OpenCvSharp.Size(24, 16), 1.05, 2);
        //Mat prev = this.GetPrviewMat(srcImageNodeData, fromImage, result).Clone();
        foreach (OpenCvSharp.Rect rect in found)
        {
            // the HOG detector returns slightly larger rectangles than the real objects.
            // so we slightly shrink the rectangles to get a nicer output.
            OpenCvSharp.Rect r = new OpenCvSharp.Rect
            {
                X = rect.X + (int)Math.Round(rect.Width * 0.1),
                Y = rect.Y + (int)Math.Round(rect.Height * 0.1),
                Width = (int)Math.Round(rect.Width * 0.8),
                Height = (int)Math.Round(rect.Height * 0.8)
            };
            result.Rectangle(r.TopLeft, r.BottomRight, VisionSettings.Instance.OutputColor.ToScalar(), result.ToThickness());
        }
        return this.OK(result);
    }
}
