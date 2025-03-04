using H.Mvvm.ViewModels.Base;

namespace H.Modules.Login
{
    public class Forget : BindableBase
    {
        private string _password;
        /// <summary> 说明  </summary>
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                RaisePropertyChanged();
                this.Valid();
            }
        }

        private string _verifyPassword;
        /// <summary> 说明  </summary>
        public string VerifyPassword
        {
            get { return _verifyPassword; }
            set
            {
                _verifyPassword = value;
                RaisePropertyChanged();
                this.Valid();
            }
        }

        private bool _isForget;
        /// <summary> 说明  </summary>
        public bool IsForget
        {
            get { return _isForget; }
            set
            {
                _isForget = value;
                RaisePropertyChanged();
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

        public bool Valid()
        {
            if (this.Password != this.VerifyPassword)
            {
                this.Message = "两次输入的密码不匹配";
                return false;
            }

            if (string.IsNullOrEmpty(this.Password))
            {
                this.Message = "密码不能为空";
                return false;
            }
            this.Message = null;
            return true;
        }

    }
}
