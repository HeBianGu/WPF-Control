// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Modules.Plugin.Base;

[AttributeUsage(AttributeTargets.Assembly, Inherited = false, AllowMultiple = false)]
public sealed class PluginAttribute : Attribute
{
    public PluginAttribute()
    {

    }

    public PluginAttribute(string name, string groupName = null, string decription = null)
    {
        Name = name;
        GroupName = groupName;
        Description = decription;
    }

    public string Name { get; set; }

    public string GroupName { get; set; }

    public string Description { get; set; }
}
