// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Common.Attributes;
using H.Common.Commands;
using H.Services.Message;
using H.Services.Message.Dialog;
using System.ComponentModel.DataAnnotations;

namespace H.Modules.License
{
    [Icon("\xE72E")]
    [Display(Name = "许可证书", Description = "显示产品许可注册页面")]
    public class ShowLicenseCommand : DisplayMarkupCommandBase
    {
        public string Module { get; set; }
        public override bool CanExecute(object parameter)
        {
            return Ioc<ILicenseService>.Instance != null;
        }


        public override async void Execute(object parameter)
        {
            var option = IocLicense.Instance.IsVail(this.Module, out string error);
            if (option == null)
            {
                var r = await IocMessage.Dialog.Show(error);
                if (r != true)
                    return;
            }

            var p = this.CreateLicenseViewPresenter();
            await IocMessage.Dialog.Show(p, x =>
            {
                x.Title = this.Name;
                x.DialogButton = DialogButton.None;
            });
        }

        protected virtual ILicenseViewPresenter CreateLicenseViewPresenter()
        {
            var r = new LicenseViewPresenter();
            if (!string.IsNullOrEmpty(this.Module))
                r.Module = this.Module;
            return r;
        }
    }
}