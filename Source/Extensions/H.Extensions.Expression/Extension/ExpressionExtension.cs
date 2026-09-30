// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Extensions.Common;
using H.Extensions.Expressions.Base;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Windows;
using Expression = H.Extensions.Expressions.Expressions.Expression;

namespace H.Extensions.Expressions.Extension;

public static class ExpressionExtension
{
    public static IEnumerable<IExpression> GetPropertyInfoExpressions(this IGetExpressionsable expressionable, string groupName, Predicate<object> predicate = null)
    {
        return GetObjExpressions(expressionable, groupName, predicate);
    }
    private static IEnumerable<IExpression> GetObjExpressions(object obj, string groupName, Predicate<object> predicate = null)
    {
        var properties = obj.GetType().GetProperties().Where(x => x.CanRead);
        foreach (var property in properties)
        {
            var display = property.GetCustomAttribute<DisplayAttribute>();
            if (display == null)
                continue;
            var expressionableAttribute = property.GetCustomAttribute<ExpressionableAttribute>();
            if (expressionableAttribute == null)
                continue;
            //string np = string.IsNullOrEmpty(path) ? $"[{display.Name}]" : $"[{path.TrimStart('[').TrimEnd(']')}].[{display.Name}]";
            var ne = new Expression()
            {
                GroupName = groupName,
                DataType = property.PropertyType.FullName,
                Name = display.Name,
                Value = property.GetValue(obj)
            };
            yield return ne;
            if (property.PropertyType.IsPrimitive)
                continue;
            var exression = property.GetCustomAttribute<ExpressionableAttribute>();
            if (exression == null)
                continue;
            var value = property.GetValue(obj);
            if (value == null)
                continue;
            if (predicate?.Invoke(value) == false)
                continue;
            var nes = GetObjExpressions(value, groupName, predicate);
            foreach (var item in nes)
            {
                yield return item;
            }
        }
    }
    public static IEnumerable<IExpression> GetDefineChildrenExpressions(this IExpression expression)
    {
        var obj = expression.Value;
        if (obj is Point point)
            return point.GetExpressions(expression.GroupName, expression.Name);
        if (obj is Rect rect)
            return rect.GetExpressions(expression.GroupName, expression.Name);
        if (obj is Size size)
            return size.GetExpressions(expression.GroupName, expression.Name);
        return Enumerable.Empty<IExpression>();
    }

    public static IEnumerable<IExpression> GetDefineEnumerableChildrenExpressions(this IExpression expression)
    {
        var obj = expression.Value;
        if (obj is IEnumerable<Point> points)
        {
            for (var i = 0; i < points.Take(5).Count(); i++)
            {
                var item = points.ElementAt(i);
                var name = $"{expression.Name}[{i}]";
                foreach (var item2 in item.GetExpressions(expression.GroupName, name))
                {
                    yield return item2;
                }
            }
        }
        if (obj is IEnumerable<Size> sizes)
        {
            for (var i = 0; i < sizes.Take(5).Count(); i++)
            {
                var item = sizes.ElementAt(i);
                var name = $"{expression.Name}[{i}]";
                foreach (var item2 in item.GetExpressions(expression.GroupName, name))
                {
                    yield return item2;
                }
            }
        }

        if (obj is IEnumerable<Rect> rects)
        {
            for (var i = 0; i < rects.Take(5).Count(); i++)
            {
                var item = rects.ElementAt(i);
                var name = $"{expression.Name}[{i}]";
                foreach (var item2 in item.GetExpressions(expression.GroupName, name))
                {
                    yield return item2;
                }
            }
        }
    }
    public static IEnumerable<IExpression> GetExpressions(this Point point, string groupName, string parentName)
    {
        yield return new Expression($"{parentName}.X", groupName, point.X);
        yield return new Expression($"{parentName}.Y", groupName, point.Y);
    }

    public static IEnumerable<IExpression> GetExpressions(this Rect rect, string groupName, string parentName)
    {
        var center = rect.GetCenter();
        var centerexpression = new Expression($"{parentName}.中点", groupName, center);
        yield return centerexpression;
        foreach (var item in center.GetExpressions(centerexpression.GroupName, centerexpression.Name))
        {
            yield return item;
        }
        yield return new Expression($"{parentName}.宽", groupName, rect.Width);
        yield return new Expression($"{parentName}.高", groupName, rect.Height);
    }

    public static IEnumerable<IExpression> GetExpressions(this Size size, string groupName, string parentName)
    {
        yield return new Expression($"{parentName}.宽", groupName, size.Width);
        yield return new Expression($"{parentName}.高", groupName, size.Height);
    }

    public static IEnumerable<IExpression> OfType<T>(this IEnumerable<IExpression> expressions)
    {
        return expressions.Where(x => x.DataType == typeof(T).FullName);
    }
}
