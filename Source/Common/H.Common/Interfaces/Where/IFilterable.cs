// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System;

namespace H.Common.Interfaces.Where;

public interface IFilterable
{
    bool IsMatch(object obj);
}

public class PredicateFilter : IFilterable
{
    private readonly Predicate<object> _predicate;
    public PredicateFilter(Predicate<object> predicate)
    {
        _predicate = predicate;
    }

    public bool IsMatch(object obj)
    {
        if (this._predicate == null)
            return true;
        return this._predicate(obj);
    }
}

public class PredicateFilter<T> : IFilterable
{
    private readonly Predicate<T> _predicate;
    public PredicateFilter(Predicate<T> predicate)
    {
        _predicate = predicate;
    }

    public bool IsMatch(T obj)
    {
        if (this._predicate == null)
            return true;
        return this._predicate(obj);
    }

    public bool IsMatch(object obj)
    {
        if (obj is T t)
            return this.IsMatch(t);
        return false;
    }
}
