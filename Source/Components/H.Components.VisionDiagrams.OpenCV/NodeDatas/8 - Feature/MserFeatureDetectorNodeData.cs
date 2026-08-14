// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Components.VisionDiagrams.OpenCV.NodeDatas.Feature;
[Icon(FontIcons.GenericScan)]
[Display(Name = "MSER特征提取", GroupName = "特征提取", Description = "检测在多个阈值下保持稳定的极值区域", Order = 0)]
public class MserFeatureDetectorNodeData : FeatureOpenCVNodeDataBase
{
    private int _delta = 5;
    [Tab(VisionTabNames.RunParameters)]
    [Display(Name = "灰度步长", GroupName = VisionTabNames.RunParameters, Description = "设置比较稳定区域时使用的灰度级步长")]
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
    [Display(Name = "最小区域面积", GroupName = VisionTabNames.RunParameters, Description = "过滤面积小于该值的稳定区域")]
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
    [Display(Name = "最大区域面积", GroupName = VisionTabNames.RunParameters, Description = "过滤面积大于该值的稳定区域")]
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
    [Display(Name = "最大变化率", GroupName = VisionTabNames.RunParameters, Description = "设置稳定区域允许的最大面积变化率")]
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
    [Display(Name = "最小差异度", GroupName = VisionTabNames.RunParameters, Description = "设置相邻稳定区域之间需要满足的最小差异度")]
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
    [Display(Name = "最大演化次数", GroupName = VisionTabNames.RunParameters, Description = "设置彩色 MSER 区域演化的最大迭代次数")]
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
    [Display(Name = "面积阈值", GroupName = VisionTabNames.RunParameters, Description = "设置区域重新初始化时使用的最小面积变化比例")]
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
    [Display(Name = "最小边距", GroupName = VisionTabNames.RunParameters, Description = "设置彩色 MSER 区域稳定性的最小边距")]
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
    [Display(Name = "边缘模糊尺寸", GroupName = VisionTabNames.RunParameters, Description = "设置梯度图像预处理使用的边缘模糊核尺寸")]
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
