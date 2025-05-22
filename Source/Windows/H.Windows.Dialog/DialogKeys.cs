// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Windows;

namespace H.Windows.Dialog;

public class DialogKeys
{
    public static ComponentResourceKey None => new ComponentResourceKey(typeof(DialogKeys), "S.DialogWindow.None");
    public static ComponentResourceKey Sumit => new ComponentResourceKey(typeof(DialogKeys), "S.DialogWindow.Sumit");
    public static ComponentResourceKey Cancel => new ComponentResourceKey(typeof(DialogKeys), "S.DialogWindow.Cancel");
    public static ComponentResourceKey SumitAndCancel => new ComponentResourceKey(typeof(DialogKeys), "S.DialogWindow.SumitAndCancel");
}
