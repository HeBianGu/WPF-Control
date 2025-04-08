// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control



// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


using H.Common.Attributes;
using H.Common.Commands;
using H.Extensions.FontIcon;
using H.Modules.Help.ReleaseVersions;
using System.ComponentModel.DataAnnotations;

namespace H.Modules.Help;

[Icon(FontIcons.History)]
[Display(Name = "发行说明", Description = "查看软件发行说明")]
public class ShowReleaseVersionsCommand : DisplayMarkupCommandBase
{
    public override Task ExecuteAsync(object parameter)
    {
        Ioc.GetService<IReleaseVersionsService>()?.Show();
        return base.ExecuteAsync(parameter);
    }

    public override bool CanExecute(object parameter)
    {
        return base.CanExecute(parameter) && Ioc.Exist<IReleaseVersionsService>();
    }
}
