// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")
using System.Windows.Media;

namespace H.Controls.ImageColorPicker.Drawings
{
    public static class DrawingContextExtension
    {
        public static void DrawHandle(this DrawingContext dc, Point point, Brush stroke, double strokeThickness = 1, double len = 6)
        {
            double s = len / 2;
            dc.DrawEllipse(null, new Pen(stroke, strokeThickness), point, len, len);
        }

        public static void DrawColorText(this DrawingContext dc, Point point, Color color, Visual visual, double fontSize = 10.0)
        {
            FormattedText formattedText = new FormattedText(
                                    $"{color}",
                                    System.Globalization.CultureInfo.CurrentCulture,
                                    FlowDirection.LeftToRight,
                                    new Typeface("Microsoft YaHei"),
                                    fontSize,
                                    Brushes.Chartreuse,
                                    VisualTreeHelper.GetDpi(visual).PixelsPerDip);
            dc.DrawText(formattedText, point + new Vector(10, 10));
        }
    }
}
