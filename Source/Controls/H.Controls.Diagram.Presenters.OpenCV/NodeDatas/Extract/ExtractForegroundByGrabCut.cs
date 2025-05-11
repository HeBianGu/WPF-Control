// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Extract;

[Obsolete("验证不通过")]
[Display(Name = "提取前景(GrabCut)", GroupName = "提取", Description = "适合复杂背景", Order = 0)]
public class ExtractForegroundByGrabCut : ExtractOpenCVNodeDataBase
{
    //private Scalar _lowerScalar = new Scalar(35, 50, 50);
    //[Display(Name = "HSV下限", GroupName = "数据")]
    //public Scalar LowerScalar
    //{
    //    get { return _lowerScalar; }
    //    set
    //    {
    //        _lowerScalar = value;
    //        RaisePropertyChanged();
    //    }
    //}
    //private Scalar _upperScalar = new Scalar(85, 255, 255);
    //[Display(Name = "HSV上限", GroupName = "数据")]
    //public Scalar UpperScalar
    //{
    //    get { return _upperScalar; }
    //    set
    //    {
    //        _upperScalar = value;
    //        RaisePropertyChanged();
    //    }
    //}

    protected override FlowableResult<Mat> Invoke(ISrcImageNodeData srcImageNodeData, IOpenCVNodeData from, IFlowableDiagramData diagram)
    {
        //using Mat src = Cv2.ImRead(srcImageNodeData.SrcFilePath, ImreadModes.Grayscale);

        //Mat img = Cv2.ImRead(inputPath);
        Mat img = from.Mat;
        if (img.Empty())
            throw new Exception("无法加载图像！");

        // 初始化掩膜和模型
        Mat mask = new Mat(img.Size(), MatType.CV_8UC1, Scalar.All(0));
        Mat bgdModel = new Mat();
        Mat fgdModel = new Mat();

        // 用户框选前景区域 (x, y, w, h)
        Rect rect = new Rect(50, 50, img.Width - 100, img.Height - 100);

        // 执行GrabCut
        Cv2.GrabCut(img, mask, rect, bgdModel, fgdModel, 5, GrabCutModes.InitWithRect);

        // 提取前景（mask=1或3的部分）
        Mat foregroundMask = new Mat();
        Cv2.Compare(mask, new Scalar(3), foregroundMask, CmpType.GE);
        Cv2.Compare(mask, new Scalar(1), foregroundMask, CmpType.GE);

        Mat foreground = new Mat();
        Cv2.BitwiseAnd(img, img, foreground, foregroundMask);

        //// 保存结果
        //Cv2.ImWrite(outputPath, foreground);
        return this.OK(foreground);
    }

}
