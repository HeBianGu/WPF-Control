// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


using H.Mvvm;
using H.Mvvm.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Windows.Controls;

namespace H.Extensions.Command
{
    [Icon("\xE77F")]
    [Display(Name = "滚动右侧", Description = "将当前滚动条滚动到最右侧")]
    public class ScrollViewerScrollToEndCommand : DisplayMarkupCommandBase
    {
        public override bool CanExecute(object parameter)
        {
            return parameter is ScrollViewer sv && sv.HorizontalOffset < sv.ExtentWidth;
        }
        public override void Execute(object parameter)
        {
            if (parameter is ScrollViewer sv)
            {
                sv.ScrollToEnd();
            }
        }
    }
}
