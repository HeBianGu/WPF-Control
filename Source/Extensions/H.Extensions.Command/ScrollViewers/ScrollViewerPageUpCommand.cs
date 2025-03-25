// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

namespace H.Extensions.Command.ScrollViewers;

public class ScrollViewerPageUpCommand : ScrollViewerScrollToTopCommand
{
    protected override void Invoke(ScrollViewer scrollViewer)
    {
        scrollViewer.PageUp();
    }
}
