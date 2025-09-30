// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Extensions.Command.ScrollViewers;

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
