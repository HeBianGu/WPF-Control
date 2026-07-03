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

public interface IGetFromExpressionKeysable
{
    IEnumerable<IExpression> GetFromExpressions();
}

public static class IGetFromExpressionKeysableExtensions
{
    public static (bool success, T value) GetExpressionValue<T>(this IGetFromExpressionKeysable getableExpression, IExpressionKey expressionKey)
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

    public static (bool success, object value) GetExpressionValue(this IGetFromExpressionKeysable getableExpression, IExpressionKey expressionKey)
    {
        if (expressionKey is IInputStringExpressionKey primitiveExpression)
            return (true, primitiveExpression.Value);
        var r = getableExpression.GetFromExpression(expressionKey);
        return (true, r.Value);
    }

    public static IEnumerable<IExpression> GetFromExpressions(this IGetFromExpressionKeysable getableExpression, IExpressionKey expressionKey)
    {
        return getableExpression.GetFromExpressions().Where(x => x.ToKey().Equals(expressionKey));
    }

    public static IExpression GetFromExpression(this IGetFromExpressionKeysable getableExpression, IExpressionKey expressionKey)
    {
        return getableExpression.GetFromExpressions(expressionKey).FirstOrDefault();
    }

    public static IEnumerable<IExpressionKey> GetFromExpressionKeys(this IGetFromExpressionKeysable getableExpression, Predicate<IExpression> predicate = null)
    {
        return getableExpression.GetFromExpressions().Where(x => predicate == null || predicate(x)).Select(x => x.ToKey());
    }

    public static IEnumerable<IExpressionKey> GetFromExpressionKeys<T>(this IGetFromExpressionKeysable getableExpression) => getableExpression.GetFromExpressionKeys(x => x.DataType == typeof(T).FullName);

    public static IEnumerable<IExpressionKey> GetIntFromExpressionKeys(this IGetFromExpressionKeysable getableExpression) => getableExpression.GetFromExpressionKeys<int>();
    public static IEnumerable<IExpressionKey> GetDoubleFromExpressionKeys(this IGetFromExpressionKeysable getableExpression) => getableExpression.GetFromExpressionKeys<double>();
    public static IEnumerable<IExpressionKey> GetFloatFromExpressionKeys(this IGetFromExpressionKeysable getableExpression) => getableExpression.GetFromExpressionKeys<float>();
    public static IEnumerable<IExpressionKey> GetStringFromExpressionKeys(this IGetFromExpressionKeysable getableExpression) => getableExpression.GetFromExpressionKeys<string>();

    public static IEnumerable<IExpressionKey> GetPrimitiveFromExpressionKeys(this IGetFromExpressionKeysable getableExpression) => getableExpression.GetFromExpressionKeys<string>().Concat(getableExpression.GetFromExpressionKeys<float>()).Concat(getableExpression.GetFromExpressionKeys<double>()).Concat(getableExpression.GetFromExpressionKeys<int>());
    public static IEnumerable<IExpressionKey> GetBoolFromExpressionKeys(this IGetFromExpressionKeysable getableExpression) => getableExpression.GetFromExpressionKeys<bool>();
    public static IEnumerable<IExpressionKey> GetUIntFromExpressionKeys(this IGetFromExpressionKeysable getableExpression) => getableExpression.GetFromExpressionKeys<uint>();
    public static IEnumerable<IExpressionKey> GetRectFromExpressionKeys(this IGetFromExpressionKeysable getableExpression) => getableExpression.GetFromExpressionKeys<Rect>();
    public static IEnumerable<IExpressionKey> GetPointFromExpressionKeys(this IGetFromExpressionKeysable getableExpression) => getableExpression.GetFromExpressionKeys<Point>();

    public static IEnumerable<IExpressionKey> GetPointssFromExpressionKeys(this IGetFromExpressionKeysable getableExpression) => getableExpression.GetFromExpressionKeys<Point[][]>();
    public static IEnumerable<IExpressionKey> GetSizeFromExpressioKeyns(this IGetFromExpressionKeysable getableExpression) => getableExpression.GetFromExpressionKeys<Size>();
}
