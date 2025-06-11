// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Reflection;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Windows.Controls;
using H.Controls.Diagram.Presenter.Extensions;
using H.Controls.FilterBox;
using H.Mvvm.Commands;

namespace H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Image;

[Display(Name = "条件分支参数设置")]
public class OpenCVPropertyConditionsPrensenter : PropertyConditionsPrensenter<OpenCVPropertyConditionPrensenter>
{
    private ConditionsNodeData _conditionsNodeData;
    public ConditionsNodeData ConditionsNodeData
    {
        get { return _conditionsNodeData; }
        set
        {
            _conditionsNodeData = value;
            RaisePropertyChanged();
        }
    }

    protected override OpenCVPropertyConditionPrensenter Create()
    {
        return new OpenCVPropertyConditionPrensenter() { ID = DateTime.Now.ToString("yyyyMMddHHmmssfff") };
    }

    public RelayCommand SelectionInputChangedCommand => new RelayCommand(x =>
    {
        if (x is SelectionChangedEventArgs args && args.OriginalSource is ComboBox combo && combo.IsMouseCaptured && this.SelectedItem != null)
        {
            this.SelectedItem.Conditions.Clear();
            this.SelectedItem.UpdateProperties(this.SelectedItem.SelectedInputNodeData);
        }
    });

    public void LoadData(ConditionsNodeData conditionsNodeData)
    {
        this.ConditionsNodeData = conditionsNodeData;
        foreach (OpenCVPropertyConditionPrensenter item in this.PropertyConfidtions)
        {
            item.SelectedInputNodeData = conditionsNodeData.AllFromNodeDatas.ElementAtOrDefault(item.SelectedInputIndex);
            item.SelectedOutputNodeData = conditionsNodeData.ToNodeDatas.ElementAtOrDefault(item.SelectedOutputIndex);
            item.UpdateProperties(item.SelectedInputNodeData);
            foreach (IPropertyConfidtion confidtion in item.Conditions)
            {
                PropertyInfo propertyInfo = item.Properties.FirstOrDefault(x => x.Name == confidtion.Filter.PropertyName);
                confidtion.Filter.PropertyInfo = propertyInfo;
            }
        }
        this.SelectedItem = this.PropertyConfidtions?.FirstOrDefault();
    }
}

