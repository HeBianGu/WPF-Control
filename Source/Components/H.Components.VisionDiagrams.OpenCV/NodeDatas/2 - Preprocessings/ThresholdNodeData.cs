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
public class ThresholdNodeData : OpenCVNodeDataBase, IPreprocessingGroupableNodeData, IThresholdNodeData
{
    private double _thresh = 125.0;
    [PropertyItem(typeof(DoubleSliderTextPropertyItem))]
    [DefaultValue(125.0)]
    [Range(0.0, 255.0)]
    [Tab(VisionTabNames.RunParameters)]
    [Display(Name = "阈值", GroupName = VisionTabNames.RunParameters, Description = "设置区分前景与背景的像素阈值")]
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
    [Display(Name = "最大阈值", GroupName = VisionTabNames.RunParameters, Description = "设置二值化处理写入前景像素的最大值")]
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
    [Display(Name = "阈值类型", GroupName = VisionTabNames.RunParameters, Description = "选择二值化、反二值化或截断等阈值处理方式")]
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
        var gray = fromImage.ToGray();
        Mat mat = fromImage.Threshold(this.Thresh, this.Maxval, this.ThresholdType);
        return this.OK(mat);
    }
}
