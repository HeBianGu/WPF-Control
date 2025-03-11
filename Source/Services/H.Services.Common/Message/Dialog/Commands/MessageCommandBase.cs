// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

namespace H.Services.Common
{

    public abstract class MessageCommandBase : ShowDialogCommandBase, ICommand
    {
        protected MessageCommandBase()
        {
            this.Width = 500;
            this.Height = 300;
        }
        public string Message { get; set; }
    }
}
