// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

namespace H.Services.Message.Dialog;

public abstract class DialogCommandBase : AsyncMarkupCommandBase
{
    protected virtual IDialog GetDialog(object parameter)
    {
        if (parameter is FrameworkElement element)
        {
            if (element is IDialog)
                return element as IDialog;
            FrameworkElement parent = element.GetParent<FrameworkElement>(x => x?.DataContext is IDialog || x is IDialog);
            return parent is IDialog dialog ? dialog : parent.DataContext as IDialog;

        }
        return null;
    }

    protected void Close(object parameter)
    {
        IDialog dialog = this.GetDialog(parameter);
        dialog.Close();
    }

    protected void Cancel(object parameter)
    {
        IDialog dialog = this.GetDialog(parameter);
        dialog.DialogResult = false;
        dialog.Close();
    }

    protected void Sumit(object parameter)
    {
        IDialog dialog = this.GetDialog(parameter);
        dialog.DialogResult = true;
        dialog.Close();
    }

}
