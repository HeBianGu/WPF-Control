// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

namespace H.Providers.Ioc
{
    public class SumitDialogCommand:DialogCommandBase
    {
        public override void Execute(object parameter)
        {
            IDialog dialog = this.GetDialog(parameter);
            dialog.DialogResult = true;
            dialog.Close();
        }
    }
}
