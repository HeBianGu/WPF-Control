// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


namespace H.Services.Common
{
    public class ShowMessageCommand : MessageCommandBase
    {
        public override bool CanExecute(object parameter)
        {
            return !string.IsNullOrEmpty(this.Message);
        }

        public override async Task ExecuteAsync(object parameter)
        {
            await IocMessage.Dialog.Show(this.Message, x =>
              {
                  x.DialogButton = this.DialogButton;
                  x.Title = this.Title;
                  this.Invoke(x);
              });
        }
    }
}
