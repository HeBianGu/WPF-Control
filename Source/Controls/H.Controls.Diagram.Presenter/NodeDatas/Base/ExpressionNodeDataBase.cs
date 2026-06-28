// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Controls.Diagram.Presenter.Expressions;
using System;

namespace H.Controls.Diagram.Presenter.NodeDatas.Base;

public interface IExpressionNodeData : ITextNodeData, IExpressionable, IDefaultValueExpressionable, INodeData, IFromExpressionSource
{

}

public abstract class ExpressionNodeDataBase : ShowPropertyViewNodeDataBase, IExpressionNodeData
{
    public IEnumerable<IExpressionKey> GetFromExpressions<T>()
    {
        List<IExpressionKey> result = new List<IExpressionKey>();
        if (this is IDefaultValueExpressionable defaultValueExpressionable)
        {
            var defaults = defaultValueExpressionable.GetDefaultValueExpressions();
            result = result.Concat(defaults).ToList();
        }
        if (this.DiagramData is IExpressionable expressionable)
        {
            result.AddRange(expressionable.GetExpressions());
        }
        var allfrom = this.AllFromNodeDatas.OfType<IExpressionNodeData>().OrderBy(x => x.Text).SelectMany(x => x.GetExpressions());
        result = result.Concat(allfrom).Where(x => x.DataType == typeof(T).FullName).ToList();
        return result;
    }

    public IEnumerable<IExpressionKey> GetIntFromExpressions() => this.GetFromExpressions<int>();
    public IEnumerable<IExpressionKey> GetDoubleFromExpressions() => this.GetFromExpressions<double>();
    public IEnumerable<IExpressionKey> GetFloatFromExpressions() => this.GetFromExpressions<float>();
    public IEnumerable<IExpressionKey> GetStringFromExpressions() => this.GetFromExpressions<string>();

    public IEnumerable<IExpressionKey> GetPrimitiveFromExpressions() => this.GetFromExpressions<string>().Concat(this.GetFromExpressions<float>()).Concat(this.GetFromExpressions<double>()).Concat(this.GetFromExpressions<int>());
    public IEnumerable<IExpressionKey> GetBoolFromExpressions() => this.GetFromExpressions<bool>();
    public IEnumerable<IExpressionKey> GetUIntFromExpressions() => this.GetFromExpressions<uint>();
    public IEnumerable<IExpressionKey> GetRectFromExpressions() => this.GetFromExpressions<Rect>();
    public IEnumerable<IExpressionKey> GetPointFromExpressions() => this.GetFromExpressions<Point>();

    public IEnumerable<IExpressionKey> GetPointssFromExpressions() => this.GetFromExpressions<Point[][]>();
    public IEnumerable<IExpressionKey> GetSizeFromExpressions() => this.GetFromExpressions<Size>();
    public virtual IEnumerable<IExpressionKey> GetExpressions(Predicate<object> predicate = null)
    {
        return this.GetPropertyInfoExpressions(this.ID, this.Text, predicate);
    }

    //public virtual (bool success, T value) GetExpressionValue<T>(IExpressionKey expressionKey)
    //{
    //    var r = this.GetFromExpressions<T>().FirstOrDefault(x => x.Equals(expressionKey));
    //    if (r == null)
    //        return (false, default);
    //    if (r.Value is T tValue)
    //        return (true, tValue);
    //    return (false, default);
    //}

    //public virtual bool TryGetExpressionValue<T>(IExpressionKey expression, out T value)
    //{
    //    value = default;
    //    var key = this.GetExpressions().FirstOrDefault(x => x == expression);
    //    if (key.Value is T tValue)
    //    {
    //        value = tValue;
    //        return true;
    //    }
    //    return false;
    //}

    //public virtual bool TryGetExpressionValue(ExpressionKey expression, out object value)
    //{
    //    return expression.TryGetExpressionValue(this.DiagramData, out value);
    //}

    public virtual IEnumerable<IExpressionKey> GetDefaultValueExpressions(Predicate<object> predicate = null)
    {
        yield break;
    }
}