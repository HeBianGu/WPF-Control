// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.Diagram.Presenter.Expressions;

public interface INodeDataExpression
{
    string GroupName { get; set; }
    string Name { get; set; }
    string Path { get; set; }
    string Type { get; set; }
}

public class NodeDataExpression : INodeDataExpression
{
    public string Path { get; set; }
    public string Type { get; set; }
    public string GroupName { get; set; }
    [Required]
    [Display(Name = "属性名称")]
    public string Name { get; set; }
    public override bool Equals(object obj)
    {
        if (obj is NodeDataExpression expression)
            return this.Path == expression.Path && this.GroupName == expression.GroupName && this.Name == expression.Name;
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(this.Path.GetHashCode(), this.GroupName.GetHashCode(), this.Name.GetHashCode());
    }
}
