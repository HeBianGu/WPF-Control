// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Globalization;

namespace H.Controls.Diagram.Presenter.Expressions;

public interface IVarExpression : IExpression
{

}

public class VarExpression<T> : Expression, IVarExpression
{
    public VarExpression()
    {

    }
    public VarExpression(T value, string groupName)
    {
        this.CurrentValue = value;
        this.GroupName = groupName;
        this.DataType = typeof(T).FullName;
    }

    [Display(Name = "注释")]
    public string Description { get; set; }

    [Display(Name = "当前值")]
    public T CurrentValue { get; set; }

    //public override IExpressionKey ToKey()
    //{
    //    return new VarExpressionKey() { Name = this.Name, GroupName = this.GroupName };
    //}
}


