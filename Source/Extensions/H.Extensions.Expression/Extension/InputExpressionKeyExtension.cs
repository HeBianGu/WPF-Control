// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Extensions.Expressions.Keys;
using System.Windows;

namespace H.Extensions.Expressions.Base;

public static class InputExpressionKeyExtension
{
    public static (bool success, T value) GetInputExpressionValue<T>(this IInputExpressionKey expressionKey, IEnumerable<IInputExpression> expressions)
    {
        if (expressionKey is IInputStringExpressionKey primitiveExpression)
        {
            var (success, value) = primitiveExpression.TryParse<T>();
            if (success)
                return (true, value);
        }

        var r = expressionKey.GetInputExpressionValue(expressions);
        if (!r.success)
            return (false, default);
        if (r.value is T tValue)
            return (true, tValue);
        return (false, default);
    }


    public static (bool success, object value) GetInputExpressionValue(this IInputExpressionKey expressionKey, IEnumerable<IInputExpression> expressions)
    {
        if (expressionKey is IInputStringExpressionKey primitiveExpression)
            return (true, primitiveExpression.Value);
        if (expressionKey == null)
            return (false, default);
        var r = expressionKey.GetInputExpressions(expressions).FirstOrDefault();
        if (r == null)
            return (false, default);
        return (true, r.Value);
    }

    public static (bool success, object value) GetInputValue(this IInputExpressionKey expressionKey, IEnumerable<IInputExpression> expressions)
    {
        return expressionKey.GetInputExpressionValue(expressions);
    }

    public static (bool success, T value) GetInputValue<T>(this IInputExpressionKey expressionKey, IEnumerable<IInputExpression> expressions)
    {
        return expressionKey.GetInputExpressionValue<T>(expressions);
    }

    public static (bool success, T value) GetInputValue<T>(this IInputExpressionKey expressionKey, IGetFromInputExpressionKeysable getFromExpressionKeysable)
    {
        return expressionKey.GetInputExpressionValue<T>(getFromExpressionKeysable.GetFromInputExpressions());
    }

    public static (bool success, T value) GetExpressionValue<T>(this IGetFromInputExpressionKeysable getFromExpressionKeysable, IInputExpressionKey expressionKey)
    {
        return expressionKey.GetInputValue<T>(getFromExpressionKeysable);
    }

    public static (bool success, T value) GetInputValue<T>(this IGetInputExpressionsable getExpressionsable, IInputExpressionKey expressionKey)
    {
        if (getExpressionsable == null)
            return (false, default);
        return expressionKey.GetInputExpressionValue<T>(getExpressionsable.GetInputExpressions());
    }

    public static IEnumerable<IInputExpression> GetFromInputExpressions(this IGetFromInputExpressionKeysable getableExpression, IInputExpressionKey expressionKey)
    {
        var expressions = getableExpression.GetFromInputExpressions();
        return expressionKey.GetInputExpressions(expressions);
    }

    public static IEnumerable<IInputExpression> GetInputExpressions(this IInputExpressionKey expressionKey, IEnumerable<IInputExpression> expressions)
    {
        return expressions.Where(x => x.ToKey().Equals(expressionKey));
    }

    public static IInputExpression GetFromInputExpression(this IGetFromInputExpressionKeysable getableExpression, IInputExpressionKey expressionKey)
    {
        return getableExpression.GetFromInputExpressions(expressionKey).FirstOrDefault();
    }

    public static IEnumerable<IInputExpressionKey> GetFromInputExpressionKeys(this IGetFromInputExpressionKeysable getableExpression, Predicate<IInputExpression> predicate = null)
    {
        return getableExpression.GetFromInputExpressions().Where(x => predicate == null || predicate(x)).Select(x => x.ToKey());
    }

    public static IEnumerable<IInputExpressionKey> GetFromInputExpressionExtensionKeys<T>(this IGetFromInputExpressionKeysable getableExpression) => getableExpression.GetFromInputExpressionKeys(x => x.DataType == typeof(T).FullName);

    public static IEnumerable<IInputExpressionKey> GetIntFromInputExpressionKeys(this IGetFromInputExpressionKeysable getableExpression) => getableExpression.GetFromInputExpressionExtensionKeys<int>();
    public static IEnumerable<IInputExpressionKey> GetDoubleFromInputExpressionKeys(this IGetFromInputExpressionKeysable getableExpression) => getableExpression.GetFromInputExpressionExtensionKeys<double>();
    public static IEnumerable<IInputExpressionKey> GetFloatFromInputExpressionKeys(this IGetFromInputExpressionKeysable getableExpression) => getableExpression.GetFromInputExpressionExtensionKeys<float>();
    public static IEnumerable<IInputExpressionKey> GetStringFromInputExpressionKeys(this IGetFromInputExpressionKeysable getableExpression) => getableExpression.GetFromInputExpressionExtensionKeys<string>();

    public static IEnumerable<IInputExpressionKey> GetPrimitiveFromInputExpressionKeys(this IGetFromInputExpressionKeysable getableExpression) => getableExpression.GetFromInputExpressionExtensionKeys<string>().Concat(getableExpression.GetFromInputExpressionExtensionKeys<float>()).Concat(getableExpression.GetFromInputExpressionExtensionKeys<double>()).Concat(getableExpression.GetFromInputExpressionExtensionKeys<int>());
    public static IEnumerable<IInputExpressionKey> GetBoolFromInputExpressionKeys(this IGetFromInputExpressionKeysable getableExpression) => getableExpression.GetFromInputExpressionExtensionKeys<bool>();
    public static IEnumerable<IInputExpressionKey> GetUIntFromInputExpressionKeys(this IGetFromInputExpressionKeysable getableExpression) => getableExpression.GetFromInputExpressionExtensionKeys<uint>();
    public static IEnumerable<IInputExpressionKey> GetRectFromInputExpressionKeys(this IGetFromInputExpressionKeysable getableExpression) => getableExpression.GetFromInputExpressionExtensionKeys<Rect>();
    public static IEnumerable<IInputExpressionKey> GetPointFromInputExpressionKeys(this IGetFromInputExpressionKeysable getableExpression) => getableExpression.GetFromInputExpressionExtensionKeys<Point>();

    public static IEnumerable<IInputExpressionKey> GetPointssFromInputExpressionKeys(this IGetFromInputExpressionKeysable getableExpression) => getableExpression.GetFromInputExpressionExtensionKeys<Point[][]>();
    public static IEnumerable<IInputExpressionKey> GetSizeFromInputExpressioKeys(this IGetFromInputExpressionKeysable getableExpression) => getableExpression.GetFromInputExpressionExtensionKeys<Size>();
}
