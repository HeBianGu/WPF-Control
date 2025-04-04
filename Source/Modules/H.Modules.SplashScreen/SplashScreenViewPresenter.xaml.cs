﻿using H.Services.Common;
using H.Mvvm.ViewModels.Base;

namespace H.Modules.SplashScreen
{
    public class SplashScreenViewPresenter : BindableBase, ISplashScreenViewPresenter
    {
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
    }
}
