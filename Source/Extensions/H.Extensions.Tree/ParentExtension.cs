// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Extensions.Tree;

public static class ParentExtension
{
    public static IEnumerable<object> GetParent(this IParent tree, object current)
    {
        List<object> result = new List<object>();
        Action<object> getParent = null;
        getParent = x =>
         {
             object parent = tree.GetParent(x);
             result.Add(parent);
             if (parent != null)
                 getParent(parent);
         };
        getParent(current);
        return result;
    }
}
