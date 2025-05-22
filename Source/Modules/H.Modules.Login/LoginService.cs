// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Modules.Login.Base;
using H.Extensions.Mvvm.ViewModels.Base;
using H.Services.Identity;
using H.Services.Identity.User;

namespace H.Modules.Login
{
    internal class LoginService : BindableBase, ILoginService
    {
        private Random random = new Random();
        private IUser _user;
        public IUser User
        {
            get { return _user; }
            private set
            {
                _user = value;
                RaisePropertyChanged();
            }
        }

        public bool Login(string name, string password, out string message)
        {
            if (string.IsNullOrEmpty(name))
            {
                message = "用户名不能为空";
                return false;
            }

            if (string.IsNullOrEmpty(password))
            {
                message = "密码不能为空";
                return false;
            }

            if (name == LoginOptions.Instance.AdminName && password == LoginOptions.Instance.AdminPassword)
            {
                Thread.Sleep(1000);
                this.User = new TestUser(name, password, "Test Name");
                message = "登录成功";
                return true;
            }
            else
            {
                Thread.Sleep(2000);
                message = "登录失败，用户名密码错误";
                return false;
            }

        }

        public bool Logout(out string message)
        {
            message = null;
            this.User = null;
            return true;
        }
    }
}
