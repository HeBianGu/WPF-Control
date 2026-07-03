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

public interface IGetExpressionKeysable
{
    IEnumerable<IExpression> GetFromExpressions();
}

public static class IGetExpressionKeysableExtensions
{
    public static (bool success, T value) GetExpressionValue<T>(this IGetExpressionKeysable getableExpression, IExpressionKey expressionKey)
    {
        if (expressionKey is IInputStringExpressionKey primitiveExpression)
        {
            var (success, value) = primitiveExpression.TryParse<T>();
            if (success)
                return (true, value);
        }
        var r = getableExpression.GetExpressionValue(expressionKey);
        if (!r.success)
            return (false, default);
        if (r.value is T tValue)
            return (true, tValue);
        return (false, default);
    }

    public static (bool success, object value) GetExpressionValue(this IGetExpressionKeysable getableExpression, IExpressionKey expressionKey)
    {
        if (expressionKey is IInputStringExpressionKey primitiveExpression)
            return (true, primitiveExpression.Value);
        var r = getableExpression.GetFromExpression(expressionKey);
        return (true, r.Value);
    }

    public static IEnumerable<IExpression> GetFromExpressions(this IGetExpressionKeysable getableExpression, IExpressionKey expressionKey)
    {
        return getableExpression.GetFromExpressions().Where(x => x.ToKey().Equals(expressionKey));
    }

    public static IExpression GetFromExpression(this IGetExpressionKeysable getableExpression, IExpressionKey expressionKey)
    {
        return getableExpression.GetFromExpressions(expressionKey).FirstOrDefault();
    }

    public static IEnumerable<IExpressionKey> GetFromExpressionKeys(this IGetExpressionKeysable getableExpression, Predicate<IExpression> predicate = null)
    {
        return getableExpression.GetFromExpressions().Where(x => predicate == null || predicate(x)).Select(x => x.ToKey());
    }

    public static IEnumerable<IExpressionKey> GetFromExpressionKeys<T>(this IGetExpressionKeysable getableExpression) => getableExpression.GetFromExpressionKeys(x => x.DataType == typeof(T).FullName);

    public static IEnumerable<IExpressionKey> GetIntFromExpressionKeys(this IGetExpressionKeysable getableExpression) => getableExpression.GetFromExpressionKeys<int>();
    public static IEnumerable<IExpressionKey> GetDoubleFromExpressionKeys(this IGetExpressionKeysable getableExpression) => getableExpression.GetFromExpressionKeys<double>();
    public static IEnumerable<IExpressionKey> GetFloatFromExpressionKeys(this IGetExpressionKeysable getableExpression) => getableExpression.GetFromExpressionKeys<float>();
    public static IEnumerable<IExpressionKey> GetStringFromExpressionKeys(this IGetExpressionKeysable getableExpression) => getableExpression.GetFromExpressionKeys<string>();

    public static IEnumerable<IExpressionKey> GetPrimitiveFromExpressionKeys(this IGetExpressionKeysable getableExpression) => getableExpression.GetFromExpressionKeys<string>().Concat(getableExpression.GetFromExpressionKeys<float>()).Concat(getableExpression.GetFromExpressionKeys<double>()).Concat(getableExpression.GetFromExpressionKeys<int>());
    public static IEnumerable<IExpressionKey> GetBoolFromExpressionKeys(this IGetExpressionKeysable getableExpression) => getableExpression.GetFromExpressionKeys<bool>();
    public static IEnumerable<IExpressionKey> GetUIntFromExpressionKeys(this IGetExpressionKeysable getableExpression) => getableExpression.GetFromExpressionKeys<uint>();
    public static IEnumerable<IExpressionKey> GetRectFromExpressionKeys(this IGetExpressionKeysable getableExpression) => getableExpression.GetFromExpressionKeys<Rect>();
    public static IEnumerable<IExpressionKey> GetPointFromExpressionKeys(this IGetExpressionKeysable getableExpression) => getableExpression.GetFromExpressionKeys<Point>();

    public static IEnumerable<IExpressionKey> GetPointssFromExpressionKeys(this IGetExpressionKeysable getableExpression) => getableExpression.GetFromExpressionKeys<Point[][]>();
    public static IEnumerable<IExpressionKey> GetSizeFromExpressioKeyns(this IGetExpressionKeysable getableExpression) => getableExpression.GetFromExpressionKeys<Size>();
}
