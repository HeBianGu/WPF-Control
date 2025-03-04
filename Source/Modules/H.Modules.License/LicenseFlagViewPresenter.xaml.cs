// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using H.Mvvm.ViewModels.Base;
using System;

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
