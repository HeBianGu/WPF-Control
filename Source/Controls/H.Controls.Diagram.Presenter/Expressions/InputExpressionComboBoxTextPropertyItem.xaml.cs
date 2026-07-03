// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Controls.Form.PropertyItem.TextPropertyItems;

namespace H.Controls.Diagram.Presenter.Expressions;


/// <summary>
/// 在表达式中使用的参数，支持输入表达式和选择参数两种方式，输入表达式需要包含在集合中
/// </summary>
public class InputExpressionComboBoxTextPropertyItem : ComboBoxTextPropertyItem
{
    public InputExpressionComboBoxTextPropertyItem(PropertyInfo property, object obj) : base(property, obj)
    {
    }
}

/// <summary>
/// 输入的值需要是T类型的值，如果不是则会提示错误
/// </summary>
/// <typeparam name="T"></typeparam>
public class InputExpressionComboBoxTextPropertyItem<T> : InputExpressionComboBoxTextPropertyItem
{
    public InputExpressionComboBoxTextPropertyItem(PropertyInfo property, object obj) : base(property, obj)
    {

    }

    protected override bool CheckType(string value, out string error)
    {
        var br = base.CheckType(value, out error);
        if (br)
            return true;
        if (value.TryChangeType(out T result))
        {
            error = null;
            return true;
        }
        else
        {
            error = $"[{this.Name}]不是有效的{typeof(T).Name}并且在数据源中无法找到匹配项";
            return false;
        }
    }
}
