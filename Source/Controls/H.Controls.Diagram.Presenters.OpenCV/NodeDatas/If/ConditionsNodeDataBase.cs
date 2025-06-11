// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System;
using System.Reflection;
using System.Text.Json.Serialization;
using H.Controls.Diagram.Presenter.Extensions;
using H.Controls.Form.PropertyItem.Attribute.SourcePropertyItem;
using H.Controls.Form.PropertyItem.ComboBoxPropertyItems;

namespace H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Image;

[Display(Name = "条件分支", GroupName = "判断条件", Description = "设置像素阈值，根据阈值执行不同路径逻辑", Order = 20)]
public class ConditionsNodeData : IfConditionNodeDataBase, IShowPropertyView
{
    public ConditionsNodeData()
    {
        this.ConditionsPrensenter = new OpenCVPropertyConditionsPrensenter();
    }
    private ISrcImageNodeData _selectedSrcImageNodeData;
    [JsonIgnore]
    [PropertyNameSourcePropertyItem(typeof(ComboBoxPropertyItem), nameof(ToNodeDatas))]
    [Display(Name = "输入源", GroupName = "基本参数")]
    public ISrcImageNodeData SelectedSrcImageNodeData
    {
        get { return _selectedSrcImageNodeData; }
        set
        {
            _selectedSrcImageNodeData = value;
            RaisePropertyChanged();
        }
    }

    private OpenCVPropertyConditionsPrensenter _conditionsPrensenter;
    public OpenCVPropertyConditionsPrensenter ConditionsPrensenter
    {
        get { return _conditionsPrensenter; }
        set
        {
            _conditionsPrensenter = value;
            RaisePropertyChanged();
            _conditionsPrensenter.LoadData(this);
        }
    }


    public object GetPropertyPresenter()
    {
        return _conditionsPrensenter;
    }

    protected override FlowableResult<Mat> Invoke(ISrcImageNodeData srcImageNodeData, IOpenCVNodeData from, IFlowableDiagramData diagram)
    {
        return this.OK(from.Mat);
    }
}

