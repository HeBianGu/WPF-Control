// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

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
        if (parameter is ScrollViewer scrollViewer)
            return scrollViewer;
        return this.GetTargetElement(parameter).GetParent<ScrollViewer>();
    }
}
