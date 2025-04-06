// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control



// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


using H.Common.Attributes;
using H.Common.Commands;
using H.Modules.Help.Contact;
using System.ComponentModel.DataAnnotations;

namespace H.Modules.Help;

[Icon("\xEC92")]
[Display(Name = "联系方式", Description = "通过此方式联系到开发者")]
public class ShowContactCommand : DisplayMarkupCommandBase
{
    public override Task ExecuteAsync(object parameter)
    {
        Ioc.GetService<IContactService>()?.Show();
        return base.ExecuteAsync(parameter);
    }

    public override bool CanExecute(object parameter)
    {
        return base.CanExecute(parameter) && Ioc.Exist<IContactService>();
    }
}
