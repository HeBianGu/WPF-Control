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
[Display(Name = "FREAK特征提取", GroupName = "特征提取", Order = 0)]
public class FreakFeatureDetector : FeatureOpenCVNodeDataBase
{
    private int _nFeatures = 1000;
    [Tab(VisionTabNames.RunParameters)]
    [Display(Name = "nFeatures", GroupName = VisionTabNames.RunParameters)]
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
    [Display(Name = "ScaleFactor", GroupName = VisionTabNames.RunParameters)]
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
    [Display(Name = "nLevels", GroupName = VisionTabNames.RunParameters)]
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
    [Display(Name = "EdgeThreshold", GroupName = VisionTabNames.RunParameters)]
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
    [Display(Name = "FirstLevel", GroupName = VisionTabNames.RunParameters)]
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
    [Display(Name = "WtaK", GroupName = VisionTabNames.RunParameters)]
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
    [Display(Name = "ScoreType", GroupName = VisionTabNames.RunParameters)]
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
    [Display(Name = "PatchSize", GroupName = VisionTabNames.RunParameters)]
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
    [Display(Name = "FastThreshold", GroupName = VisionTabNames.RunParameters)]
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
    [Display(Name = "OrientationNormalized", GroupName = VisionTabNames.RunParameters)]
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
    [Display(Name = "ScaleNormalized", GroupName = VisionTabNames.RunParameters)]
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
    [Display(Name = "PatternScale", GroupName = VisionTabNames.RunParameters)]
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
    [Display(Name = "nOctaves", GroupName = VisionTabNames.RunParameters)]
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
