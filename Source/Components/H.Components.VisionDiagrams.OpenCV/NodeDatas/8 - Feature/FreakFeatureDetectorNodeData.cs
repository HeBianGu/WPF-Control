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
[Display(Name = "FREAK特征提取", GroupName = "特征提取", Description = "检测关键点并计算 FREAK 二进制特征描述子", Order = 0)]
public class FreakFeatureDetectorNodeData : FeatureOpenCVNodeDataBase
{
    private int _nFeatures = 1000;
    [Tab(VisionTabNames.RunParameters)]
    [Display(Name = "最大特征数", GroupName = VisionTabNames.RunParameters, Description = "设置检测后保留的最大关键点数量")]
    public int nFeatures
    {
        get { return _nFeatures; }
        set
        {
            _nFeatures = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private float _scaleFactor = 1.2f;
    [Tab(VisionTabNames.RunParameters)]
    [Display(Name = "尺度因子", GroupName = VisionTabNames.RunParameters, Description = "设置相邻图像金字塔层之间的缩放比例")]
    public float ScaleFactor
    {
        get { return _scaleFactor; }
        set
        {
            _scaleFactor = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private int _nLevels = 8;
    [Tab(VisionTabNames.RunParameters)]
    [Display(Name = "金字塔层数", GroupName = VisionTabNames.RunParameters, Description = "设置关键点检测使用的图像金字塔层数")]
    public int nLevels
    {
        get { return _nLevels; }
        set
        {
            _nLevels = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }
    private int _edgeThreshold = 31;
    [Tab(VisionTabNames.RunParameters)]
    [Display(Name = "边缘阈值", GroupName = VisionTabNames.RunParameters, Description = "设置不进行关键点检测的图像边缘宽度")]
    public int EdgeThreshold
    {
        get { return _edgeThreshold; }
        set
        {
            _edgeThreshold = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private int _firstLevel = 0;
    [Tab(VisionTabNames.RunParameters)]
    [Display(Name = "起始层级", GroupName = VisionTabNames.RunParameters, Description = "设置原始图像在金字塔中的层级索引")]
    public int FirstLevel
    {
        get { return _firstLevel; }
        set
        {
            _firstLevel = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private int _wtaK = 2;
    [Tab(VisionTabNames.RunParameters)]
    [Display(Name = "像素比较数量", GroupName = VisionTabNames.RunParameters, Description = "设置生成描述子每个元素时参与比较的像素点数量")]
    public int WtaK
    {
        get { return _wtaK; }
        set
        {
            _wtaK = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private ORBScoreType _scoreType = ORBScoreType.Harris;
    [Tab(VisionTabNames.RunParameters)]
    [Display(Name = "评分方式", GroupName = VisionTabNames.RunParameters, Description = "选择关键点排序使用的响应评分方式")]
    public ORBScoreType ScoreType
    {
        get { return _scoreType; }
        set
        {
            _scoreType = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private int _patchSize = 31;
    [Tab(VisionTabNames.RunParameters)]
    [Display(Name = "特征块大小", GroupName = VisionTabNames.RunParameters, Description = "设置关键点方向描述使用的邻域图像块大小")]
    public int PatchSize
    {
        get { return _patchSize; }
        set
        {
            _patchSize = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private int _fastThreshold = 20;
    [Tab(VisionTabNames.RunParameters)]
    [Display(Name = "FAST阈值", GroupName = VisionTabNames.RunParameters, Description = "设置底层 FAST 关键点检测器的响应阈值")]
    public int FastThreshold
    {
        get { return _fastThreshold; }
        set
        {
            _fastThreshold = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private bool _orientationNormalized = true;
    [Tab(VisionTabNames.RunParameters)]
    [Display(Name = "方向归一化", GroupName = VisionTabNames.RunParameters, Description = "指定 FREAK 描述子是否进行方向归一化")]
    public bool OrientationNormalized
    {
        get { return _orientationNormalized; }
        set
        {
            _orientationNormalized = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private bool _scaleNormalized = true;
    [Tab(VisionTabNames.RunParameters)]
    [Display(Name = "尺度归一化", GroupName = VisionTabNames.RunParameters, Description = "指定 FREAK 描述子是否进行尺度归一化")]
    public bool ScaleNormalized
    {
        get { return _scaleNormalized; }
        set
        {
            _scaleNormalized = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private float _patternScale = 22.0f;
    [Tab(VisionTabNames.RunParameters)]
    [Display(Name = "采样模式缩放", GroupName = VisionTabNames.RunParameters, Description = "设置 FREAK 视网膜采样模式的缩放比例")]
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

    private int _nOctaves = 4;
    [Tab(VisionTabNames.RunParameters)]
    [Display(Name = "尺度组数", GroupName = VisionTabNames.RunParameters, Description = "设置 FREAK 描述子覆盖的尺度组数量")]
    public int nOctaves
    {
        get { return _nOctaves; }
        set
        {
            _nOctaves = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    protected override FlowableResult<IMatImage> Invoke(Mat fromImage)
    {
        using Mat gray = fromImage.CvtColor(ColorConversionCodes.BGR2GRAY);
        Mat dst = fromImage.Clone();
        // ORB
        using ORB orb = ORB.Create(this.nFeatures, this.ScaleFactor, this.nLevels, this.EdgeThreshold, this.FirstLevel, this.WtaK, this.ScoreType, this.PatchSize, this.FastThreshold);
        KeyPoint[] keypoints = orb.Detect(gray);

        // FREAK
        using FREAK freak = FREAK.Create(this.OrientationNormalized, this.ScaleNormalized, this.PatternScale, this.nOctaves);
        using Mat freakDescriptors = new Mat();
        freak.Compute(gray, ref keypoints, freakDescriptors);

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
            this.FeatureCountResult = keypoints.Length;
        }
        return this.OK(dst, keypoints.ToResultPresenter(), this.FeatureCountResult.ToDetectSuccessMessage());
    }
}
