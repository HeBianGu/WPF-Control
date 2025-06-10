// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Collections.ObjectModel;
using System.Reflection;
using H.Controls.FilterBox;

namespace H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Image;

public class OpenCVPropertyConditionPrensenter : PropertyConditionPrensenter
{
    private ObservableCollection<INodeData> _inputNodeDatas = new ObservableCollection<INodeData>();
    public ObservableCollection<INodeData> InputNodeDatas
    {
        get { return _inputNodeDatas; }
        set
        {
            _inputNodeDatas = value;
            RaisePropertyChanged();
            this.SelectedInputNodeData = value?.FirstOrDefault();
        }
    }

    private INodeData _selectedInputNodeData;
    public INodeData SelectedInputNodeData
    {
        get { return _selectedInputNodeData; }
        set
        {
            _selectedInputNodeData = value;
            RaisePropertyChanged();
            this.Conditions.Clear();
            this.Properties = value.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(x => x.PropertyType.IsPrimitive ||x.PropertyType == typeof(string) || x.PropertyType == typeof(DateTime))
                .Where(x => x.GetCustomAttribute<DisplayAttribute>()?.GroupName.Contains("数据") == true).ToObservable();
        }
    }

    private ObservableCollection<INodeData> _outPutNodeDatas = new ObservableCollection<INodeData>();
    public ObservableCollection<INodeData> OutNodeDatas
    {
        get { return _outPutNodeDatas; }
        set
        {
            _outPutNodeDatas = value;
            RaisePropertyChanged();
            this.SelectedOutputNodeData = value?.FirstOrDefault();
        }
    }

    private INodeData _selectedOutputNodeData;
    public INodeData SelectedOutputNodeData
    {
        get { return _selectedOutputNodeData; }
        set
        {
            _selectedOutputNodeData = value;
            RaisePropertyChanged();
        }
    }


}

