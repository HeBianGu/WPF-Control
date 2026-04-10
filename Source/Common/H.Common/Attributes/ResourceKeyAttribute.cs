// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Common.Attributes;

public class ResourceKeyAttribute : Attribute
{
    public ResourceKeyAttribute()
    {
        
    }
    public ResourceKeyAttribute(string key)
    {
        this.Key = key;
    }
    public string Key { get; private set; }
}
