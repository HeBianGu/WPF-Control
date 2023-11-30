// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using H.Providers.Ioc;

namespace H.Providers.Ioc
{
    public interface ILoginService
    {
        IUser User { get; }
        bool Login(string name, string password, out string message);
        bool Logout(out string message);
    }
}