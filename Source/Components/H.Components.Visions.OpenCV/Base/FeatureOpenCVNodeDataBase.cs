// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Components.Visions.OpenCV.NodeDataGroups;

namespace H.Components.Visions.OpenCV.Base;
[Icon(FontIcons.FitPage)]
public abstract class FeatureOpenCVNodeDataBase : OpenCVNodeDataBase, IFeatureDetectorOpenCVNodeData
{
    private int _featureCountResult;
    [ReadOnly(true)]
    [Expressionable]
    [Display(Name = "特征点数量", GroupName = VisionPropertyGroupNames.ResultParameters, Description = "结果参数，此结果可应用再条件分支等作为判断参数")]
    public int FeatureCountResult
    {
        get { return _featureCountResult; }
        set
        {
            _featureCountResult = value;
            RaisePropertyChanged();
        }
    }
}
