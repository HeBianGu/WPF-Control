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

public interface IGetFromExpressionsable
{
    IEnumerable<IExpressionKey> GetFromExpressions<T>();
}

public static class GetableExpressionExtensions
{
    public static (bool success, T value) GetExpressionValue<T>(this IGetFromExpressionsable getableExpression, IExpressionKey expressionKey)
    {
        var r = getableExpression.GetFromExpressions<T>().FirstOrDefault(x => x.Equals(expressionKey));
        if (r == null)
            return (false, default);
        if (r.Value is T tValue)
            return (true, tValue);
        return (false, default);
    }
}
