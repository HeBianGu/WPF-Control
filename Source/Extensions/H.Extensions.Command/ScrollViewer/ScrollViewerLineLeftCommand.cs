// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


using System.Windows.Controls;

namespace H.Extensions.Command
{
    public class ScrollViewerLineLeftCommand : ScrollViewerScrollToHomeCommand
    {
        public override void Execute(object parameter)
        {
            if (parameter is ScrollViewer sv)
            {
                sv.LineLeft();
            }
        }
    }
}
