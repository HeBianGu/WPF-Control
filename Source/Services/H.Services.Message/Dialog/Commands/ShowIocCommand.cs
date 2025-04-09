// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

namespace H.Services.Message.Dialog.Commands;

public class ShowIocCommand : ShowMessageDialogCommandBase
{
    public Type Type { get; set; }
    public override bool CanExecute(object parameter)
    {
        return this.Type != null;
    }

    public override async Task ExecuteAsync(object parameter)
    {
        object p = Ioc.Services.GetService(this.Type);
        await IocMessage.ShowDialog(p, x =>
         {
             x.DialogButton = DialogButton.Sumit;
             x.Title = this.Name;
             this.Invoke(x);
         });
    }
}
