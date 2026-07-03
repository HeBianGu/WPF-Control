// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.Diagram.Presenter.Expressions;

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

