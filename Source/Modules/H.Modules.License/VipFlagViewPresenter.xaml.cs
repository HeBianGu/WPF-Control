// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Common.Attributes;
using H.Extensions.Mvvm.ViewModels.Base;

namespace H.Modules.License
{
    public interface IVipFlagViewPresenter
    {
        void Refresh();
    }

    [Icon("\xE72E")]
    public class VipFlagViewPresenter : DisplayBindableBase, IVipFlagViewPresenter
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
