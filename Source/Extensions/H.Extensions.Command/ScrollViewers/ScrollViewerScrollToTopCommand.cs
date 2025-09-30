// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Extensions.Command.ScrollViewers;

[Icon("\xE77F")]
[Display(Name = "滚动顶部", Description = "将当前滚动条滚动到最顶部")]
public class ScrollViewerScrollToTopCommand : ScrollViewerScrollToCommandBase
{
    protected override bool CanInvoke(ScrollViewer scrollViewer)
    {
        return scrollViewer.VerticalOffset > 0;
    }

    protected override void Invoke(ScrollViewer scrollViewer)
    {
        scrollViewer.ScrollToTop();
    }
}
