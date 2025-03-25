// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Windows;
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
