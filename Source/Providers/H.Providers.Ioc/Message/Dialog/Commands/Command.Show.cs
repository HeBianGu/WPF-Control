// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

namespace H.Providers.Ioc
{
    public class ShowCommand : MessageCommandBase
    {
        public object Presnter { get; set; }
        public override void Execute(object parameter)
        {
            IocMessage.Dialog.Show(Presnter, x =>
            {
                x.DialogButton = this.DialogButton;
                x.Title = this.Title;
            });
        }
        public override bool CanExecute(object parameter)
        {
            return Presnter != null;
        }
    }
}
