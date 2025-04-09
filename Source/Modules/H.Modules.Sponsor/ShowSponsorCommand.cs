// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control
using H.Common.Attributes;
using H.Common.Commands;
using H.Extensions.FontIcon;
using H.Iocable;
using H.Services.Message;
using System.ComponentModel.DataAnnotations;

namespace H.Modules.Sponsor;

[Icon(FontIcons.QRCode)]
[Display(Name = "赞助支持", Description = "应用此功能给予赞助支持")]
public class ShowSponsorCommand : DisplayMarkupCommandBase
{
    public override bool CanExecute(object parameter)
    {
        return Ioc.Exist<ISponsorPresenter> != null && base.CanExecute(parameter);
    }
    public override async Task ExecuteAsync(object parameter)
    {
        await IocMessage.ShowDialog(Ioc.GetService<ISponsorPresenter>());
    }
}
