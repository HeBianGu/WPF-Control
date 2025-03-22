// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

namespace H.Services.Common
{
    public abstract class ShowDialogCommandBase : DisplayMarkupCommandBase
    {
        public double Width { get; set; } = double.NaN;
        public double Height { get; set; } = double.NaN;
        public DialogButton DialogButton { get; set; }
        public ITransitionable Transitionable { get; set; }
        public HorizontalAlignment HorizontalContentAlignment { get; set; } = HorizontalAlignment.Stretch;

        protected virtual void Invoke(IDialog w)
        {
            w.Width = this.Width;
            w.Height = this.Height;
            w.Transitionable = this.Transitionable;
            w.Title = this.Name ?? w.Title;
            w.HorizontalContentAlignment = this.HorizontalContentAlignment;
            w.Icon = this.Icon ?? w.Icon;
        }
    }
}
