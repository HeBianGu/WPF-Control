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

public interface IExpressionable
{
    /// <summary>
    /// 获取表达式
    /// </summary>
    IEnumerable<IExpressionKey> GetExpressions(Predicate<object> predicate = null);
    ///// <summary>
    ///// 尝试获取表达式值
    ///// </summary>
    //bool TryGetExpressionValue(IExpressionKey expression, out object value);
}

public interface IDefaultValueExpressionable
{
    IEnumerable<IExpressionKey> GetDefaultValueExpressions(Predicate<object> predicate = null);
}

//public static class ExpressionableExtension
//{
//    public static (bool success, T value) GetExpressionValue<T>(this IExpressionable expressionable, IExpressionKey expressionKey)
//    {
//        var r = expressionable.GetExpressionValue(expressionKey);
//        if (!r.success)
//            return (false, default);
//        if (r.value is T tValue)
//            return (true, tValue);
//        return (false, default);
//    }

//    public static (bool success, object value) GetExpressionValue(this IExpressionable expressionable, IExpressionKey expressionKey)
//    {
//        var key = expressionable.GetExpressions().FirstOrDefault(x => x == expressionKey);
//        if (key == null)
//            return (false, default);
//        return (true, key.Value);
//    }

//}
