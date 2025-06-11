// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using H.Controls.Diagram.Presenter.Extensions;
using H.Controls.Form.PropertyItem.Attribute.SourcePropertyItem;
using H.Controls.Form.PropertyItem.ComboBoxPropertyItems;

namespace H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Image;

[Display(Name = "条件分支", GroupName = "判断条件", Description = "设置像素阈值，根据阈值执行不同路径逻辑", Order = 20)]
public class ConditionsNodeData : IfConditionNodeDataBase, IShowPropertyView, IOnDiagramDeserialized
{
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

        }
    }

    public object GetPropertyPresenter()
    {
        if (this._conditionsPrensenter == null)
        {
            this._conditionsPrensenter = new OpenCVPropertyConditionsPrensenter();
            this._conditionsPrensenter.LoadData(this);
        }
        return _conditionsPrensenter;
    }

    protected override FlowableResult<Mat> Invoke(ISrcImageNodeData srcImageNodeData, IOpenCVNodeData from, IFlowableDiagramData diagram)
    {
        return this.OK(from.Mat);
    }

    public void OnDiagramDeserialized()
    {
        if (this._conditionsPrensenter == null)
        {
            this._conditionsPrensenter = new OpenCVPropertyConditionsPrensenter();
            this._conditionsPrensenter.LoadData(this);
        }
        _conditionsPrensenter.LoadData(this);
    }

    protected override IEnumerable<Tuple<IFlowablePortData, Predicate<IFlowableLinkData>>> GetFlowablePortDatas(IFlowableDiagramData diagramData)
    {
        if (this._conditionsPrensenter == null)
            yield break;
        var matches = this._conditionsPrensenter.PropertyConfidtions.Where(x => x.IsMatchInputNode()).ToList();
        var toNodeDatas = matches.Select(x => x.SelectedOutputNodeData).Where(x => x != null);
        foreach (var toNodeData in toNodeDatas)
        {
            foreach (IFlowablePortData item in this.GetToNodePortData(toNodeData, diagramData).OfType<IFlowablePortData>())
            {
                System.Diagnostics.Debug.WriteLine(item.NodeID);
                var predicate = new Predicate<IFlowableLinkData>(x => x.ToNodeID == toNodeData.ID);
                yield return new Tuple<IFlowablePortData, Predicate<IFlowableLinkData>>(item, predicate);
            }
        }
    }
}

