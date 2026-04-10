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
    IEnumerable<NodeDataExpression> GetExpressions(Predicate<object> predicate = null);
    /// <summary>
    /// 尝试获取表达式值
    /// </summary>
    bool TryGetExpressionValue(NodeDataExpression expression, out object value);
}

public interface IDefaultValueExpressionable
{
    IEnumerable<NodeDataExpression> GetDefaultValueExpressions(Predicate<object> predicate = null);
}
