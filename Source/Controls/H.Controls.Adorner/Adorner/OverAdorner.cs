// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.Adorner.Adorner;

public class OverAdorner : BorderAdorner
{
    public OverAdorner(UIElement adornedElement) : base(adornedElement)
    {
        this.Fill = new SolidColorBrush(Colors.LightSkyBlue) { Opacity = 0.1 };
        this.Pen = new Pen(new SolidColorBrush(Colors.SkyBlue), 1);
    }
}
