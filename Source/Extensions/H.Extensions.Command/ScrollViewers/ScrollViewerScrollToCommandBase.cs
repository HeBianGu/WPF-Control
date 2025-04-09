// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

namespace H.Extensions.Command.ScrollViewers;

public abstract class ScrollViewerScrollToCommandBase : DisplayMarkupCommandBase
{
    public override bool CanExecute(object parameter)
    {
        if (this.GetScrollViewer(parameter) is ScrollViewer sv)
            return this.CanInvoke(sv);
        return false;
    }
    public override void Execute(object parameter)
    {
        if (this.GetScrollViewer(parameter) is ScrollViewer sv)
            this.Invoke(sv);
    }

    protected abstract void Invoke(ScrollViewer scrollViewer);

    protected abstract bool CanInvoke(ScrollViewer scrollViewer);

    protected ScrollViewer GetScrollViewer(object parameter)
    {
        return this.GetTargetElement(parameter).GetParent<ScrollViewer>();
    }
}
