// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using H.Providers.Mvvm;
using System;
using System.ComponentModel;
using System.Reflection;

namespace H.Presenters.Design
{
    public class DesignPresenter : DesignPresenterBase, IDesignPresenter
    {
        public virtual object Clone()
        {
            return this.CloneBy(x => x.GetCustomAttribute<BrowsableAttribute>()?.Browsable != false);
            //return this.CloneXml();
            //string txt = JsonSerializer.Serialize(this);
            //return JsonSerializer.Deserialize(txt, GetType());
        }
    }
}
