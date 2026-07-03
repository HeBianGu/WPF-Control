// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Controls.Diagram.Presenter.Expressions;

namespace H.Controls.Diagram.Presenter.NodeDatas.Base;

public interface IExpressionNodeData : ITextNodeData, IGetExpressionsable, IDefaultValueExpressionable, INodeData, IGetExpressionKeysable
{

}

public abstract class ExpressionNodeDataBase : ShowPropertyViewNodeDataBase, IExpressionNodeData
{
    public IEnumerable<IExpression> GetFromExpressions()
    {
        List<IExpression> result = new List<IExpression>();
        if (this is IDefaultValueExpressionable defaultValueExpressionable)
        {
            var defaults = defaultValueExpressionable.GetDefaultValueExpressions();
            foreach (var item in defaults)
                yield return item;
        }

        if (this.DiagramData is IGetExpressionsable expressionable)
        {
            foreach (var item in expressionable.GetExpressions())
                yield return item;
        }
        var allfrom = this.AllFromNodeDatas.OfType<IExpressionNodeData>().OrderBy(x => x.Text).SelectMany(x => x.GetExpressions());
        foreach (var item in allfrom)
            yield return item;
    }

    public IEnumerable<IExpressionKey> GetIntFromExpressionKeys() => this.GetFromExpressionKeys<int>();
    public IEnumerable<IExpressionKey> GetDoubleFromExpressionKeys() => this.GetFromExpressionKeys<double>();
    public IEnumerable<IExpressionKey> GetFloatFromExpressionKeys() => this.GetFromExpressionKeys<float>();
    public IEnumerable<IExpressionKey> GetStringFromExpressionKeys() => this.GetFromExpressionKeys<string>();

    public IEnumerable<IExpressionKey> GetPrimitiveFromExpressionKeys() => this.GetFromExpressionKeys<string>().Concat(this.GetFromExpressionKeys<float>()).Concat(this.GetFromExpressionKeys<double>()).Concat(this.GetFromExpressionKeys<int>());
    public IEnumerable<IExpressionKey> GetBoolFromExpressionKeys() => this.GetFromExpressionKeys<bool>();
    public IEnumerable<IExpressionKey> GetUIntFromExpressionKeys() => this.GetFromExpressionKeys<uint>();
    public IEnumerable<IExpressionKey> GetRectFromExpressionKeys() => this.GetFromExpressionKeys<Rect>();
    public IEnumerable<IExpressionKey> GetPointFromExpressionKeys() => this.GetFromExpressionKeys<Point>();

    public IEnumerable<IExpressionKey> GetPointssFromExpressionKeys() => this.GetFromExpressionKeys<Point[][]>();
    public IEnumerable<IExpressionKey> GetSizeFromExpressionKeys() => this.GetFromExpressionKeys<Size>();
    public virtual IEnumerable<IExpression> GetExpressions(Predicate<object> predicate = null)
    {
        return this.GetPropertyInfoExpressions(this.Text, predicate);
    }

    public virtual IEnumerable<IExpression> GetDefaultValueExpressions(Predicate<object> predicate = null)
    {
        yield break;
    }
}