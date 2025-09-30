// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

global using System.Windows.Controls;

namespace H.Extensions.Command.ScrollViewers;

public class ScrollViewerLineLeftCommand : ScrollViewerScrollToHomeCommand
{
    protected override void Invoke(ScrollViewer scrollViewer)
    {
        scrollViewer.LineLeft();
    }
}
