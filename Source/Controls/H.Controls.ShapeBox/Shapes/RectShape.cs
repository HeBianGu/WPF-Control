// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Windows.Media;
using H.Controls.ShapeBox.Shapes.Base;
using H.Extensions.TypeConverter;

namespace H.Controls.ShapeBox.Shapes
{
    public class RectShape : TitleShapeBase
    {
        public RectShape()
        {
            
        }
        public RectShape(Rect rect)
        {
            this.Rect = rect;
        }
        [TypeConverter(typeof(Round2RectConverter))]
        [Display(Name = "矩形范围", GroupName = "数据")]
        public Rect Rect { get; set; }
        public override void MatrixDrawing(IView view, DrawingContext drawingContext, Pen pen, Brush fill = null)
        {
            if (this.Rect.IsEmpty)
                return;
            drawingContext.DrawRectangle(fill, pen, Rect);
            this.DrawTitle(view, drawingContext, this.Rect.TopLeft, pen.Brush);
            base.MatrixDrawing(view, drawingContext, pen, fill);
        }
    }
}
