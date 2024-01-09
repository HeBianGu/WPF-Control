// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


using System.Windows.Controls;

namespace H.Extensions.Command
{
    public class ScrollViewerPageRightCommand : ScrollViewerScrollToEndCommand
    {
        public override void Execute(object parameter)
        {
            if (parameter is ScrollViewer sv)
            {
                sv.PageRight();
            }
        }
    }
}
