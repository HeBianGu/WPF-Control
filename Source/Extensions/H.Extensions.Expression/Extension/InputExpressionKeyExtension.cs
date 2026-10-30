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
    public static (bool success, T value) GetInputExpressionValue<T>(this IInputExpressionKey key, IEnumerable<IInputExpression> exps)
    {
        if (key is IInputStringExpressionKey primitiveExpression)
        {
            var (success, value) = primitiveExpression.TryParse<T>();
            if (success)
                return (true, value);
        }

        var r = key.GetInputExpressionValue(exps);
        if (!r.success)
            return (false, default);
        if (r.value is T tValue)
            return (true, tValue);
        return (false, default);
    }


    public static (bool success, object value) GetInputExpressionValue(this IInputExpressionKey key, IEnumerable<IInputExpression> exps)
    {
        if (key is IInputStringExpressionKey primitiveExpression)
            return (true, primitiveExpression.Value);
        if (key == null)
            return (false, default);
        var r = key.GetInputExpressions(exps).FirstOrDefault();
        if (r == null)
            return (false, default);
        return (true, r.Value);
    }

    public static (bool success, object value) GetInputValue(this IInputExpressionKey key, IEnumerable<IInputExpression> exps)
    {
        return key.GetInputExpressionValue(exps);
    }

    public static (bool success, T value) GetInputValue<T>(this IInputExpressionKey key, IEnumerable<IInputExpression> exps)
    {
        return key.GetInputExpressionValue<T>(exps);
    }

    public static (bool success, T value) GetInputValue<T>(this IInputExpressionKey key, IGetFromInputExpressionKeysable fkeys)
    {
        var froms = fkeys.GetFromInputExpressions();
        if (fkeys is IGetInputExpressionsable keys)
            froms = froms.Concat(keys.GetInputExpressions());
        return key.GetInputExpressionValue<T>(froms);
    }

    public static (bool success, T value) GetExpressionValue<T>(this IGetFromInputExpressionKeysable fexp, IInputExpressionKey key)
    {
        return key.GetInputValue<T>(fexp);
    }

    public static (bool success, T value) GetInputValue<T>(this IGetInputExpressionsable gexp, IInputExpressionKey key)
    {
        if (gexp == null)
            return (false, default);
        return key.GetInputExpressionValue<T>(gexp.GetInputExpressions());
    }

    public static IEnumerable<IInputExpression> GetExpressions(this IGetFromInputExpressionKeysable gexp, IInputExpressionKey key)
    {
        var exps = gexp.GetFromInputExpressions();
        return key.GetInputExpressions(exps);
    }

    public static IEnumerable<IInputExpression> GetInputExpressions(this IInputExpressionKey key, IEnumerable<IInputExpression> exps)
    {
        return exps.Where(x => x.ToKey().Equals(key));
    }

    public static IInputExpression GetExpression(this IGetFromInputExpressionKeysable gexp, IInputExpressionKey key)
    {
        return gexp.GetExpressions(key).FirstOrDefault();
    }

    public static IEnumerable<IInputExpressionKey> GetFromKeys(this IGetFromInputExpressionKeysable gexp, Predicate<IInputExpression> predicate = null)
    {
        return gexp.GetFromInputExpressions().Where(x => predicate == null || predicate(x)).Select(x => x.ToKey());
    }

    public static IEnumerable<IInputExpressionKey> GetKeys(this IGetInputExpressionsable gexp, Predicate<IInputExpression> predicate = null)
    {
        return gexp.GetInputExpressions().Where(x => predicate == null || predicate(x)).Select(x => x.ToKey());
    }


    public static IEnumerable<IInputExpressionKey> GetFromKeys<T>(this IGetFromInputExpressionKeysable fexp, bool cthis = false)
    {
        var fkeys = fexp.GetFromKeys(x => x.DataType == typeof(T).FullName);
        if (cthis && fexp is IGetInputExpressionsable gexp)
            fkeys = fkeys.Concat(gexp.GetKeys(x => x.DataType == typeof(T).FullName));
        return fkeys;
    }

    public static IEnumerable<IInputExpressionKey> GetIntFromKeys(this IGetFromInputExpressionKeysable gexp) => gexp.GetFromKeys<int>();
    public static IEnumerable<IInputExpressionKey> GetDoubleFromKeys(this IGetFromInputExpressionKeysable gexp) => gexp.GetFromKeys<double>();
    public static IEnumerable<IInputExpressionKey> GetFloatFromKeys(this IGetFromInputExpressionKeysable gexp) => gexp.GetFromKeys<float>();
    public static IEnumerable<IInputExpressionKey> GetStringFromKeys(this IGetFromInputExpressionKeysable gexp) => gexp.GetFromKeys<string>();

    public static IEnumerable<IInputExpressionKey> GetPrimitiveFromKeys(this IGetFromInputExpressionKeysable gexp) => gexp.GetFromKeys<string>().Concat(gexp.GetFromKeys<float>()).Concat(gexp.GetFromKeys<double>()).Concat(gexp.GetFromKeys<int>());
    public static IEnumerable<IInputExpressionKey> GetBoolFromKeys(this IGetFromInputExpressionKeysable gexp) => gexp.GetFromKeys<bool>();
    public static IEnumerable<IInputExpressionKey> GetUIntFromKeys(this IGetFromInputExpressionKeysable gexp) => gexp.GetFromKeys<uint>();
    public static IEnumerable<IInputExpressionKey> GetRectFromKeys(this IGetFromInputExpressionKeysable gexp) => gexp.GetFromKeys<Rect>();
    public static IEnumerable<IInputExpressionKey> GetPointFromKeys(this IGetFromInputExpressionKeysable gexp) => gexp.GetFromKeys<Point>();

    public static IEnumerable<IInputExpressionKey> GetPointssFromKeys(this IGetFromInputExpressionKeysable gexp) => gexp.GetFromKeys<Point[][]>();
    public static IEnumerable<IInputExpressionKey> GetSizeFromKeys(this IGetFromInputExpressionKeysable gexp) => gexp.GetFromKeys<Size>();
}
