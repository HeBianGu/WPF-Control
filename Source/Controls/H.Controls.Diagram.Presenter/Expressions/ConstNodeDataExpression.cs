// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.Diagram.Presenter.Expressions;

public interface IConstNodeDataExpression : INodeDataExpression
{
    object Value { get; }
    void UpdatePath();
}

public static class ConstNodeDataExpressions
{
    public static ConstNodeDataExpression<string> Empty { get; } = new ConstNodeDataExpression<string>(string.Empty, "默认") { ID= "D7ABB0A2-16DE-4417-A93E-9F1FC2FEBFA0" };
    //public static ConstNodeDataExpression<string> Null { get; } = new ConstNodeDataExpression<string>(null, "默认", "无");
    public static ConstNodeDataExpression<int> IntZore { get; } = new ConstNodeDataExpression<int>(0, "默认");
    public static ConstNodeDataExpression<double> DoubleZore { get; } = new ConstNodeDataExpression<double>(0.0, "默认");
    public static ConstNodeDataExpression<float> FloatZore { get; } = new ConstNodeDataExpression<float>(0.0f, "默认");
    public static ConstNodeDataExpression<bool> True { get; } = new ConstNodeDataExpression<bool>(true, "默认");
    public static ConstNodeDataExpression<bool> False { get; } = new ConstNodeDataExpression<bool>(false, "默认");

    public static IEnumerable<IConstNodeDataExpression> GetDefaults()
    {
        yield return Empty;
        yield return True;
        yield return False;
        yield return IntZore;
        yield return FloatZore;
        yield return DoubleZore;
    }
}

public class ConstNodeDataExpression<T> : NodeDataExpression, IGetableNodeDataExpression, IConstNodeDataExpression
{
    public ConstNodeDataExpression()
    {

    }

    public string ID { get; set; } = Guid.NewGuid().ToString();
    public ConstNodeDataExpression(T value, string groupName)
    {
        this.Value = value;
        this.Type = typeof(T).FullName;
        this.GroupName = groupName;
        this.Name = typeof(T).Name;
        this.UpdatePath();
    }

    public void UpdatePath()
    {
        this.Path = $"[{this.GroupName}].[{this.Name}]='{this.Value?.ToString()}'";
    }
    [Display(Name = "值")]
    public T Value { get; set; }

    object IConstNodeDataExpression.Value => this.Value;

    public bool TryGetExpressionValue(IDiagramData diagramData, out object value)
    {
        value = this.Value;
        return true;
    }

    public override bool Equals(object obj)
    {
        if (obj is ConstNodeDataExpression<T> expression)
            return this.Path == expression.Path 
                && this.GroupName == expression.GroupName 
                && this.Name == expression.Name 
                && this.Value.Equals(expression.Value)
                && this.ID.Equals(expression.ID);
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(this.Path.GetHashCode(), this.GroupName.GetHashCode(), this.Name.GetHashCode(),this.Value.GetHashCode(), this.ID.GetHashCode());
    }
}
