// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Common;
using System.Resources;

namespace H.Extensions.Mvvm.ViewModels.Base;

public abstract class ResxDisplayBindableBase : DisplayBindableBase
{

    const string resxFormat = "  <data name=\"{0}\" xml:space=\"preserve\">\r\n    <value>{1}</value>\r\n  </data>";
    public ResxDisplayBindableBase()
    {
        this.UpdateResx();
    }

    protected virtual void UpdateResx()
    {
        string rname = this.GetType().GetNameResx(this.Name);
        string rgroup = this.GetType().GetGroupNameResx(this.GroupName);
        string rdesc = this.GetType().GetDescriptionResx(this.Description);
        this.Name = rname ?? this.Name;
        this.GroupName = rgroup ?? this.GroupName;
        this.Description = rdesc ?? this.Description;
    }
}
