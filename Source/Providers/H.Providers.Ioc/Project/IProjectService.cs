// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Collections.Generic;

namespace H.Providers.Ioc
{
    public interface IProjectService : ISave, ISplashLoad
    {
        IProjectItem Current { get; set; }
        IProjectItem Create();
        void Add(IProjectItem project);
        void Delete(Func<IProjectItem, bool> func);
        IEnumerable<IProjectItem> Where(Func<IProjectItem, bool> func = null);
        Action<IProjectItem, IProjectItem> CurrentChanged { get; set; }
    }

    public interface IProjectLoadService : ISplashLoad
    {

    }

}