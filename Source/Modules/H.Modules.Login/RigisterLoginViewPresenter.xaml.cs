
using H.Mvvm;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;
using H.Services.Mail;
using H.Mvvm.Commands;
using H.Services.Identity;

namespace H.Modules.Login
{
    public class RigisterLoginViewPresenter : LoginViewPresenter, ILoginViewPresenter
    {
        public RigisterLoginViewPresenter(IOptions<LoginOptions> options) : base(options)
        {

        }

        private int _selectedIndex;
        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set
            {
                _selectedIndex = value;
                RaisePropertyChanged();
            }
        }


        private MailVerify _mailVerify = new MailVerify();
        public MailVerify MailVerify
        {
            get { return _mailVerify; }
            set
            {
                _mailVerify = value;
                RaisePropertyChanged();
            }
        }


        private Registor _registor = new Registor();
        public Registor Registor
        {
            get { return _registor; }
            set
            {
                _registor = value;
                RaisePropertyChanged();
            }
        }

        private Forget _forget = new Forget();
        public Forget Forget
        {
            get { return _forget; }
            set
            {
                _forget = value;
                RaisePropertyChanged();
            }
        }

        private int _code;
        public InvokeCommand GetVerifyCodeCommand => new InvokeCommand(async (s, e) =>
        {
            var ms = Ioc.GetService<IMailService>();
            if (ms == null)
                return;
            _code = Random.Shared.Next(100000, 999999);
            MailMessageItem mailMessageItem = new MailMessageItem();
            mailMessageItem.From = this.MailVerify.Mail;
            mailMessageItem.To = new string[] { RegistorOptions.Instance.MailAccount };
            mailMessageItem.Subject = "注册验证码";
            mailMessageItem.Body = this._code.ToString();
            string message = null;
            var r = await Task.Run(() =>
             {
                 try
                 {
                     s.IsBusy = true;
                     var result = ms.Send(mailMessageItem, false, out message);
                     Thread.Sleep(2000);
                     return result;
                 }
                 catch (Exception)
                 {

                     throw;
                 }
                 finally
                 {

                     s.IsBusy = false;
                 }
             });
            this.MailVerify.Message = r ? "验证码已发送" : message;
        }, (s, e) => this.MailVerify.IsMailValid());

        public RelayCommand GotoMailCommand => new RelayCommand(l =>
        {
            this.GotoRegistor();
            this.Clear();
        });

        private void GotoRegistor()
        {
            if (RegistorOptions.Instance.UseMail)
                this.SelectedIndex = 1;
            else
                this.SelectedIndex = 2;
        }

        public RelayCommand GotoLoginCommand => new RelayCommand(l =>
        {
            this.Forget.IsForget = false;
            this.GotoLogin();
        });

        public RelayCommand GotoNextCommand => new RelayCommand(l =>
        {
            this.GotoNext();
            this.Clear();
        }, x => this.MailVerify.Valid());

        private void GotoNext()
        {
            if (!this.MailVerify.Valid())
                return;
            this.SelectedIndex = this.Forget.IsForget ? 3 : 2;
        }

        public RelayCommand GotoForgetCommand => new RelayCommand(l =>
        {
            this.GotoForget();
            this.Clear();
        });

        private void GotoForget()
        {
            this.Forget.IsForget = true;
            if (RegistorOptions.Instance.UseMail)
                this.SelectedIndex = 1;
            else
                this.SelectedIndex = 3;
        }

        public InvokeCommand ChangePasswordCommand => new InvokeCommand(async (s, e) =>
        {
            if (!this.Forget.Valid())
                return;

            bool result = await Task.Run<bool>(() =>
            {
                try
                {
                    s.IsBusy = true;
                    s.Message = "正在修改密码...";
                    Thread.Sleep(1000);
                    bool r = Ioc<IRegisterService>.Instance.ResetPassword(this.MailVerify.Mail, this.UserName, this.Forget.Password, out string message);
                    if (!r)
                    {
                        s.Message = message;
                        Thread.Sleep(1000);
                        this.Forget.Message = message;
                        return false;
                    }
                    s.Message = "修改成功";
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
                this.Password = this.Forget.Password;
                this.GotoLogin();
                this.Clear();
            }
        }, (s, e) => this.Forget.Valid());

        public void Clear()
        {
            this.MailVerify.InputCode = null;
            this.MailVerify.VerifyCode = null;
            this.MailVerify.Agree = false;

            this.Registor.Password = null;
            this.Registor.VerifyPassword = null;

            this.Forget.Password = null;
            this.Forget.VerifyPassword = null;
        }

        public InvokeCommand RegisterCommand => new InvokeCommand(async (s, e) =>
        {
            if (!this.Registor.Valid())
                return;
            bool result = await Task.Run<bool>(() =>
            {
                try
                {
                    s.IsBusy = true;
                    s.Message = "正在注册...";
                    Thread.Sleep(1000);
                    bool r = Ioc<IRegisterService>.Instance.Register(this.MailVerify.Mail, this.Registor.UserName, this.Registor.Password, out string message);
                    if (!r)
                    {
                        s.Message = message;
                        Thread.Sleep(1000);
                        this.Registor.Message = message;
                        return false;
                    }
                    s.Message = "注册成功";
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
                this.UserName = this.Registor.UserName;
                this.Password = this.Registor.Password;
                this.GotoLogin();
            }
        }, (s, e) => this.Registor.Valid());

        private void GotoLogin()
        {
            this.SelectedIndex = 0;
        }
    }
}
