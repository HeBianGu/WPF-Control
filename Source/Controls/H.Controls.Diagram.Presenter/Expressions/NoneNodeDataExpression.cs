// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.Diagram.Presenter.Expressions;

public class NoneNodeDataExpression : ExpressionKey
{
    public NoneNodeDataExpression()
    {
        this.NameID = "ACD2A328-C668-4787-B96B-6644358B1560";
        this.GroupNameID = "C9A0D7B5-3F8E-4D1A-BB2C-6F3E5A1B2D3E";
        this.GroupName = "默认";
        this.Name = "继承";
    }

    public override string DisplayName => $"{this.Name}";

    //public bool TryGetExpressionValue(IDiagramData diagramData, out object value)
    //{
    //    value = null;
    //    return false;
    //}
}
