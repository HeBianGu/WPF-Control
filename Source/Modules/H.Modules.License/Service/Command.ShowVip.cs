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
    [Icon("\xE713")]
    [Display(Name = "会员", Description = "显示会员页面")]
    public class ShowVipCommand : DisplayMarkupCommandBase
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
            await IocMessage.Dialog.Show(new LicenseViewPresenter() { UseVip = true }, x =>
            {
                x.Title = "许可和会员";
                x.DialogButton = DialogButton.None;
            });
        }
    }
}