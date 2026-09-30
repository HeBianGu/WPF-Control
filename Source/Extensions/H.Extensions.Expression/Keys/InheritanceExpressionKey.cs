// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Extensions.Expressions.Keys;

public interface IInheritanceExpressionKey : IExpressionKey
{

}

public class InheritanceExpressionKey : ExpressionKey, IInheritanceExpressionKey
{
    public const string NAME = "继承";
    public InheritanceExpressionKey()
    {
        this.GroupName = "默认";
        this.Name = "继承";
    }
}
