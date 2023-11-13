using H.Extensions.Command;
using H.Providers.Ioc;
using H.Providers.Mvvm;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;

namespace H.Modules.Login
{
    public class LoginViewPresenter : NotifyPropertyChangedBase, ILoginViewPresenter
    {
        IOptions<LoginOptions> _options;
        public LoginViewPresenter(IOptions<LoginOptions> options)
        {
            _options = options;
            this.UserName = options.Value.LastUserName;
            this.Password = options.Value.LastPassword;
        }

        private string _userName;
        /// <summary> 说明  </summary>
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
        /// <summary> 说明  </summary>
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                RaisePropertyChanged();
            }
        }

        public RelayCommand LoginCommand => new RelayCommand(async (s, e) =>
        {
            if (e is IDialogWindow dialog)
            {
                var result = await Task.Run<bool>(() =>
                 {
                     try
                     {
                         s.IsBusy = true;
                         s.Message = "正在登录...";
                         Thread.Sleep(1000);
                         var r = Ioc<ILoginService>.Instance.Login(this.UserName, this.Password, out string message);
                         if (!r)
                         {
                             s.Message = message;
                             Thread.Sleep(1000);
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

                         s.Message = "登录成功";
                         Thread.Sleep(1000);
                         return true;
                     }
                     catch (Exception ex)
                     {
                         s.Message = ex.Message;
                         Logger.Instance?.Error(ex);
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
