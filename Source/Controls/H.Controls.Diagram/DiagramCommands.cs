// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Extensions.FontIcon;
using H.Extensions.Mvvm.Commands;

namespace H.Controls.Diagram;

public static class DiagramCommands
{
    //public static RoutedUICommand Start = new RoutedUICommand() { Text = "开始流程" };
    //public static RoutedUICommand Stop = new RoutedUICommand() { Text = "停止流程" };
    //public static RoutedUICommand Reset = new RoutedUICommand() { Text = "重置流程" };
    public static DisplayRoutedUICommand Clear = new DisplayRoutedUICommand() { Text = "清空节点", Icon = FontIcons.Clear };
    public static DisplayRoutedUICommand DeleteSelected = new DisplayRoutedUICommand() { Text = "删除选中", Icon = FontIcons.Delete };
    public static DisplayRoutedUICommand ZoomToFit = new DisplayRoutedUICommand() { Text = "缩放适配", Icon = FontIcons.FitPage };
    public static DisplayRoutedUICommand Aligment = new DisplayRoutedUICommand() { Text = "自动布局", Icon = FontIcons.Flow };
    public static DisplayRoutedUICommand SelectAll = new DisplayRoutedUICommand() { Text = "全选", Icon = FontIcons.Checkbox };
    public static DisplayRoutedUICommand Next = new DisplayRoutedUICommand() { Text = "下一个", Icon = FontIcons.Next };
    public static DisplayRoutedUICommand Previous = new DisplayRoutedUICommand() { Text = "上一个", Icon = FontIcons.Previous };
    public static DisplayRoutedUICommand MoveLeft = new DisplayRoutedUICommand() { Text = "左移动", Icon = FontIcons.ChevronLeft };
    public static DisplayRoutedUICommand MoveRight = new DisplayRoutedUICommand() { Text = "右移动", Icon = FontIcons.ChevronRight };
    public static DisplayRoutedUICommand MoveUp = new DisplayRoutedUICommand() { Text = "上移动", Icon = FontIcons.ChevronUp };
    public static DisplayRoutedUICommand MoveDown = new DisplayRoutedUICommand() { Text = "下移动", Icon = FontIcons.ChevronDown };
}
