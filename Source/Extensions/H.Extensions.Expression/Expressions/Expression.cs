// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Extensions.Expressions.Base;
using H.Extensions.Expressions.Keys;
using H.Mvvm.ViewModels.Base;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace H.Extensions.Expressions.Expressions;

public class Expression : BindableBase, IExpression
{
    public Expression()
    {

    }

    public Expression(string name, string groupName, object value) : this(name, groupName, value, value)
    {

    }

    public Expression(string name, string groupName, object value, object displayValue)
    {
        this.Name = name;
        this.GroupName = groupName;
        this.Value = value;
        this.DisplayValue = displayValue;
        this.DataType = value.GetType().FullName;
    }
    private string _GroupName;
    [ReadOnly(true)]
    [Display(Name = "分组")]
    public string GroupName
    {
        get { return _GroupName; }
        set
        {
            _GroupName = value;
            RaisePropertyChanged();
        }
    }
    private string _Name;
    [Display(Name = "名称")]
    public string Name
    {
        get { return _Name; }
        set
        {
            _Name = value;
            RaisePropertyChanged();
        }
    }
    public string DataType { get; set; }
    [JsonIgnore]
    public virtual object Value { get; set; }

    [JsonIgnore]
    public virtual object DisplayValue { get; set; }

    public virtual IExpressionKey ToKey()
    {
        return new ExpressionKey()
        {
            GroupName = this.GroupName,
            Name = this.Name,
            DisplayValue = this.DisplayValue ?? this.Value
        };
    }
}

