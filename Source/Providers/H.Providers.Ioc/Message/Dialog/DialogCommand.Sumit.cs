// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

namespace H.Providers.Ioc
{
    public class SumitDialogCommand : DialogCommandBase
    {
        public override void Execute(object parameter)
        {
            IDialog dialog = this.GetDialog(parameter);
            dialog.DialogResult = true;
            dialog.Close();
        }
    }
}
