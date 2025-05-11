// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Extensions.Command.ScrollViewers;

[Icon("\xE77F")]
[Display(Name = "滚动右侧", Description = "将当前滚动条滚动到最右侧")]
public class ScrollViewerScrollToEndCommand : ScrollViewerScrollToCommandBase
{
    protected override bool CanInvoke(ScrollViewer scrollViewer)
    {
        return scrollViewer.HorizontalOffset < scrollViewer.ExtentWidth;
    }

    protected override void Invoke(ScrollViewer scrollViewer)
    {
        scrollViewer.ScrollToEnd();
    }
}
