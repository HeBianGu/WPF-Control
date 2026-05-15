// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")
global using H.Extensions.Behvaiors.DataGrids;

namespace H.Components.Vision.Presenters.ResultPresenters;

public interface IResultPresenterItem
{
    int Index { get; set; }
    string Name { get; set; }
}

public abstract class ResultPresenterItemBase : BindableBase, IResultPresenterItem
{
    [DataGridColumn("Auto")]
    [Display(Name = "序号", GroupName = "基础信息", Order = -2)]
    public int Index { get; set; }
    private string _name;
    [DataGridColumn("*")]
    [Display(Name = "结果信息", GroupName = "基础信息", Order = -1)]
    public string Name
    {
        get { return _name; }
        set
        {
            _name = value;
            RaisePropertyChanged();
        }
    }
}

public class AreaCenterResultPresenterItem : ResultPresenterItemBase
{
    private double _centerX;
    [DataGridColumn("*")]
    [Display(Name = "中心（X）", GroupName = "基础信息")]
    public double CenterX
    {
        get { return _centerX; }
        set
        {
            _centerX = value;
            RaisePropertyChanged();
        }
    }


    private double _centerY;
    [DataGridColumn("*")]
    [Display(Name = "中心（Y）", GroupName = "基础信息")]
    public double CenterY
    {
        get { return _centerY; }
        set
        {
            _centerY = value;
            RaisePropertyChanged();
        }
    }


    private double _area;
    [DataGridColumn("*")]
    [Display(Name = "面积", GroupName = "基础信息")]
    public double Area
    {
        get { return _area; }
        set
        {
            _area = value;
            RaisePropertyChanged();
        }
    }
}

public interface IValueResultPresenterItem
{
    object Value { get; }
}

public class ValueResultPresenterItem<T> : ResultPresenterItemBase, IValueResultPresenterItem
{
    public ValueResultPresenterItem()
    {

    }
    public ValueResultPresenterItem(T value)
    {
        this.Value = value;
    }
    private T _value;
    [DataGridColumn("*")]
    [Display(Name = "结果数值", GroupName = "基础信息")]
    public T Value
    {
        get { return _value; }
        set
        {
            _value = value;
            RaisePropertyChanged();
        }
    }

    object IValueResultPresenterItem.Value => this.Value;
}

public class Value2ResultPresenterItem<T> : ResultPresenterItemBase
{
    private T _value1;
    [DataGridColumn("*")]
    [Display(Name = "结果数值1", GroupName = "基础信息")]
    public T Value1
    {
        get { return _value1; }
        set
        {
            _value1 = value;
            RaisePropertyChanged();
        }
    }

    private T _value2;
    [DataGridColumn("*")]
    [Display(Name = "结果数值2", GroupName = "基础信息")]
    public T Value2
    {
        get { return _value2; }
        set
        {
            _value2 = value;
            RaisePropertyChanged();
        }
    }
}

public class Value3ResultPresenterItem<T> : Value2ResultPresenterItem<T>
{
    private T _value3;
    [DataGridColumn("*")]
    [Display(Name = "结果数值3", GroupName = "基础信息")]
    public T Value3
    {
        get { return _value3; }
        set
        {
            _value3 = value;
            RaisePropertyChanged();
        }
    }
}

public class Value4ResultPresenterItem<T> : Value3ResultPresenterItem<T>
{
    private T _value4;
    [DataGridColumn("*")]
    [Display(Name = "结果数值4", GroupName = "基础信息")]
    public T Value4
    {
        get { return _value4; }
        set
        {
            _value4 = value;
            RaisePropertyChanged();
        }
    }
}

public class Value5ResultPresenterItem<T> : Value4ResultPresenterItem<T>
{
    private T _value5;
    [DataGridColumn("*")]
    [Display(Name = "结果数值5", GroupName = "基础信息")]
    public T Value5
    {
        get { return _value5; }
        set
        {
            _value5 = value;
            RaisePropertyChanged();
        }
    }
}


public class StringResultItem : ValueResultPresenterItem<string>
{
    public StringResultItem(string value) : base(value)
    {
    }
}

