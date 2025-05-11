// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Collections;

namespace H.Extensions.Tree;

public class ClassTypeTree : ITree, IParent
{
    private readonly Type _type;
    public ClassTypeTree(Type type)
    {
        this._type = type;
    }

    public IEnumerable GetChildren(object parent)
    {
        if (parent == null)
        {
            return new Type[] { this._type };
        }
        if (parent is Type type)
        {
            return type.Assembly.GetTypes().Where(x => x.BaseType == type);
        }
        return Enumerable.Empty<string>();
    }

    public object GetParent(object current)
    {
        if (current is Type type)
            return type.BaseType;
        return null;
    }
}
