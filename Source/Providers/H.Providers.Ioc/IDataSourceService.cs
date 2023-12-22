// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Collections.Generic;

namespace H.Providers.Ioc
{
    public interface IDataSourceService<T> : ISplashLoad, ISave
    {
        IEnumerable<T> Collection { get; }
        void Add(params T[] ts);
        void Delete(params T[] ts);
    }
}