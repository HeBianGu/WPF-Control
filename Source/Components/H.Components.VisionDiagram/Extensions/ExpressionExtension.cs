// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Controls.ShapeBox.Shapes;
using Expression = H.Controls.Diagram.Presenter.Expressions.Expression;

namespace H.Components.VisionDiagram.Extensions;

public static class ExpressionExtension
{
    public static IEnumerable<IExpression> GetVisionAllDefineChildrenExpressions(this IExpression expression)
    {
        foreach (var item in expression.GetDefineChildrenExpressions())
            yield return item;
        foreach (var item in expression.GetDefineEnumerableChildrenExpressions())
            yield return item;
        foreach (var item in expression.GetVisionDefineChildrenExpressions())
            yield return item;
        foreach (var item in expression.GetVisionDefineEnumerableChildrenExpressions())
            yield return item;
    }

    private static IEnumerable<IExpression> GetVisionDefineChildrenExpressions(this IExpression expression)
    {
        var obj = expression.Value;
        string groupName = expression.ToKey().DisplayName;
        if (obj is VisionCircle circle)
          return GetExpressions(circle, groupName);
        if (obj is VisionLine line)
            return GetExpressions(line, groupName);
        return Enumerable.Empty<IExpression>();
    }

    public static IEnumerable<IExpression> GetVisionDefineEnumerableChildrenExpressions(this IExpression expression)
    {
        var obj = expression.Value;
        string groupName = expression.ToKey().DisplayName;
        if (obj is IEnumerable<VisionCircle> circles)
        {
            for (var i = 0; i < circles.Count(); i++)
            {
                var item = circles.ElementAt(i);
                var groupName2 = $"{groupName}[{i}]";
                foreach (var item2 in item.GetExpressions(groupName2))
                {
                    yield return item2;
                }
            }
        }
        if (obj is IEnumerable<VisionLine> lines)
        {
            for (var i = 0; i < lines.Count(); i++)
            {
                var item = lines.ElementAt(i);
                var groupName2 = $"{groupName}[{i}]";
                foreach (var item2 in item.GetExpressions(groupName2))
                {
                    yield return item2;
                }
            }
        }
    }

    public static IEnumerable<IExpression> GetExpressions(this VisionCircle circle, string groupName)
    {
        var rexp= new Expression("半径", groupName, circle.Radius);
        yield return rexp;
        foreach (var item in circle.Point.GetExpressions(rexp.ToKey().DisplayName))
        {
            yield return item;
        }
    }

    public static IEnumerable<IExpression> GetExpressions(this VisionLine line, string groupName)
    {
        var cexp = new Expression("起点", groupName, line.Start);
        yield return cexp;
        foreach (var item in line.Start.GetExpressions(cexp.ToKey().DisplayName))
        {
            yield return item;
        }
        var eexp = new Expression("终点", groupName, line.End);
        yield return eexp;
        foreach (var item in line.End.GetExpressions(eexp.ToKey().DisplayName))
        {
            yield return item;
        }
        var ctexp = new Expression("中点", groupName, line.Center);
        yield return cexp;
        foreach (var item in line.Center.GetExpressions(ctexp.ToKey().DisplayName))
        {
            yield return item;
        }
    }
}
