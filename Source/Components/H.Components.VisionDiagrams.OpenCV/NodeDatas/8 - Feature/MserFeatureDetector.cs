// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Components.VisionDiagrams.OpenCV.NodeDatas.Feature;
[Icon(FontIcons.GenericScan)]
[Display(Name = "MSER特征提取", GroupName = "特征提取", Order = 0)]
public class MserFeatureDetector : FeatureOpenCVNodeDataBase
{
    private int _delta = 5;
    [Tab(VisionTabNames.RunParameters)]
    [Display(Name = "Delta", GroupName = VisionTabNames.RunParameters)]
    public int Delta
    {
        get { return _delta; }
        set
        {
            _delta = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private int _minArea = 60;
    [Tab(VisionTabNames.RunParameters)]
    [Display(Name = "MinArea", GroupName = VisionTabNames.RunParameters)]
    public int MinArea
    {
        get { return _minArea; }
        set
        {
            _minArea = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private int _maxArea = 14400;
    [Tab(VisionTabNames.RunParameters)]
    [Display(Name = "MaxArea", GroupName = VisionTabNames.RunParameters)]
    public int MaxArea
    {
        get { return _maxArea; }
        set
        {
            _maxArea = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private double _maxVariation = 0.25;
    [Tab(VisionTabNames.RunParameters)]
    [Display(Name = "MaxVariation", GroupName = VisionTabNames.RunParameters)]
    public double MaxVariation
    {
        get { return _maxVariation; }
        set
        {
            _maxVariation = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private double _minDiversity = 0.2;
    [Tab(VisionTabNames.RunParameters)]
    [Display(Name = "MinDiversity", GroupName = VisionTabNames.RunParameters)]
    public double MinDiversity
    {
        get { return _minDiversity; }
        set
        {
            _minDiversity = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private int _maxEvolution = 200;
    [Tab(VisionTabNames.RunParameters)]
    [Display(Name = "MaxEvolution", GroupName = VisionTabNames.RunParameters)]
    public int MaxEvolution
    {
        get { return _maxEvolution; }
        set
        {
            _maxEvolution = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private double _areaThreshold = 1.01;
    [Tab(VisionTabNames.RunParameters)]
    [Display(Name = "AreaThreshold", GroupName = VisionTabNames.RunParameters)]
    public double AreaThreshold
    {
        get { return _areaThreshold; }
        set
        {
            _areaThreshold = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private double _minMargin = 0.003;
    [Tab(VisionTabNames.RunParameters)]
    [Display(Name = "MinMargin", GroupName = VisionTabNames.RunParameters)]
    public double MinMargin
    {
        get { return _minMargin; }
        set
        {
            _minMargin = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private int _edgeBlurSize = 5;
    [Tab(VisionTabNames.RunParameters)]
    [Display(Name = "EdgeBlurSize", GroupName = VisionTabNames.RunParameters)]
    public int EdgeBlurSize
    {
        get { return _edgeBlurSize; }
        set
        {
            _edgeBlurSize = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    protected override FlowableResult<IMatImage> Invoke(Mat fromImage)
    {
        using Mat gray = fromImage.Clone();
        //Mat dst = new Mat(srcImageNodeData.SrcFilePath, ImreadModes.Color);
        Mat dst = fromImage.Clone();
        MSER mser = MSER.Create(this.Delta, this.MinArea, this.MaxArea, this.MaxVariation, this.MinDiversity, this.MaxEvolution, this.AreaThreshold, this.MinMargin, this.EdgeBlurSize);
        mser.DetectRegions(gray, out OpenCvSharp.Point[][] contours, out _);
        foreach (OpenCvSharp.Point[] pts in contours)
        {
            Scalar color = Scalar.RandomColor();
            foreach (OpenCvSharp.Point p in pts)
            {
                dst.Circle(p, 1, color);
            }
        }
        this.FeatureCountResult = contours.Length;
        return this.OK(dst, contours.Select(x => x.ToWindowRect()).ToResultPresenter(x => "位置信息"), this.FeatureCountResult.ToDetectSuccessMessage());
    }
}
