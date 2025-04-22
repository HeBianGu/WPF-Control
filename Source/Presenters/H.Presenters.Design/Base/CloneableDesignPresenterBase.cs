// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

global using H.Extensions.Common;
global using H.Mvvm.ViewModels.Base;
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

