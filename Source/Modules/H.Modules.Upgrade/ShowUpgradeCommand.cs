﻿// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase



using H.Providers.Ioc;
using H.Providers.Mvvm;
using System;
using System.Reflection;
using System.Windows.Input;

namespace H.Modules.Upgrade
{
    public class ShowUpgradeCommand : MarkupCommandBase
    {
        private IUpgradeService Service => Ioc<IUpgradeService>.Instance;
        public override bool CanExecute(object parameter)
        {
            return this.Service != null;
        }

        public override void Execute(object parameter)
        {
            if (this.Service.Upgrade(out string message)==false)
                IocMessage.ShowMessage(message);
        }
    }
}