using H.Providers.Mvvm;
using System.Windows;

namespace H.Windows.Dock
{
    public class ShowDockWindowCommand : MarkupCommandBase
    {
        public HorizontalAlignment Horizontal { get; set; } = HorizontalAlignment.Right;
        public VerticalAlignment Vertical { get; set; } = VerticalAlignment.Bottom;

        public object Content { get; set; }

        public override void Execute(object parameter)
        {
            var w = new DockWindow()
            {
                HorizontalDock = this.Horizontal,
                VerticalDock = this.Vertical,
                AllowsTransparency = true,
                WindowStyle = WindowStyle.None,
                Content = this.Content
            };
            w.Show();
        }
    }
}
