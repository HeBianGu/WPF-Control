// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.ShapeBox;

public class ThumbShapeBox : ShapeBox
{
    protected override void DrawShape(IShape shape, DrawingContext drawingContext, Brush stroke, double strokeThickness = 1, Brush fill = null)
    {
        if (shape is IThumbShape thumb)
            thumb.DrawThumb(this, drawingContext, this.Stroke, strokeThickness, this.Fill);
        else
            shape.Draw(this, drawingContext, this.Stroke, strokeThickness, this.Fill);
    }
}
