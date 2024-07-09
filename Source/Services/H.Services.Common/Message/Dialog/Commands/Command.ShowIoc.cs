// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

namespace H.Services.Common
{
    public class ShowIocCommand : MessageCommandBase
    {
        public Type Type { get; set; }
        public override void Execute(object parameter)
        {
            object p = System.Ioc.Services.GetService(this.Type);
            IocMessage.Dialog.Show(p, x =>
            {
                x.DialogButton = DialogButton.Sumit;
                x.Title = this.Title;
            });
        }
        public override bool CanExecute(object parameter)
        {
            return this.Type != null;
        }
    }
}
