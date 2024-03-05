// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


using H.Providers.Mvvm;
using System.Windows.Controls;

namespace H.Extensions.Command
{
    public class ScrollViewerScrollToHomeCommand : MarkupCommandBase
    {
        public override bool CanExecute(object parameter)
        {
            return parameter is ScrollViewer sv && sv.HorizontalOffset > 0;
        }
        public override void Execute(object parameter)
        {
            if (parameter is ScrollViewer sv)
            {
                sv.ScrollToHome();
            }
        }
    }
}
