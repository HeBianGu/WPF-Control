// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control



// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


using H.Common.Attributes;
using H.Common.Commands;
using H.Extensions.FontIcon;
using H.Modules.Help.Support;
using H.Modules.Help.WebSite;
using System.ComponentModel.DataAnnotations;

namespace H.Modules.Help;

[Icon(FontIcons.Home)]
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
