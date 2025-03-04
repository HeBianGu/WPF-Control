using H.Mvvm.ViewModels.Base;

namespace H.Modules.Login
{
    public class Registor : BindableBase
    {
        private string _useName;
        /// <summary> 说明  </summary>
        public string UserName
        {
            get { return _useName; }
            set
            {
                _useName = value;
                RaisePropertyChanged();
                this.Valid();
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
            if (string.IsNullOrEmpty(this.UserName))
            {
                this.Message = "用户名不能为空";
                return false;
            }


            if (string.IsNullOrEmpty(this.Password))
            {
                this.Message = "密码不能为空";
                return false;
            }

            if (this.Password != this.VerifyPassword)
            {
                this.Message = "两次输入的密码不匹配";
                return false;
            }
            this.Message = null;
            return true;
        }

    }
}
