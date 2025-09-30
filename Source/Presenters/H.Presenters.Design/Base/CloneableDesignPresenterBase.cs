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

namespace H.Presenters.Design.Base;

public abstract class CloneableDesignPresenterBase : DesignPresenterBase, ICloneable, ICloneable<CloneableDesignPresenterBase>, ICloneableDesignPresenter
{
    //public virtual object Clone()
    //{
    //    return this.CloneBy(x => x.GetCustomAttribute<BrowsableAttribute>()?.Browsable != false);
    //    //return this.CloneXml();
    //    //string txt = JsonSerializer.Serialize(this);
    //    //return JsonSerializer.Deserialize(txt, GetType());
    //}

    public CloneableDesignPresenterBase Clone()
    {
        return ((ICloneable)this).Clone() as CloneableDesignPresenterBase;
    }

    object ICloneable.Clone()
    {
        return this.CloneBy(x => x.GetCustomAttribute<BrowsableAttribute>()?.Browsable != false);
    }

    ICloneableDesignPresenter ICloneableDesignPresenter.Clone()
    {
        return this.Clone();
    }
}

