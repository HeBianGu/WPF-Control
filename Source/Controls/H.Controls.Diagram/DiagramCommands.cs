// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control



using System.Windows.Input;

namespace H.Controls.Diagram;

public static class DiagramCommands
{
    //public static RoutedUICommand Start = new RoutedUICommand() { Text = "开始流程" };
    //public static RoutedUICommand Stop = new RoutedUICommand() { Text = "停止流程" };
    //public static RoutedUICommand Reset = new RoutedUICommand() { Text = "重置流程" };
    public static RoutedUICommand Clear = new RoutedUICommand() { Text = "清空节点" };
    public static RoutedUICommand DeleteSelected = new RoutedUICommand() { Text = "删除选中" };
    public static RoutedUICommand ZoomToFit = new RoutedUICommand() { Text = "缩放适配" };
    public static RoutedUICommand Aligment = new RoutedUICommand() { Text = "自动布局" };
    public static RoutedUICommand SelectAll = new RoutedUICommand() { Text = "全选" };
    public static RoutedUICommand Next = new RoutedUICommand() { Text = "下一个" };
    public static RoutedUICommand Previous = new RoutedUICommand() { Text = "上一个" };
    public static RoutedUICommand MoveLeft = new RoutedUICommand() { Text = "左移动" };
    public static RoutedUICommand MoveRight = new RoutedUICommand() { Text = "右移动" };
    public static RoutedUICommand MoveUp = new RoutedUICommand() { Text = "上移动" };
    public static RoutedUICommand MoveDown = new RoutedUICommand() { Text = "下移动" };
}
