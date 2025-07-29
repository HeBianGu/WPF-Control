// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")
using System.Windows.Ink;
using System.Windows.Media;

namespace H.Controls.ShapeBox.Shapes.Base
{
    public abstract class ShapeBase : IShape
    {
        public Brush Stroke { get; set; }
        public double StrokeThickness { get; set; }
        public Brush fill { get; set; }
        public abstract void Draw(DrawingContext drawingContext, Brush stroke, double strokeThickness = 1, Brush fill = null);
    }


}
