// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control



// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


using H.Common.Attributes;
using H.Common.Commands;
using H.Extensions.FontIcon;
using H.Modules.Help.Support;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace H.Modules.Help;

[Icon(FontIcons.Group)]
[Display(Name = "技术支持", Description = "查看技术支持文档")]
public class ShowSupportCommand : DisplayMarkupCommandBase
{
    public override Task ExecuteAsync(object parameter)
    {
        Ioc.GetService<ISupportService>()?.Show();
        return base.ExecuteAsync(parameter);
    }

    public override bool CanExecute(object parameter)
    {
        return base.CanExecute(parameter) && Ioc.Exist<ISupportService>();
    }
}

//[Icon("\xE946")]
//[Display(Name = "隐私政策", Description = "查看系统隐私政策")]
//public class ShowPrivacyCommand : DisplayMarkupCommandBase
//{
//    public override Task ExecuteAsync(object parameter)
//    {
//        Process.Start(new ProcessStartInfo(AboutViewPresenter.Instance.Privacy) { UseShellExecute = true });
//        return Task.CompletedTask;
//    }
//}

//[Icon("\xE946")]
//[Display(Name = "服务协议", Description = "查看系统服务协议")]
//public class ShowAgreementCommand : IocCommandBase<IAboutViewPresenter>
//{
//    public override Task ExecuteAsync(object parameter)
//    {
//        Process.Start(new ProcessStartInfo(AboutViewPresenter.Instance.Agreement) { UseShellExecute = true });
//        return Task.CompletedTask;
//    }
//}
