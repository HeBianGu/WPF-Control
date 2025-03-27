// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

namespace H.Services.Message.Dialog.Commands;

[Icon("\xE890")]
[Display(Name = "查看", Description = "显示表单查看数据")]
public class ShowViewCommand : ShowMessageDialogCommandBase
{
    public object Value { get; set; }

    public override async Task ExecuteAsync(object parameter)
    {
        await IocMessage.Form.ShowView(this.Value ?? parameter, x => this.Invoke(x));
    }

    public override bool CanExecute(object parameter)
    {
        return base.CanExecute(parameter) && (parameter != null || this.Value != null);
    }
}
