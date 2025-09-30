// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.Adorner.Adorner;

public class ErrorAdorner : BorderAdorner
{
    public ErrorAdorner(UIElement adornedElement) : base(adornedElement)
    {
        this.Pen = new Pen(Brushes.Red, 1);
        this.ScaleLen = 3;
        this.Fill = new SolidColorBrush(Colors.Red) { Opacity = 0.5 };
    }
}
