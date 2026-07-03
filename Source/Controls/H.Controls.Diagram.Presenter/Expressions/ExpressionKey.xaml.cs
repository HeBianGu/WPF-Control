// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Services.Serializable;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace H.Controls.Diagram.Presenter.Expressions;

[TypeConverter(typeof(ExpressionKeyTypeConverter))]
public interface IExpressionKey
{
    /// <summary>
    /// 唯一名称
    /// </summary>
    string Name { get; set; }
    /// <summary>
    /// 分组名称
    /// </summary>
    string GroupName { get; set; }
}

public interface IExpression
{
    /// <summary>
    /// 唯一名称
    /// </summary>
    string Name { get; set; }
    /// <summary>
    /// 分组名称
    /// </summary>
    string GroupName { get; set; }
    /// <summary>
    /// 数据类型
    /// </summary>
    string DataType { get; set; }

    object Value { get; set; }

    IExpressionKey ToKey();
}

public class Expression : BindableBase, IExpression
{
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

    public virtual IExpressionKey ToKey()
    {
        return new ExpressionKey() { GroupName = this.GroupName, Name = this.Name };
    }
}

public class ExpressionKey : BindableBase, IExpressionKey
{
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

    public virtual string DisplayName => $"{this.GroupName}.{this.Name}";


    public override bool Equals(object obj)
    {
        if (obj is IExpressionKey expression)
            return this.Name == expression.Name && this.GroupName == expression.GroupName;
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(this.Name, this.GroupName);
    }
}

public interface IInputStringExpressionKey : IExpressionKey
{
    object Value { get; set; }
    (bool success, T value) TryParse<T>();
}

public class InputStringExpressionKey : ExpressionKey, IInputStringExpressionKey
{
    public object Value { get; set; }
    public override bool Equals(object obj)
    {
        if (obj is IInputStringExpressionKey expression)
            return this.Value == expression.Value;
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(this.Value);
    }

    public (bool success, T value) TryParse<T>()
    {
        var r = this.Value.TryChangeType<T>(out T result);
        return (r, result);
    }
}

[IgnoreTypeConverterJsonConverter]
public class ExpressionKeyTypeConverter : TypeConverter
{
    public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
    {
        if (sourceType == typeof(string))
            return true;
        var r = base.CanConvertFrom(context, sourceType);
        return r;
    }
    public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
    {
        if (value is string str)
        {
            if (str == InheritanceExpressionKey.NAME)
                return new InheritanceExpressionKey();
            if (double.TryParse(str, out double dvalue))
                return new InputStringExpressionKey() { Value = str };
            var arr = str.Split('.');
            if (arr.Length == 2)
            {
                return new ExpressionKey() { GroupName = arr[0], Name = arr[1] };
            }
            else
            {
                return new InputStringExpressionKey() { Value = str };
            }
        }
        return base.ConvertFrom(context, culture, value);
    }
    public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
    {
        if (destinationType == typeof(string))
        {
            if (value is InputStringExpressionKey primitiveExpressionKey)
            {
                return $"{primitiveExpressionKey.Value}";
            }
            if (value is InheritanceExpressionKey inheritanceExpressionKey)
            {
                return $"{InheritanceExpressionKey.NAME}";
            }
            if (value is ExpressionKey key)
            {
                return $"{key.GroupName}.{key.Name}";
            }
        }
        return base.ConvertTo(context, culture, value, destinationType);
    }
}

