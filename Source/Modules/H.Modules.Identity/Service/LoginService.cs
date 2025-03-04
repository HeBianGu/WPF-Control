// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


using H.Services.Common;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using H.Extensions.DataBase;
using H.Services.Operation;
using H.Mvvm.ViewModels.Base;

namespace H.Modules.Identity
{
    internal class LoginService : BindableBase, ILoginService
    {
        private readonly IOptions<IdentifyOptions> _options;
        public LoginService(IOptions<IdentifyOptions> options)
        {
            _options = options;
        }
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
