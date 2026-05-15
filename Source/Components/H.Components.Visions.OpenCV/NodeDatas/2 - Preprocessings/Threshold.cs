// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

global using H.Components.Vision.NodeGroups.Preprocessings;

namespace H.Components.Visions.OpenCV.NodeDatas.Basic;

public interface IThresholdNodeData
{

}

[Icon(FontIcons.Color)]
[Display(Name = "二值化", GroupName = "基础函数", Description = "降噪成黑白色", Order = 3)]
public class Threshold : OpenCVNodeDataBase, IPreprocessingGroupableNodeData, IThresholdNodeData
{
    private double _thresh = 125.0;
    [PropertyItem(typeof(DoubleSliderTextPropertyItem))]
    [DefaultValue(125.0)]
    [Range(0.0, 255.0)]
    [Display(Name = "阈值", GroupName = VisionPropertyGroupNames.RunParameters)]
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
    [Display(Name = "最大阈值", GroupName = VisionPropertyGroupNames.RunParameters)]
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
    [Display(Name = "阈值类型", GroupName = VisionPropertyGroupNames.RunParameters)]
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
        Mat mat = fromImage.Threshold(this.Thresh, this.Maxval, this.ThresholdType);
        return this.OK(mat);
    }
}
