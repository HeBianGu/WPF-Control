// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Globalization;

namespace H.Controls.Diagram.Presenter.Expressions;

public interface IConstExpressionKey : IExpressionKey
{

}

// 这里的静态资源是单例，不可以用在默认值，会有冲突
public static class ConstNodeDataExpressions
{
    public static ConstExpressionKey<string> Empty { get; } = new ConstExpressionKey<string>(string.Empty, "默认");
    //public static ConstNodeDataExpression<string> Null { get; } = new ConstNodeDataExpression<string>(null, "默认", "无");
    public static ConstExpressionKey<int> IntZore { get; } = new ConstExpressionKey<int>(0, "默认");
    public static ConstExpressionKey<double> DoubleZore { get; } = new ConstExpressionKey<double>(0.0, "默认");
    public static ConstExpressionKey<float> FloatZore { get; } = new ConstExpressionKey<float>(0.0f, "默认");
    public static ConstExpressionKey<bool> True { get; } = new ConstExpressionKey<bool>(true, "默认");
    public static ConstExpressionKey<bool> False { get; } = new ConstExpressionKey<bool>(false, "默认");

    public static IEnumerable<IConstExpressionKey> GetDefaults()
    {
        yield return Empty;
        yield return True;
        yield return False;
        yield return IntZore;
        yield return FloatZore;
        yield return DoubleZore;
    }
}

public class ConstExpressionKey<T> : ExpressionKey, IConstExpressionKey
{
    public ConstExpressionKey()
    {

    }
    public ConstExpressionKey(T value, string groupName)
    {
        this.CurrentValue = value;
        this.GroupName = groupName;
        this.DataType = typeof(T).FullName;
    }

    [Display(Name = "当前值")]
    public T CurrentValue { get; set; }

    public override object Value { get => this.CurrentValue; set => this.CurrentValue = (T)value; }
}


