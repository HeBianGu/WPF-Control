// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")


// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.Diagram.Presenter.Expressions;

public interface IFromExpressionSource
{
    IEnumerable<IExpressionKey> GetFromExpressions<T>();
}

public static class GetableExpressionExtensions
{
    public static (bool success, T value) GetExpressionValue<T>(this IFromExpressionSource getableExpression, IExpressionKey expressionKey)
    {
        if (expressionKey is IInputStringExpressionKey primitiveExpression)
        {
            var (success, value) = primitiveExpression.TryParse<T>();
            if (success)
                return (true, value);
        }
        var r = getableExpression.GetFromExpressions<T>().FirstOrDefault(x => x.Equals(expressionKey));
        if (r == null)
            return (false, default);
        if (r.Value is T tValue)
            return (true, tValue);
        return (false, default);
    }

    public static IEnumerable<IExpressionKey> GetIntFromExpressions(this IFromExpressionSource getableExpression) => getableExpression.GetFromExpressions<int>();
    public static IEnumerable<IExpressionKey> GetDoubleFromExpressions(this IFromExpressionSource getableExpression) => getableExpression.GetFromExpressions<double>();
    public static IEnumerable<IExpressionKey> GetFloatFromExpressions(this IFromExpressionSource getableExpression) => getableExpression.GetFromExpressions<float>();
    public static IEnumerable<IExpressionKey> GetStringFromExpressions(this IFromExpressionSource getableExpression) => getableExpression.GetFromExpressions<string>();

    public static IEnumerable<IExpressionKey> GetPrimitiveFromExpressions(this IFromExpressionSource getableExpression) => getableExpression.GetFromExpressions<string>().Concat(getableExpression.GetFromExpressions<float>()).Concat(getableExpression.GetFromExpressions<double>()).Concat(getableExpression.GetFromExpressions<int>());
    public static IEnumerable<IExpressionKey> GetBoolFromExpressions(this IFromExpressionSource getableExpression) => getableExpression.GetFromExpressions<bool>();
    public static IEnumerable<IExpressionKey> GetUIntFromExpressions(this IFromExpressionSource getableExpression) => getableExpression.GetFromExpressions<uint>();
    public static IEnumerable<IExpressionKey> GetRectFromExpressions(this IFromExpressionSource getableExpression) => getableExpression.GetFromExpressions<Rect>();
    public static IEnumerable<IExpressionKey> GetPointFromExpressions(this IFromExpressionSource getableExpression) => getableExpression.GetFromExpressions<Point>();

    public static IEnumerable<IExpressionKey> GetPointssFromExpressions(this IFromExpressionSource getableExpression) => getableExpression.GetFromExpressions<Point[][]>();
    public static IEnumerable<IExpressionKey> GetSizeFromExpressions(this IFromExpressionSource getableExpression) => getableExpression.GetFromExpressions<Size>();
}
