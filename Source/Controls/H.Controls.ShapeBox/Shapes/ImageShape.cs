// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.ShapeBox.Shapes;

public class ImageShape : RectShape
{
    public ImageShape()
    {
    }
    public ImageShape(Rect rect) : base(rect)
    {
    }
    public ImageSource ImageSource { get; set; }

    public override void MatrixDrawing(IView view, DrawingContext drawingContext, Pen pen, Brush fill = null)
    {
        base.MatrixDrawing(view, drawingContext, pen, fill);

        if (this.ImageSource == null)
            return;
        drawingContext.DrawImage(this.ImageSource, this.Rect);
    }
}
