﻿// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


namespace H.Services.Common
{
    public abstract class ShowIocCommandBase<T> : ShowDialogCommandBase
    {
        public override async Task ExecuteAsync(object parameter)
        {
            object p = Ioc.GetService<T>();
            await IocMessage.Dialog.Show(p, x =>
            {
                x.DialogButton = DialogButton.Sumit;
                this.Invoke(x);
            });
        }

        public override bool CanExecute(object parameter)
        {
            return Ioc.GetService<T>() != null && base.CanExecute(parameter);
        }
    }
}
