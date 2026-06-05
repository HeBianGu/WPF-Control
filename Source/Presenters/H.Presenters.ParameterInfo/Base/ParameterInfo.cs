// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Mvvm.ViewModels.Base;

namespace H.Presenters.ParameterInfo.Base;

/// <summary>
/// 参数描述。
/// </summary>
public class ParameterInfo : BindableBase, IParameterInfo
{
    private string _Name;
    public string Name
    {
        get { return _Name; }
        set
        {
            _Name = value;
            RaisePropertyChanged();
        }
    }

    private string _DisplayName;

    public string DisplayName
    {
        get { return _DisplayName; }
        set
        {
            _DisplayName = value;
            RaisePropertyChanged();
        }
    }


    private ParameterType _Type;

    public ParameterType Type
    {
        get { return _Type; }
        set
        {
            _Type = value;
            RaisePropertyChanged();
        }
    }


    private ParameterAccess _Access;

    public ParameterAccess Access
    {
        get { return _Access; }
        set
        {
            _Access = value;
            RaisePropertyChanged();
        }
    }


    private object _Minimum;
    public object Minimum
    {
        get { return _Minimum; }
        set
        {
            _Minimum = value;
            RaisePropertyChanged();
        }
    }


    private object _Maximum;

    public object Maximum
    {
        get { return _Maximum; }
        set
        {
            _Maximum = value;
            RaisePropertyChanged();
        }
    }


    private object _Increment;

    public object Increment
    {
        get { return _Increment; }
        set
        {
            _Increment = value;
            RaisePropertyChanged();
        }
    }


    private object _DefaultValue;

    public object DefaultValue
    {
        get { return _DefaultValue; }
        set
        {
            _DefaultValue = value;
            RaisePropertyChanged();
        }
    }


    private object _CurrentValue;

    public object CurrentValue
    {
        get { return _CurrentValue; }
        set
        {
            _CurrentValue = value;
            RaisePropertyChanged();
        }
    }


    private IReadOnlyList<string> _Options;

    public IReadOnlyList<string> Options
    {
        get { return _Options; }
        set
        {
            _Options = value;
            RaisePropertyChanged();
        }
    }


    private string _Unit;

    public string Unit
    {
        get { return _Unit; }
        set
        {
            _Unit = value;
            RaisePropertyChanged();
        }
    }


    private string _Category;

    public string Category
    {
        get { return _Category; }
        set
        {
            _Category = value;
            RaisePropertyChanged();
        }
    }


    private string _Description;
    public string Description
    {
        get { return _Description; }
        set
        {
            _Description = value;
            RaisePropertyChanged();
        }
    }
}


