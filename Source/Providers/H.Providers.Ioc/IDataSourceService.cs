﻿// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Collections.Generic;

namespace H.Providers.Ioc
{
    public interface IDataSourceService<T>
    {
        IEnumerable<T> Collection { get; }
        void Add(params T[] ts);
        void Delete(params T[] ts);
        bool Save(out string message);
        void Load();
    }
}