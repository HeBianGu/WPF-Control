// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Extensions.Mvvm.Commands;
using System.Windows.Input;

namespace H.Controls.PrintBox
{
    public static class PrintCommands
    {
        public static DisplayRoutedUICommand PrintView = new DisplayRoutedUICommand() { Text = "打印预览", Icon = "\xE890" };
        public static DisplayRoutedUICommand Print = new DisplayRoutedUICommand() { Text = "打印", Icon = "\xE749" };
        public static DisplayRoutedUICommand PrintSetting = new DisplayRoutedUICommand() { Text = "打印设置", Icon = "\xE713" };
        //public static RoutedUICommand ParamSetting = new RoutedUICommand() { Text = "参数设置" };
        //public static DisplayRoutedUICommand PageSize = new DisplayRoutedUICommand() { Text = "纸张设置", Icon = "\xE8FF" };
    }
}
