// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Modules.Login.Base;

namespace H.Modules.Login;

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
            message = Properties.Resources.AccountCannotBeNull;
            return false;
        }

        if (string.IsNullOrEmpty(password))
        {
            message = Properties.Resources.PasswordCannotBeNull;
            return false;
        }

        if (name == LoginOptions.Instance.AdminName && password == LoginOptions.Instance.AdminPassword)
        {
            Thread.Sleep(1000);
            this.User = new TestUser(name, password, "Test Name");
            message = Properties.Resources.LoginSuccess;
            return true;
        }
        else
        {
            Thread.Sleep(2000);
            message = Properties.Resources.LoginError;
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
