// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

namespace H.Services.Common
{
    public abstract class ShowDialogCommandBase : DisplayMarkupCommandBase
    {
        [Obsolete(nameof(Name))]
        public string Title { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public DialogButton DialogButton { get; set; }
        public ITransitionable Transitionable { get; set; }

        protected virtual void Invoke(IDialog w)
        {
            if (w is Control c)
            {
                c.Width = this.Width;
                c.Height = this.Height;
            }
            w.Transitionable = this.Transitionable;
            w.Title = this.Name ?? w.Title;
            w.Icon = this.Icon ?? w.Icon;
        }
    }
}
