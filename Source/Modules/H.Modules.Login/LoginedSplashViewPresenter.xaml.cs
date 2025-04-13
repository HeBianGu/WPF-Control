// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using H.Common.Attributes;
using H.Extensions.FontIcon;
using H.Mvvm.ViewModels.Base;
using H.Services.Identity;

namespace H.Modules.Login
{
    [Icon(FontIcons.Connect)]
    public class LoginedSplashViewPresenter : DisplayBindableBase, ILoginedSplashViewPresenter
    {
        private string _message;
        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                RaisePropertyChanged();
            }
        }
    }
}
