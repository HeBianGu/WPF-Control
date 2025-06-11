// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Collections.ObjectModel;
using System.Reflection;
using System.Text.Json.Serialization;
using H.Controls.FilterBox;

namespace H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Image;

public interface IOpenCVPropertyConditionPrensenter : IPropertyConditionPrensenter
{
    int SelectedInputIndex { get; set; }
    INodeData SelectedInputNodeData { get; set; }
    int SelectedOutputIndex { get; set; }
    INodeData SelectedOutputNodeData { get; set; }

    bool IsMatchInputNode();
    void UpdateProperties(INodeData value);
}

public class OpenCVPropertyConditionPrensenter : PropertyConditionPrensenter, IOpenCVPropertyConditionPrensenter
{
    private int _selectedInputIndex;
    public int SelectedInputIndex
    {
        get { return _selectedInputIndex; }
        set
        {
            _selectedInputIndex = value;
            RaisePropertyChanged();
        }
    }


    private INodeData _selectedInputNodeData;
    [JsonIgnore]
    [Browsable(false)]
    public INodeData SelectedInputNodeData
    {
        get { return _selectedInputNodeData; }
        set
        {
            _selectedInputNodeData = value;
            RaisePropertyChanged();
        }
    }

    public void UpdateProperties(INodeData value)
    {
        if (value == null)
        {
            this.Properties.Clear();
            return;
        }

        this.Properties = value.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
               .Where(x => x.PropertyType.IsPrimitive || x.PropertyType == typeof(string) || x.PropertyType == typeof(DateTime))
               .Where(x => x.GetCustomAttribute<DisplayAttribute>()?.GroupName.Contains("数据") == true).ToObservable();
    }

    private int _selectedOutputIndex;
    public int SelectedOutputIndex
    {
        get { return _selectedOutputIndex; }
        set
        {
            _selectedOutputIndex = value;
            RaisePropertyChanged();
        }
    }

    private INodeData _selectedOutputNodeData;
    [Browsable(false)]
    [JsonIgnore]
    public INodeData SelectedOutputNodeData
    {
        get { return _selectedOutputNodeData; }
        set
        {
            _selectedOutputNodeData = value;
            RaisePropertyChanged();
        }
    }

    public bool IsMatchInputNode()
    {
        return this.IsMatch(this.SelectedInputNodeData);
    }

    public override bool IsMatch(object obj)
    {
        if (this.Conditions.Count == 0)
            return false;
        return base.IsMatch(obj);
    }

}

