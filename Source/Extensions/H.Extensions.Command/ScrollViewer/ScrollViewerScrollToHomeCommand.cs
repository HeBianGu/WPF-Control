// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


using H.Mvvm;
using H.Mvvm.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Windows.Controls;

namespace H.Extensions.Command
{
    [Icon("\xE77F")]
    [Display(Name = "滚动左侧", Description = "将当前滚动条滚动到最左侧")]
    public class ScrollViewerScrollToHomeCommand : ScrollViewerScrollToCommandBase
    {
        protected override void Invoke(ScrollViewer scrollViewer)
        {
            scrollViewer.ScrollToHome();
        }
        protected override bool CanInvoke(ScrollViewer scrollViewer)
        {
            return scrollViewer.HorizontalOffset > 0;
        }
    }
}
