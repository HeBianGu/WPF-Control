// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

global using H.Extensions.Mvvm.ViewModels.Base;
global using H.Mvvm.ViewModels.Base;

namespace H.Modules.License
{
    public interface ILicenseFlagViewPresenter
    {
        void Refresh();
    }

    public class LicenseFlagViewPresenter : BindableBase, ILicenseFlagViewPresenter
    {
        public LicenseFlagViewPresenter()
        {
            this.Refresh();
        }

        private DateTime _time;
        public DateTime Time
        {
            get { return _time; }
            private set
            {
                _time = value;
                RaisePropertyChanged();
            }
        }

        private bool _isTrail;
        public bool IsTrail
        {
            get { return _isTrail; }
            private set
            {
                _isTrail = value;
                RaisePropertyChanged();
            }
        }

        public void Refresh()
        {
            var option = Ioc<ILicenseService>.Instance?.IsVail(out string message);
            this.Time = option.EndTime;
            this.IsTrail = option.Level == -1;
        }
    }
}
