// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


global using System.Windows.Controls;

namespace H.Extensions.Command.ScrollViewers;

public class ScrollViewerLineLeftCommand : ScrollViewerScrollToHomeCommand
{
    protected override void Invoke(ScrollViewer scrollViewer)
    {
        scrollViewer.LineLeft();
    }
}
