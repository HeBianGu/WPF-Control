﻿// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using H.Providers.Ioc;
using System;

namespace H.Modules.Login
{
    internal class RegisterService : IRegisterService
    {
        public bool Register(string mail, string account, string password, out string message)
        {
            message = string.Empty;
            return true;
        }

        public bool ResetPassword(string mail, string account, string password, out string message)
        {
            message = string.Empty;
            return true;
        }
    }
}
