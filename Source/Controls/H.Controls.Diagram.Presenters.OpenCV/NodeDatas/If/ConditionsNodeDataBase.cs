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
using H.Controls.FilterBox;
using H.Controls.Form.PropertyItem.Attribute.SourcePropertyItem;
using H.Controls.Form.PropertyItem.ComboBoxPropertyItems;

namespace H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Image;

[Display(Name = "条件分支", GroupName = "判断条件", Description = "设置像素阈值，根据阈值执行不同路径逻辑", Order = 20)]
public class ConditionsNodeData : IfConditionNodeDataBase, IShowPropertyView
{
    private ISrcImageNodeData _selectedSrcImageNodeData;
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

    [JsonIgnore]
    [Browsable(false)]
    public IEnumerable<INodeData> ToNodeDatas
    {
        get
        {
            return this.GetToNodeDatas();
        }
    }

    public object GetPropertyPresenter()
    {
        var _propertyConfidtions = new OpenCVPropertyConditionsPrensenter(this);
        return _propertyConfidtions;
    }

    //protected override IEnumerable<IFlowablePortData> GetFlowablePortDatas(IFlowableDiagramData diagramData)
    //{
    //    var srcImageNodeData = diagramData.GetStartNodeDatas().OfType<ISrcImageNodeData>().FirstOrDefault();
    //    var ports = base.GetFlowablePortDatas(diagramData);
    //    bool r = srcImageNodeData.Mat.Width > this.Pixel || srcImageNodeData.Mat.Height > this.Pixel;
    //    if (r)
    //    {
    //        return ports.Where(p => p.Name == "像素大于");
    //    }
    //    else
    //    {
    //        return ports.Where(p => p.Name == "像素小于");
    //    }
    //}

    protected override FlowableResult<Mat> Invoke(ISrcImageNodeData srcImageNodeData, IOpenCVNodeData from, IFlowableDiagramData diagram)
    {
        return this.OK(from.Mat);
    }
}

public class OpenCVPropertyConditionsPrensenter : PropertyConditionsPrensenter<OpenCVPropertyConditionPrensenter>
{
    private ConditionsNodeData _conditionsNodeData;
    public OpenCVPropertyConditionsPrensenter(ConditionsNodeData conditionsNodeData)
    {
        this._conditionsNodeData = conditionsNodeData;
    }
    protected override OpenCVPropertyConditionPrensenter Create()
    {
        var result = new OpenCVPropertyConditionPrensenter() { ID = DateTime.Now.ToString("yyyyMMddHHmmssfff") };
        result.InputNodeDatas = this._conditionsNodeData.GetAllFromNodeDatas().ToObservable();
        result.OutNodeDatas = this._conditionsNodeData.ToNodeDatas.ToObservable();
        return result;
    }
}

