// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

namespace H.Services.Common
{
    public abstract class MessageCommandBase : IocAsyncMarkupCommandBase, ICommand
    {
        public string Title { get; set; }
        public double Width { get; set; } = 500;
        public double Height { get; set; } = 300;
        public DialogButton DialogButton { get; set; }
        public ITransitionable Transitionable { get; set; }

        protected virtual void Build(IDialog w)
        {
            if (w is Control c)
            {
                c.Width = this.Width;
                c.Height = this.Height;
            }
            w.Transitionable = this.Transitionable;
        }
    }
}
