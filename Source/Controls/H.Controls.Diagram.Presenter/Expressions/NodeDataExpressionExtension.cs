// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.Diagram.Presenter.Expressions;

public static class NodeDataExpressionExtension
{
    public static string GetNodeDataText(this NodeDataExpression expression)
    {
        return expression.Path.Split('.')[0].Trim('[').Trim(']');
    }

    private static string GetPropertyDisplayName(this string path)
    {
        return path.Split('.').Last().Trim('[').Trim(']');
    }

    public static string GetPropertyDisplayName(this NodeDataExpression expression)
    {
        return expression.Path.GetPropertyDisplayName();
    }

    public static int GetLevel(this NodeDataExpression expression)
    {
        return expression.Path.Split('.').Length;
    }
    public static bool IsNodeData(this NodeDataExpression expression)
    {
        return expression.GetLevel() == 2;
    }
    public static bool IsDiagramData(this NodeDataExpression expression)
    {
        return expression.GetLevel() == 1;
    }

    public static NodeDataExpression GetParentExpression(this NodeDataExpression expression)
    {
        var parentPath = expression.Path.Split($".[{expression.Name}]")[0];
        NodeDataExpression result = new NodeDataExpression();
        result.Path = parentPath;
        result.GroupName = expression.GroupName;
        result.Name = parentPath.GetPropertyDisplayName();
        return result;
    }

    public static IEnumerable<NodeDataExpression> GetParentNodeDataExpressions(this NodeDataExpression expression)
    {
        yield return expression;
        if (expression.IsNodeData())
            yield break;
        var pe = expression.GetParentExpression();
        if (pe.IsNodeData())
        {
            yield return pe;
            yield break;
        }
        yield return pe;
        foreach (var item in pe.GetParentNodeDataExpressions())
        {
            yield return item;
        }
    }

    private static bool TryGetExpressionValue(string displayName, object parent, out object value)
    {
        value = null;
        if (displayName == null)
            return false;
        if (parent == null)
            return false;
        var properties = parent.GetType().GetProperties().Where(x => x.CanRead);
        foreach (var property in properties)
        {
            var display = property.GetCustomAttribute<DisplayAttribute>();
            if (display == null)
                continue;
            var expressionableAttribute = property.GetCustomAttribute<ExpressionableAttribute>();
            if (expressionableAttribute == null)
                continue;
            if (display.Name == displayName)
            {
                value = property.GetValue(parent);
                return true;
            }
        }
        return false;
    }

    public static bool TryGetExpressionValue(this NodeDataExpression expression, IDiagramData diagramData, out object value)
    {
        if (expression is IGetableNodeDataExpression getable)
            return getable.TryGetExpressionValue(diagramData, out value);
        value = null;
        if (expression == null)
            return false;
        if (expression.GetLevel() < 1)
            return false;
        if (expression.IsDiagramData())
            return TryGetExpressionValue(expression.GetPropertyDisplayName(), diagramData, out value);
        object parentValue = null;
        var parentExpressions = expression.GetParentNodeDataExpressions().Reverse().ToList();
        foreach (var parentExpression in parentExpressions)
        {
            if (parentExpression.IsNodeData())
            {
                var pr = parentExpression.TryGetRootExpressionValue(diagramData, out parentValue);
                if (!pr)
                    return false;
            }
            else
            {
                object currentValue = null;
                var pr = TryGetExpressionValue(parentExpression.GetPropertyDisplayName(), parentValue, out currentValue);
                if (!pr)
                    return false;
                parentValue = currentValue;
            }
        }
        value = parentValue;
        return true;
    }

    public static bool TryGetRootExpressionValue(this NodeDataExpression expression, IDiagramData diagramData, out object value)
    {
        if (expression is IGetableNodeDataExpression getable)
            return getable.TryGetExpressionValue(diagramData, out value);
        value = null;
        if (expression == null)
            return false;
        var text = expression.GetNodeDataText();
        var displayName = expression.GetPropertyDisplayName();
        var nodeData = diagramData.NodeDatas.OfType<ITextNodeData>().Where(x => x.Text == text).FirstOrDefault();
        if (nodeData == null)
            return false;
        return TryGetExpressionValue(displayName, nodeData, out value);
    }

    public static IEnumerable<NodeDataExpression> GetExpressions(this IExpressionable expressionable, string groupName, string path = null)
    {
        return GetObjExpressions(expressionable, path, groupName);
    }
    private static IEnumerable<NodeDataExpression> GetObjExpressions(object obj, string path, string groupName)
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
            string np = string.IsNullOrEmpty(path) ? $"[{display.Name}]" : $"[{path.TrimStart('[').TrimEnd(']')}].[{display.Name}]";
            var ne = new NodeDataExpression()
            {
                GroupName = groupName,
                Type = property.PropertyType.FullName,
                Name = display.Name,
                Path = np
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
            var nes = GetObjExpressions(value, ne.Path, groupName);
            foreach (var item in nes)
            {
                yield return item;
            }
        }
    }
}
