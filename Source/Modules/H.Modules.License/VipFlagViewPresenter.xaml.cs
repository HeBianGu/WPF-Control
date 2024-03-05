// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using H.Providers.Mvvm;
using System;

namespace H.Modules.License
{
    public interface IVipFlagViewPresenter
    {
        void Refresh();
    }

    public class VipFlagViewPresenter : ViewModelBase, IVipFlagViewPresenter
    {
        public VipFlagViewPresenter()
        {
            this.Refresh();
        }

        private int _level;
        public int Level
        {
            get { return _level; }
            private set
            {
                _level = value;
                RaisePropertyChanged();
            }
        }


        public void Refresh()
        {
            var option = Ioc<ILicenseService>.Instance?.IsVail(out string message);
            this.Level = option.Level;
        }

    }
}
