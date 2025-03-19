// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


using H.Mvvm;
using H.Mvvm.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using System.Windows.Controls;

namespace H.Extensions.Command
{
    [Icon("\xE77F")]
    [Display(Name = "滚动底部", Description = "将当前进度条滚动到最底部")]
    public class ScrollViewerScrollToBottomCommand : ScrollViewerScrollToCommandBase
    {
        protected override void Invoke(ScrollViewer scrollViewer)
        {
            scrollViewer.ScrollToBottom();
        }

        protected override bool CanInvoke(ScrollViewer scrollViewer)
        {
           return scrollViewer.VerticalOffset < scrollViewer.ExtentHeight;
        }
    }
}
