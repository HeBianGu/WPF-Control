// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control



// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


using H.Common.Attributes;
using H.Common.Commands;
using H.Modules.Help.Support;
using H.Modules.Help.WebSite;
using System.ComponentModel.DataAnnotations;

namespace H.Modules.Help;

[Icon("\xEC92")]
[Display(Name = "官方网址", Description = "查看官方网址")]
public class ShowWebSiteCommand : DisplayMarkupCommandBase
{
    public override Task ExecuteAsync(object parameter)
    {
        Ioc.GetService<IWebsiteService>()?.Show();
        return base.ExecuteAsync(parameter);
    }

    public override bool CanExecute(object parameter)
    {
        return base.CanExecute(parameter) && Ioc.Exist<IWebsiteService>();
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
