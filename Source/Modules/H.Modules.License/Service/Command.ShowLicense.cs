// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control



using H.Services.Common;
using System;

namespace H.Modules.License
{
    public class ShowLicenseCommand : IocAsyncMarkupCommandBase
    {
        public override bool CanExecute(object parameter)
        {
            return Ioc<ILicenseService>.Instance != null;
        }
        public override async void Execute(object parameter)
        {
            var option = LicenseProxy.Instance.IsVail(out string error);
            if (option == null)
            {
                var r = await IocMessage.Dialog.Show(error);
                if (r != true)
                    return;
            }
            await IocMessage.Dialog.Show(new LicenseViewPresenter(), x =>
            {
                x.Title = "许可";
                x.DialogButton = DialogButton.None;
            });
        }
    }
}