// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

namespace H.Providers.Ioc
{
    public class ShowMessageCommand : MessageCommandBase
    {
        public string Message { get; set; }
        public override void Execute(object parameter)
        {
            IocMessage.Dialog.ShowMessage(Message, Title, DialogButton, Build);
        }
        public override bool CanExecute(object parameter)
        {
            return !string.IsNullOrEmpty(Message);
        }
    }
}
