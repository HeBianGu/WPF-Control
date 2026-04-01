// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Resources;

namespace H.Common.Commands;

public abstract class ResxDisplayMarkupCommandBase : DisplayMarkupCommandBase
{
    public ResxDisplayMarkupCommandBase()
    {
        this.UpdateResx();
    }
    protected void UpdateResx()
    {
        string rname = this.GetType().GetNameResx(); ;
        string rdesc = this.GetType().GetDescriptionResx();
        this.Name = rname ?? this.Name;
        this.Description = rdesc ?? this.Description;
    }
}
