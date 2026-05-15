// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using OpenCvSharp.XFeatures2D;

namespace H.VisionMaster.OpenCV.NodeDatas.Feature;
[Icon(FontIcons.GenericScan)]
[Display(Name = "STAR特征提取", GroupName = "特征提取", Order = 0)]
public class StarFeatureDetector : FeatureOpenCVNodeDataBase
{
    private int _maxSize = 45;
    [Display(Name = "MaxSize", GroupName = VisionPropertyGroupNames.RunParameters)]
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
    [Display(Name = "ResponseThreshold", GroupName = VisionPropertyGroupNames.RunParameters)]
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
    [Display(Name = "LineThresholdProjected", GroupName = VisionPropertyGroupNames.RunParameters)]
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
    [Display(Name = "LineThresholdBinarized", GroupName = VisionPropertyGroupNames.RunParameters)]
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
    [Display(Name = "SuppressNonmaxSize", GroupName = VisionPropertyGroupNames.RunParameters)]
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
