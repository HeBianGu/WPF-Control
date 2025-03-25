// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

namespace H.Services.Message.Dialog.Commands;

public class ShowCommand : ShowMessageDialogCommandBase
{
    public ShowCommand()
    {
        this.Width = double.NaN;
        this.Height = double.NaN;
    }
    public object Presnter { get; set; }
    public override bool CanExecute(object parameter)
    {
        return this.Presnter != null || parameter != null;
    }

    public override async Task ExecuteAsync(object parameter)
    {
        await IocMessage.Dialog.Show(this.Presnter ?? parameter, x =>
           {
               x.DialogButton = this.DialogButton;
               x.Title = this.Name;
               if (this.PresenterTemplate != null)
                   x.PresenterTemplate = this.PresenterTemplate;
               this.Invoke(x);
           });
    }
}
