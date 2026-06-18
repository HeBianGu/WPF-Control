// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

global using H.Common.Attributes;
global using H.Common.Interfaces;
global using System.ComponentModel.DataAnnotations;
global using System.Reflection;

namespace H.Common.Commands;

public abstract class DisplayMarkupCommandBase : AsyncMarkupCommandBase, IIconable, INameable, IDescriptionable, IGroupable, IOrderable
{
    protected DisplayMarkupCommandBase()
    {
        DisplayAttribute d = this.GetType().GetCustomAttribute<DisplayAttribute>();
        if (d != null)
        {
            this.Name = d.Name;
            this.Description = d.Description;
            this.GroupName = d?.GroupName;
            this.Order = d?.GetOrder() ?? 0;
        }

        IconAttribute icon = this.GetType().GetCustomAttribute<IconAttribute>();
        this.Icon = icon?.Icon;
    }
    public string Name { get; set; }
    public string Icon { get; set; }
    public string Description { get; set; }
    public string GroupName { get; set; }
    public int Order { get; set; }
}
