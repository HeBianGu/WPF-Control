// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


using System.Windows.Controls;

namespace H.Extensions.Command
{
    public class ScrollViewerPageUpCommand : ScrollViewerScrollToTopCommand
    {
        protected override void Invoke(ScrollViewer scrollViewer)
        {
            scrollViewer.PageUp();
        }
    }
}
