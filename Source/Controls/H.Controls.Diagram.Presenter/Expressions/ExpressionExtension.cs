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

    public static IEnumerable<IExpression> OfType<T>(this IEnumerable<IExpression> expressions)
    {
        return expressions.Where(x => x.DataType == typeof(T).FullName);
    }
}
