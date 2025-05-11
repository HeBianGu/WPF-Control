// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Windows.Input;

namespace H.Controls.PrintBox
{
    public static class PrintCommands
    {
        public static RoutedUICommand PrintView = new RoutedUICommand() { Text = "打印预览" };
        public static RoutedUICommand Print = new RoutedUICommand() { Text = "打印" };
        public static RoutedUICommand PrintSetting = new RoutedUICommand() { Text = "打印设置" };
        //public static RoutedUICommand ParamSetting = new RoutedUICommand() { Text = "参数设置" };
        public static RoutedUICommand PageSize = new RoutedUICommand() { Text = "纸张设置" };
    }
}
