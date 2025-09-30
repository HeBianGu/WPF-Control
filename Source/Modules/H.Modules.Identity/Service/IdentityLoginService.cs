// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

global using H.Mvvm.ViewModels.Base;
global using H.Services.Operation;

namespace H.Modules.Identity
{
    internal class IdentityLoginService : BindableBase, ILoginService
    {
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
            message = "登录成功";
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

            if (AdminUser.Instance.Account == name && AdminUser.Instance.Password == password)
            {
                this.User = AdminUser.Instance;
            }
            else
            {
                IStringRepository<hi_dd_user> reporitory = Ioc.GetService<IStringRepository<hi_dd_user>>();
                hi_dd_user user = reporitory.GetList(x => x.Account.ToLower() == name.ToLower()).FirstOrDefault();
                if (user == null)
                {
                    message = "用户名不正确";
                    return false;
                }
                if (user.Password.ToLower() != password.ToLower())
                {
                    message = "密码不正确";
                    return false;
                }

                if (user.Enable == false)
                {
                    message = "该用户没有激活，请联系管理员激活";
                    return false;
                }
                this.User = new User(user);
            }
            Ioc<IOperationService>.Instance?.Log<hi_dd_user>("登录", "登录成功");
            return true;
        }

        public bool Logout(out string message)
        {
            message = null;
            Ioc<IOperationService>.Instance?.Log<hi_dd_user>("登录", "退出登录");
            this.User = null;
            return true;
        }
    }
}
