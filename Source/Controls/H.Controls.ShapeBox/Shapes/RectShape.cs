// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")
using System.Windows.Media;
using H.Controls.ShapeBox.Shapes.Base;

namespace H.Controls.ShapeBox.Shapes
{
    public class RectShape : TitleShapeBase
    {
        public Rect Rect { get; set; }
        public override void Drawing(IView view, DrawingContext drawingContext, Pen pen, Brush fill = null)
        {
            base.Drawing(view, drawingContext, pen, fill);
            if (this.Rect.IsEmpty)
                return;
            drawingContext.DrawRectangle(fill, pen, Rect);
            this.DrawTitle(view, drawingContext, this.Rect.TopLeft, pen.Brush);
        }
    }
}
