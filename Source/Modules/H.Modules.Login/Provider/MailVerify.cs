using H.Mvvm.ViewModels.Base;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace H.Modules.Login
{
    public class MailVerify : BindableBase, IDataErrorInfo
    {
        private string _mail;
        /// <summary> 说明  </summary>
        public string Mail
        {
            get { return _mail; }
            set
            {
                _mail = value;
                RaisePropertyChanged();
                this.Valid();

            }
        }

        private string _verifyCode;
        /// <summary> 说明  </summary>
        public string VerifyCode
        {
            get { return _verifyCode; }
            set
            {
                _verifyCode = value;
                RaisePropertyChanged();
                this.Valid();
            }
        }

        private string _inputCode;
        /// <summary> 说明  </summary>
        public string InputCode
        {
            get { return _inputCode; }
            set
            {
                _inputCode = value;
                RaisePropertyChanged();
                this.Valid();
            }
        }

        private bool _agree;
        /// <summary> 说明  </summary>
        public bool Agree
        {
            get { return _agree; }
            set
            {
                _agree = value;
                RaisePropertyChanged();
                this.Valid();
            }
        }

        private string _message;
        /// <summary> 说明  </summary>
        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                RaisePropertyChanged();
            }
        }

        public string Error => this.Message;

        public string this[string columnName]
        {
            get
            {
                this.Valid();
                return this.Message;
            }
        }

        public bool IsMailValid()
        {
            Regex regex = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
            if (string.IsNullOrEmpty(this.Mail) || !regex.IsMatch(this.Mail))
            {
                this.Message = "邮箱格式不正确";
                return false;
            }
            return true;
        }

        public bool Valid()
        {
            if (!this.IsMailValid())
                return false;

            if (this.InputCode != this.VerifyCode)
            {
                this.Message = "验证码不匹配";
                return false;
            }


            if (string.IsNullOrEmpty(this.VerifyCode))
            {
                this.Message = "请先点击获取验证码";
                return false;
            }

            if (string.IsNullOrEmpty(this.InputCode))
            {
                this.Message = "验证码不能为空";
                return false;
            }

            if (!this.Agree)
            {
                this.Message = "请勾选确认阅读";
                return false;
            }
            this.Message = null;
            return true;
        }
    }
}
