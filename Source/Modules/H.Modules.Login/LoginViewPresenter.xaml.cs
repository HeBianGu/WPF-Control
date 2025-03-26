
using H.Mvvm;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;
using H.Mvvm.ViewModels.Base;
using H.Mvvm.Commands;
using H.Services.Message.Dialog;
using H.Services.Identity;

namespace H.Modules.Login
{
    public class LoginViewPresenter : BindableBase, ILoginViewPresenter
    {
        private IOptions<LoginOptions> _options;
        public LoginViewPresenter(IOptions<LoginOptions> options)
        {
            _options = options;
            this.UserName = options.Value.LastUserName;
            this.Password = options.Value.LastPassword;
        }

        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                RaisePropertyChanged();
            }
        }


        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                RaisePropertyChanged();
            }
        }

        public InvokeCommand LoginCommand => new InvokeCommand(async (s, e) =>
        {
            if (e is IDialog dialog)
            {
                bool result = await Task.Run<bool>(() =>
                 {
                     try
                     {
                         s.IsBusy = true;
                         s.Message = "正在登录...";
                         Thread.Sleep(1000);
                         bool r = Ioc<ILoginService>.Instance.Login(this.UserName, this.Password, out string message);
                         if (!r)
                         {
                             s.Message = message;
                             Thread.Sleep(2000);
                             return false;
                         }

                         if (LoginOptions.Instance.Remember)
                         {
                             LoginOptions.Instance.LastUserName = this.UserName;
                             LoginOptions.Instance.LastPassword = this.Password;
                         }
                         else
                         {
                             LoginOptions.Instance.LastUserName = null;
                             LoginOptions.Instance.LastPassword = null;
                         }
                         if (LoginOptions.Instance.Save(out message) == false)
                         {
                             s.Message = message;
                             Thread.Sleep(1000);
                         }
                         s.Message = "登录成功";
                         Thread.Sleep(1000);
                         return true;
                     }
                     catch (Exception ex)
                     {
                         s.Message = ex.Message;
                         IocLog.Instance?.Error(ex);
                         return false;
                     }
                     finally
                     {
                         s.IsBusy = false;
                     }
                 });
                if (result)
                {
                    if (_options.Value.Remember)
                    {
                        _options.Value.LastUserName = this.UserName;
                        _options.Value.LastPassword = this.Password;
                    }
                    else
                    {
                        _options.Value.LastUserName = null;
                        _options.Value.LastPassword = null;
                    }
                    dialog.Sumit();
                }
            }
        });
    }
}
