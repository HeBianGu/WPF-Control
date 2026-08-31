// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.ValueConverter;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Input;

namespace H.Windows.Main;

public abstract class TransparencyMainWindowBase : MainWindowBase
{
    static TransparencyMainWindowBase()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(TransparencyMainWindowBase), new FrameworkPropertyMetadata(typeof(TransparencyMainWindowBase)));
    }


    public TransparencyMainWindowBase()
    {
        this.AllowsTransparency = true;
        this.WindowStyle = WindowStyle.None;
        this.MouseDown += (s, e) =>
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        };
    }
}
