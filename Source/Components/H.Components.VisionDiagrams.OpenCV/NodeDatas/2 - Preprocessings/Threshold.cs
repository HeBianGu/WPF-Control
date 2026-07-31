// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Components.VisionDiagrams.OpenCV.NodeDatas.Basic;
[Icon(FontIcons.Color)]
[Display(Name = "图像二值化", GroupName = "基础函数", Description = "降噪成黑白色", Order = 3)]
public class Threshold : OpenCVNodeDataBase, IPreprocessingGroupableNodeData, IThresholdNodeData
{
    private double _thresh = 125.0;
    [PropertyItem(typeof(DoubleSliderTextPropertyItem))]
    [DefaultValue(125.0)]
    [Range(0.0, 255.0)]
    [Tab(VisionTabNames.RunParameters)]
    [Display(Name = "阈值", GroupName = VisionTabNames.RunParameters)]
    public double Thresh
    {
        get { return _thresh; }
        set
        {
            _thresh = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private double _maxval = 255;
    [PropertyItem(typeof(DoubleSliderTextPropertyItem))]
    [Range(0.0, 255.0)]
    [Tab(VisionTabNames.RunParameters)]
    [Display(Name = "最大阈值", GroupName = VisionTabNames.RunParameters)]
    public double Maxval
    {
        get { return _maxval; }
        set
        {
            _maxval = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }

    private ThresholdTypes _thresholdTypes = ThresholdTypes.Binary;
    [Tab(VisionTabNames.RunParameters)]
    [Display(Name = "阈值类型", GroupName = VisionTabNames.RunParameters)]
    public ThresholdTypes ThresholdType
    {
        get { return _thresholdTypes; }
        set
        {
            _thresholdTypes = value;
            RaisePropertyChanged();
            this.Invoke();
        }
    }
    protected override FlowableResult<IMatImage> Invoke(Mat fromImage)
    {
        var gray = fromImage.ToGrayMat();
        Mat mat = fromImage.Threshold(this.Thresh, this.Maxval, this.ThresholdType);
        return this.OK(mat);
    }
}
