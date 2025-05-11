// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Services.Identity;

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
