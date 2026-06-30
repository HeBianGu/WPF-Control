// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Extensions.ApplicationBase;

public abstract class CacheActionOptionsBase
{
    protected List<object> CacheActionOptions { get; } = new List<object>();
    public Action<T> GetConfigOptions<T>()
    {
        return this.CacheActionOptions.Where(x => x.GetType() == typeof(Action<T>)).FirstOrDefault() as Action<T>;
    }

    protected void ConfigOptions<T>(Action<T> action)
    {
        this.CacheActionOptions.Add(action);
    }
}
