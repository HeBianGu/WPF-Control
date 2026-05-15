// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.VisionMaster.OpenCV.NodeDatas.Feature;
[Icon(FontIcons.GenericScan)]
[Display(Name = "BRISK特征提取", GroupName = "特征提取", Order = 0)]
public class BriskFeatureDetector : FeatureOpenCVNodeDataBase
{
    private bool _useRectangle = true;
    [Display(Name = "绘制矩形", GroupName = VisionPropertyGroupNames.RunParameters)]
    public bool UseRectangle
    {
        get { return _useRectangle; }
        set
        {
            _useRectangle = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private int _threshold = 30;
    [DefaultValue(30)]
    [PropertyItem(typeof(Int32SliderTextPropertyItem))]
    [Range(0, 500)]
    [Display(Name = "Threshold", GroupName = VisionPropertyGroupNames.RunParameters)]
    public int Threshold
    {
        get { return _threshold; }
        set
        {
            _threshold = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private int _octaves = 3;
    [PropertyItem(typeof(Int32SliderTextPropertyItem))]
    [Range(0, 8)]
    [Display(Name = "Octaves", GroupName = VisionPropertyGroupNames.RunParameters)]
    public int Octaves
    {
        get { return _octaves; }
        set
        {
            _octaves = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private float _patternScale = 1.0f;
    [Display(Name = "PatternScale", GroupName = VisionPropertyGroupNames.RunParameters)]
    public float PatternScale
    {
        get { return _patternScale; }
        set
        {
            _patternScale = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    protected override FlowableResult<IMatImage> Invoke(Mat fromImage)
    {
        using Mat gray = fromImage.CvtColor(ColorConversionCodes.BGR2GRAY);
        Mat dst = fromImage.Clone();
        using BRISK brisk = BRISK.Create(Threshold, Octaves);
        KeyPoint[] keypoints = brisk.Detect(gray);
        if (keypoints != null)
        {
            foreach (KeyPoint kpt in keypoints)
            {
                Scalar color = Scalar.RandomColor();
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
        }
        this.FeatureCountResult = keypoints.Length;
        return this.OK(dst, keypoints.ToResultPresenter(), this.FeatureCountResult.ToDetectSuccessMessage());

    }
}
