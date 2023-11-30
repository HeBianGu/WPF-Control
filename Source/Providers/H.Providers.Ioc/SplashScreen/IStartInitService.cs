// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System;

namespace H.Providers.Ioc
{
    public interface IStartInitService
    {
        bool Start(Func<IStartWindow, bool> action);
    }
}