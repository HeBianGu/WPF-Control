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

using H.Extensions.Expressions.Keys;
using System.Windows;

namespace H.Extensions.Expressions.Base;

public static class IGetFromExpressionKeysableExtensions
{
    public static (bool success, T value) GetExpressionValue<T>(this IExpressionKey expressionKey, IEnumerable<IExpression> expressions)
    {
        if (expressionKey is IInputStringExpressionKey primitiveExpression)
        {
            var (success, value) = primitiveExpression.TryParse<T>();
            if (success)
                return (true, value);
        }

        var r = expressionKey.GetExpressionValue(expressions);
        if (!r.success)
            return (false, default);
        if (r.value is T tValue)
            return (true, tValue);
        return (false, default);
    }


    public static (bool success, object value) GetExpressionValue(this IExpressionKey expressionKey, IEnumerable<IExpression> expressions)
    {
        if (expressionKey is IInputStringExpressionKey primitiveExpression)
            return (true, primitiveExpression.Value);
        if (expressionKey == null)
            return (false, default);
        var r = expressionKey.GetExpressions(expressions).FirstOrDefault();
        if (r == null)
            return (false, default);
        return (true, r.Value);
    }

    public static (bool success, object value) GetValue(this IExpressionKey expressionKey, IEnumerable<IExpression> expressions)
    {
        return expressionKey.GetExpressionValue(expressions);
    }

    public static (bool success, T value) GetValue<T>(this IExpressionKey expressionKey, IEnumerable<IExpression> expressions)
    {
        return expressionKey.GetExpressionValue<T>(expressions);
    }

    public static (bool success, T value) GetValue<T>(this IExpressionKey expressionKey, IGetFromExpressionKeysable getFromExpressionKeysable)
    {
        return expressionKey.GetExpressionValue<T>(getFromExpressionKeysable.GetFromExpressions());
    }

    public static (bool success, T value) GetExpressionValue<T>(this IGetFromExpressionKeysable getFromExpressionKeysable, IExpressionKey expressionKey)
    {
        return expressionKey.GetValue<T>(getFromExpressionKeysable);
    }

    public static (bool success, T value) GetValue<T>(this IGetExpressionsable getExpressionsable, IExpressionKey expressionKey)
    {
        if (getExpressionsable == null)
            return (false, default);
        return expressionKey.GetExpressionValue<T>(getExpressionsable.GetExpressions());
    }

    public static IEnumerable<IExpression> GetFromExpressions(this IGetFromExpressionKeysable getableExpression, IExpressionKey expressionKey)
    {
        var expressions = getableExpression.GetFromExpressions();
        return expressionKey.GetExpressions(expressions);
    }

    public static IEnumerable<IExpression> GetExpressions(this IExpressionKey expressionKey, IEnumerable<IExpression> expressions)
    {
        return expressions.Where(x => x.ToKey().Equals(expressionKey));
    }

    public static IExpression GetFromExpression(this IGetFromExpressionKeysable getableExpression, IExpressionKey expressionKey)
    {
        return getableExpression.GetFromExpressions(expressionKey).FirstOrDefault();
    }

    public static IEnumerable<IExpressionKey> GetFromExpressionKeys(this IGetFromExpressionKeysable getableExpression, Predicate<IExpression> predicate = null)
    {
        return getableExpression.GetFromExpressions().Where(x => predicate == null || predicate(x)).Select(x => x.ToKey());
    }

    public static IEnumerable<IExpressionKey> GetFromExpressionExtensionKeys<T>(this IGetFromExpressionKeysable getableExpression) => getableExpression.GetFromExpressionKeys(x => x.DataType == typeof(T).FullName);

    public static IEnumerable<IExpressionKey> GetIntFromExpressionKeys(this IGetFromExpressionKeysable getableExpression) => getableExpression.GetFromExpressionExtensionKeys<int>();
    public static IEnumerable<IExpressionKey> GetDoubleFromExpressionKeys(this IGetFromExpressionKeysable getableExpression) => getableExpression.GetFromExpressionExtensionKeys<double>();
    public static IEnumerable<IExpressionKey> GetFloatFromExpressionKeys(this IGetFromExpressionKeysable getableExpression) => getableExpression.GetFromExpressionExtensionKeys<float>();
    public static IEnumerable<IExpressionKey> GetStringFromExpressionKeys(this IGetFromExpressionKeysable getableExpression) => getableExpression.GetFromExpressionExtensionKeys<string>();

    public static IEnumerable<IExpressionKey> GetPrimitiveFromExpressionKeys(this IGetFromExpressionKeysable getableExpression) => getableExpression.GetFromExpressionExtensionKeys<string>().Concat(getableExpression.GetFromExpressionExtensionKeys<float>()).Concat(getableExpression.GetFromExpressionExtensionKeys<double>()).Concat(getableExpression.GetFromExpressionExtensionKeys<int>());
    public static IEnumerable<IExpressionKey> GetBoolFromExpressionKeys(this IGetFromExpressionKeysable getableExpression) => getableExpression.GetFromExpressionExtensionKeys<bool>();
    public static IEnumerable<IExpressionKey> GetUIntFromExpressionKeys(this IGetFromExpressionKeysable getableExpression) => getableExpression.GetFromExpressionExtensionKeys<uint>();
    public static IEnumerable<IExpressionKey> GetRectFromExpressionKeys(this IGetFromExpressionKeysable getableExpression) => getableExpression.GetFromExpressionExtensionKeys<Rect>();
    public static IEnumerable<IExpressionKey> GetPointFromExpressionKeys(this IGetFromExpressionKeysable getableExpression) => getableExpression.GetFromExpressionExtensionKeys<Point>();

    public static IEnumerable<IExpressionKey> GetPointssFromExpressionKeys(this IGetFromExpressionKeysable getableExpression) => getableExpression.GetFromExpressionExtensionKeys<Point[][]>();
    public static IEnumerable<IExpressionKey> GetSizeFromExpressioKeyns(this IGetFromExpressionKeysable getableExpression) => getableExpression.GetFromExpressionExtensionKeys<Size>();
}
