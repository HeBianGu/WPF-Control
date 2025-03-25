// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

namespace H.Extensions.Command.ScrollViewers;

[Icon("\xE77F")]
[Display(Name = "滚动顶部", Description = "将当前滚动条滚动到最顶部")]
public class ScrollViewerScrollToTopCommand : ScrollViewerScrollToCommandBase
{
    protected override bool CanInvoke(ScrollViewer scrollViewer)
    {
        return scrollViewer.VerticalOffset > 0;
    }

    protected override void Invoke(ScrollViewer scrollViewer)
    {
        scrollViewer.ScrollToTop();
    }
}
