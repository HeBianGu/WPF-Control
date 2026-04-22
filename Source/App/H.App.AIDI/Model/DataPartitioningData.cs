// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Controls.Form.Attributes;
using H.Controls.Form.PropertyItem.TextPropertyItems;
using H.Mvvm.ViewModels.Base;

namespace H.App.AIDI.Model;
public class DataPartitioningData : BindableBase
{
    private DataPartitioningType _DataPartitioningType;
    [DefaultValue(DataPartitioningType.Ratio)]
    [Display(Name = "划分方式")]
    public DataPartitioningType DataPartitioningType
    {
        get { return _DataPartitioningType; }
        set
        {
            _DataPartitioningType = value;
            RaisePropertyChanged();
        }
    }

    private double _Ratio = 70;
    [Unit("%")]
    [Range(0.0, 100.0)]
    [DefaultValue(70)]
    [PropertyItem(typeof(UnitTextPropertyItem))]
    [Display(Name = "比例")]
    public double Ratio
    {
        get { return _Ratio; }
        set
        {
            _Ratio = value;
            RaisePropertyChanged();
        }
    }
}

[TypeConverter(typeof(DisplayEnumConverter))]
public enum DataPartitioningType
{
    [Display(Name = "按比例划分")]
    Ratio,
    [Display(Name = "按数量划分(暂未实现)")]
    Count,
    [Display(Name = "随机划分(暂未实现)")]
    Random
}


