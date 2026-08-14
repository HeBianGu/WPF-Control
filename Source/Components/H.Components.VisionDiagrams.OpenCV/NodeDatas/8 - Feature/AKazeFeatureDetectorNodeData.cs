// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

global using System.Diagnostics;
namespace H.Components.VisionDiagrams.OpenCV.NodeDatas.Feature;
[Icon(FontIcons.GenericScan)]
[Display(Name = "AKAZE特征提取", GroupName = "特征提取", Description = "使用非线性尺度空间检测 AKAZE 关键点并计算特征描述子", Order = 0)]
public class AKazeFeatureDetectorNodeData : FeatureOpenCVNodeDataBase
{
    private AKAZEDescriptorType _descriptorType = AKAZEDescriptorType.MLDB;
    [Tab(VisionTabNames.RunParameters)]
    [Display(Name = "描述子类型", GroupName = VisionTabNames.RunParameters, Description = "选择 AKAZE 特征描述子的编码类型")]
    public AKAZEDescriptorType DescriptorType
    {
        get { return _descriptorType; }
        set
        {
            _descriptorType = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private int _descriptorSize = 0;
    [Tab(VisionTabNames.RunParameters)]
    [Display(Name = "描述子大小", GroupName = VisionTabNames.RunParameters, Description = "设置生成特征描述子的位数，使用 0 时采用完整长度")]
    public int DescriptorSize
    {
        get { return _descriptorSize; }
        set
        {
            _descriptorSize = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private int _descriptorChannels = 3;
    [Tab(VisionTabNames.RunParameters)]
    [Display(Name = "描述子通道数", GroupName = VisionTabNames.RunParameters, Description = "设置计算特征描述子使用的图像通道数量")]
    public int DescriptorChannels
    {
        get { return _descriptorChannels; }
        set
        {
            _descriptorChannels = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private float _threshold = 0.001f;
    [Tab(VisionTabNames.RunParameters)]
    [Display(Name = "检测阈值", GroupName = VisionTabNames.RunParameters, Description = "设置 AKAZE 检测器接受关键点响应的最低阈值")]
    public float Threshold
    {
        get { return _threshold; }
        set
        {
            _threshold = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private int _nOctaves = 4;
    [Tab(VisionTabNames.RunParameters)]
    [Display(Name = "尺度组数", GroupName = VisionTabNames.RunParameters, Description = "设置非线性尺度空间包含的尺度组数量")]
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

    private int _nOctaveLayers = 4;
    [Tab(VisionTabNames.RunParameters)]
    [Display(Name = "每组层数", GroupName = VisionTabNames.RunParameters, Description = "设置每个尺度组包含的中间层数量")]
    public int nOctaveLayers
    {
        get { return _nOctaveLayers; }
        set
        {
            _nOctaveLayers = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private KAZEDiffusivityType _diffusivity = KAZEDiffusivityType.DiffPmG2;
    [Tab(VisionTabNames.RunParameters)]
    [Display(Name = "扩散方式", GroupName = VisionTabNames.RunParameters, Description = "选择构建非线性尺度空间使用的扩散模型")]
    public KAZEDiffusivityType Diffusivity
    {
        get { return _diffusivity; }
        set
        {
            _diffusivity = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    protected override FlowableResult<IMatImage> Invoke(Mat fromImage)
    {
        Mat gray = fromImage;
        using AKAZE akaze = AKAZE.Create(this.DescriptorType, this.DescriptorSize, this.DescriptorChannels, this.Threshold, this.nOctaves, this.nOctaveLayers, this.Diffusivity);
        //var kazeDescriptors = new Mat();
        using Mat akazeDescriptors = new Mat();
        KeyPoint[] akazeKeyPoints = null;
        TimeSpan akazeTime = MeasureTime(() =>
            akaze.DetectAndCompute(gray, null, out akazeKeyPoints, akazeDescriptors));
        Mat dstAkaze = new Mat();
        Cv2.DrawKeypoints(gray, akazeKeyPoints, dstAkaze);
        this.FeatureCountResult = akazeKeyPoints.Length;
        return this.OK(dstAkaze, akazeKeyPoints.ToResultPresenter(), this.FeatureCountResult.ToDetectSuccessMessage());
    }

    private TimeSpan MeasureTime(Action action)
    {
        Stopwatch watch = Stopwatch.StartNew();
        action();
        watch.Stop();
        return watch.Elapsed;
    }
}
