// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Windows.Controls;

namespace H.Extensions.Common;

public static class TreeViewExtension
{
    public static void SelectNone(this TreeView treeView)
    {
        foreach (var item in treeView.GetChildren<TreeViewItem>())
        {
            item.IsSelected = false;
        }
    }
}
