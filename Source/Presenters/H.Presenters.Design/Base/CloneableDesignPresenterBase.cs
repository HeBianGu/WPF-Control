// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

global using H.Extensions.Common;
global using H.Extensions.Mvvm.ViewModels.Base;
global using System.Reflection;
using H.Common.Interfaces;
using H.Extensions.Mvvm.ViewModels;

namespace H.Presenters.Design.Base;

public abstract class CloneableDesignPresenterBase : DeletableDesignPresenterBase, ICloneable, ICloneableDesignPresenter
{
    //public virtual object Clone()
    //{
    //    return this.CloneBy(x => x.GetCustomAttribute<BrowsableAttribute>()?.Browsable != false);
    //    //return this.CloneXml();
    //    //string txt = JsonSerializer.Serialize(this);
    //    //return JsonSerializer.Deserialize(txt, GetType());
    //}

    object ICloneable.Clone()
    {
        return this.Clone();
    }

    public virtual ICloneableDesignPresenter Clone()
    {
        var result = this.CloneByDisplay() as ICloneableDesignPresenter;
        if (result is IDisplayBindable display)
        {
            display.Name = this.Name;
            display.Description = this.Description;
            display.GroupName = this.GroupName;
        }
        return result;
    }
}

