// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.Diagram.Presenter.Expressions;

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
        string groupName = expression.ToKey().DisplayName;
        if (obj is Point point)
            return GetExpressions(point, groupName);
        if (obj is Rect rect)
            return GetExpressions(rect, groupName);
        if (obj is Size size)
            return GetExpressions(size, groupName);
        return Enumerable.Empty<IExpression>();
    }

    public static IEnumerable<IExpression> GetDefineEnumerableChildrenExpressions(this IExpression expression)
    {
        var obj = expression.Value;
        string groupName = expression.ToKey().DisplayName;
        if (obj is IEnumerable<Point> points)
        {
            for (var i = 0; i < points.Count(); i++)
            {
                var item = points.ElementAt(i);
                var groupName2 = $"{groupName}[{i}]";
                foreach (var item2 in item.GetExpressions(groupName2))
                {
                    yield return item2;
                }
            }
        }
        if (obj is IEnumerable<Size> sizes)
        {
            for (var i = 0; i < sizes.Count(); i++)
            {
                var item = sizes.ElementAt(i);
                var groupName2 = $"{groupName}[{i}]";
                foreach (var item2 in item.GetExpressions(groupName2))
                {
                    yield return item2;
                }
            }
        }

        if (obj is IEnumerable<Rect> rects)
        {
            for (var i = 0; i < rects.Count(); i++)
            {
                var item = rects.ElementAt(i);
                var groupName2 = $"{groupName}[{i}]";
                foreach (var item2 in item.GetExpressions(groupName2))
                {
                    yield return item2;
                }
            }
        }
    }
    public static IEnumerable<IExpression> GetExpressions(this Point point, string groupName)
    {
        yield return new Expression("X", groupName, point.X);
        yield return new Expression("Y", groupName, point.Y);
    }

    public static IEnumerable<IExpression> GetExpressions(this Rect rect, string groupName)
    {
        var center = rect.GetCenter();
        var centerexpression = new Expression("中点", groupName, center);
        yield return centerexpression;
        foreach (var item in center.GetExpressions(centerexpression.ToKey().DisplayName))
        {
            yield return item;
        }
        yield return new Expression("宽", groupName, rect.Width);
        yield return new Expression("高", groupName, rect.Height);
    }

    public static IEnumerable<IExpression> GetExpressions(this Size size, string groupName)
    {
        yield return new Expression("宽", groupName, size.Width);
        yield return new Expression("高", groupName, size.Height);
    }

    public static IEnumerable<IExpression> OfType<T>(this IEnumerable<IExpression> expressions)
    {
        return expressions.Where(x => x.DataType == typeof(T).FullName);
    }
}
