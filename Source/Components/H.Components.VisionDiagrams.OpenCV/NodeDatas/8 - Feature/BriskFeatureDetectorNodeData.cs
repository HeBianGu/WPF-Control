// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Components.VisionDiagrams.OpenCV.NodeDatas.Feature;
[Icon(FontIcons.GenericScan)]
[Display(Name = "BRISK特征提取", GroupName = "特征提取", Description = "检测 BRISK 关键点并生成具有旋转和尺度不变性的二进制描述子", Order = 0)]
public class BriskFeatureDetectorNodeData : FeatureOpenCVNodeDataBase
{
    private bool _useRectangle = true;
    [Tab(VisionTabNames.RunParameters)]
    [Display(Name = "绘制矩形", GroupName = VisionTabNames.DisplayParameters, Description = "指定是否绘制包围检测关键点的矩形")]
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
    [Tab(VisionTabNames.RunParameters)]
    [Display(Name = "检测阈值", GroupName = VisionTabNames.RunParameters, Description = "设置 BRISK 关键点检测的响应阈值")]
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
    [Tab(VisionTabNames.RunParameters)]
    [Display(Name = "尺度组数", GroupName = VisionTabNames.RunParameters, Description = "设置 BRISK 检测使用的尺度组数量")]
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
    [Tab(VisionTabNames.RunParameters)]
    [Display(Name = "采样模式缩放", GroupName = VisionTabNames.RunParameters, Description = "设置 BRISK 描述子采样模式的缩放比例")]
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
