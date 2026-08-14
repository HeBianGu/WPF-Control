// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using OpenCvSharp.XFeatures2D;

namespace H.Components.VisionDiagrams.OpenCV.NodeDatas.Feature;
[Icon(FontIcons.GenericScan)]
[Display(Name = "STAR特征提取", GroupName = "特征提取", Description = "使用多尺度 STAR 算法检测图像角点特征", Order = 0)]
public class StarFeatureDetectorNodeData : FeatureOpenCVNodeDataBase
{
    private int _maxSize = 45;
    [Tab(VisionTabNames.RunParameters)]
    [Display(Name = "最大特征尺寸", GroupName = VisionTabNames.RunParameters, Description = "设置 STAR 检测器使用的最大尺度尺寸")]
    public int MaxSize
    {
        get { return _maxSize; }
        set
        {
            _maxSize = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private int _responseThreshold = 30;
    [Tab(VisionTabNames.RunParameters)]
    [Display(Name = "响应阈值", GroupName = VisionTabNames.RunParameters, Description = "设置接受 STAR 关键点响应的最低阈值")]
    public int ResponseThreshold
    {
        get { return _responseThreshold; }
        set
        {
            _responseThreshold = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private int _lineThresholdProjected = 10;
    [Tab(VisionTabNames.RunParameters)]
    [Display(Name = "投影线阈值", GroupName = VisionTabNames.RunParameters, Description = "设置抑制投影线状响应的阈值")]
    public int LineThresholdProjected
    {
        get { return _lineThresholdProjected; }
        set
        {
            _lineThresholdProjected = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private int _lineThresholdBinarized = 8;
    [Tab(VisionTabNames.RunParameters)]
    [Display(Name = "二值线阈值", GroupName = VisionTabNames.RunParameters, Description = "设置抑制二值线状响应的阈值")]
    public int LineThresholdBinarized
    {
        get { return _lineThresholdBinarized; }
        set
        {
            _lineThresholdBinarized = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private int _suppressNonmaxSize = 5;
    [Tab(VisionTabNames.RunParameters)]
    [Display(Name = "非极大值抑制尺寸", GroupName = VisionTabNames.RunParameters, Description = "设置关键点非极大值抑制使用的邻域尺寸")]
    public int SuppressNonmaxSize
    {
        get { return _suppressNonmaxSize; }
        set
        {
            _suppressNonmaxSize = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    protected override FlowableResult<IMatImage> Invoke(Mat fromImage)
    {
        Mat dst = fromImage.Clone();
        Mat gray = fromImage;

        StarDetector detector = StarDetector.Create(this.MaxSize, this.ResponseThreshold, this.LineThresholdProjected, this.LineThresholdBinarized, this.SuppressNonmaxSize);
        KeyPoint[] keypoints = detector.Detect(gray);

        if (keypoints != null)
        {
            Scalar color = new Scalar(0, 255, 0);
            foreach (KeyPoint kpt in keypoints)
            {
                float r = kpt.Size / 2;
                Cv2.Circle(dst, (OpenCvSharp.Point)kpt.Pt, (int)r, color);
                Cv2.Line(dst,
                    (OpenCvSharp.Point)new Point2f(kpt.Pt.X + r, kpt.Pt.Y + r),
                    (OpenCvSharp.Point)new Point2f(kpt.Pt.X - r, kpt.Pt.Y - r),
                    color);
                Cv2.Line(dst,
                    (OpenCvSharp.Point)new Point2f(kpt.Pt.X - r, kpt.Pt.Y + r),
                    (OpenCvSharp.Point)new Point2f(kpt.Pt.X + r, kpt.Pt.Y - r),
                    color);
            }
            this.FeatureCountResult = keypoints.Length;
        }
        return this.OK(dst, keypoints.ToResultPresenter(), this.FeatureCountResult.ToDetectSuccessMessage());

    }
}
