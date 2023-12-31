﻿// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Collections.Generic;

namespace H.Providers.Ioc
{
    public interface IDataSource<T>
    {
        IEnumerable<T> Collection { get; }
        void Add(params T[] ts);
        void Delete(params T[] ts);
        event EventHandler CollectionChanged;
    }
}