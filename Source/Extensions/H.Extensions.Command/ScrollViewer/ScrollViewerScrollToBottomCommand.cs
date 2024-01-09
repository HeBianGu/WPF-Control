// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


using H.Providers.Mvvm;
using System.Windows.Controls;

namespace H.Extensions.Command
{
    public class ScrollViewerScrollToBottomCommand : MarkupCommandBase
    {
        public override bool CanExecute(object parameter)
        {
            return parameter is ScrollViewer sv && sv.VerticalOffset <sv.ExtentHeight;
        }
        public override void Execute(object parameter)
        {
            if (parameter is ScrollViewer sv)
            {
                sv.ScrollToBottom();
            }
        }
    }
}
