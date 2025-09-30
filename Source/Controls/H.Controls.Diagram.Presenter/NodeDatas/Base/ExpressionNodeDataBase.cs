// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Controls.Diagram.Presenter.Expressions;

namespace H.Controls.Diagram.Presenter.NodeDatas.Base;

public interface IExpressionNodeData : ITextNodeData, IExpressionable
{

}

public abstract class ExpressionNodeDataBase : ShowPropertyViewNodeDataBase, IExpressionNodeData
{
    public IEnumerable<NodeDataExpression> GetFromExpressions<T>()
    {
        List<NodeDataExpression> result = new List<NodeDataExpression>();
        if (this.DiagramData is IExpressionable expressionable)
        {
            result.AddRange(expressionable.GetExpressions());
        }
        var allfrom = this.AllFromNodeDatas.OfType<IExpressionNodeData>().OrderBy(x => x.Text).SelectMany(x => x.GetExpressions());
        result = result.Concat(allfrom).Where(x => x.Type == typeof(T).FullName).ToList();
        return result;
    }

    public IEnumerable<NodeDataExpression> GetIntFromExpressions() => this.GetFromExpressions<int>();
    public IEnumerable<NodeDataExpression> GetDoubleFromExpressions() => this.GetFromExpressions<double>();
    public IEnumerable<NodeDataExpression> GetFloatFromExpressions() => this.GetFromExpressions<float>();
    public IEnumerable<NodeDataExpression> GetStringFromExpressions() => this.GetFromExpressions<string>();
    public IEnumerable<NodeDataExpression> GetBoolFromExpressions() => this.GetFromExpressions<bool>();
    public IEnumerable<NodeDataExpression> GetUIntFromExpressions() => this.GetFromExpressions<uint>();
    public IEnumerable<NodeDataExpression> GetRectFromExpressions() => this.GetFromExpressions<Rect>();
    public IEnumerable<NodeDataExpression> GetPointFromExpressions() => this.GetFromExpressions<Point>();
    public IEnumerable<NodeDataExpression> GetSizeFromExpressions() => this.GetFromExpressions<Size>();
    public virtual IEnumerable<NodeDataExpression> GetExpressions()
    {
        return this.GetExpressions(this.Text, this.Text);
    }

    public virtual bool TryGetExpressionValue<T>(NodeDataExpression expression, out T value)
    {
        value = default;
        if (expression == null)
            return false;
        if (this.TryGetExpressionValue(expression, out object objValue))
        {
            if (objValue is T tValue)
            {
                value = tValue;
                return true;
            }
        }
        return false;
    }

    public virtual bool TryGetExpressionValue(NodeDataExpression expression, out object value)
    {
        return expression.TryGetExpressionValue(this.DiagramData, out value);
    }
}