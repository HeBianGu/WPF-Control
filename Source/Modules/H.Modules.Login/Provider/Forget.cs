using H.Providers.Mvvm;

namespace H.Modules.Login
{
    public class Forget : ViewModelBase
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
            return true;
        }

    }
}
