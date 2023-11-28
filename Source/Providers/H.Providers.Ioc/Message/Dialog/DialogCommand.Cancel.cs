// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

namespace H.Providers.Ioc
{
    public class CancelDialogCommand : DialogCommandBase
    {
        public override void Execute(object parameter)
        {
            IDialog dialog = this.GetDialog(parameter);
            dialog.DialogResult = false;
            dialog.Close();
        }
    }
}
