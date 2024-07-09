// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

namespace H.Providers.Ioc
{
    public abstract class MessageCommandBase : MarkupExtension, ICommand
    {
        public string Title { get; set; }
        public int Width { get; set; } = 500;
        public int Height { get; set; } = 300;
        public DialogButton DialogButton { get; set; }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }
        public virtual bool CanExecute(object parameter)
        {
            return true;
        }

        public abstract void Execute(object parameter);

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }

        protected void Build(IDialog w)
        {
            if (w is Control c)
            {
                c.Width = this.Width;
                c.Height = this.Height;
            }
        }
    }
}
