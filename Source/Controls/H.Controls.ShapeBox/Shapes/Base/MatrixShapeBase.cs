// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")
using System.Windows.Media;

namespace H.Controls.ShapeBox.Shapes.Base
{
    public abstract class MatrixShapeBase : ShapeBase
    {
        protected virtual Matrix GetMatrix()
        {
            return Matrix.Identity;
        }

        protected virtual Matrix GetInvertMatrix()
        {
            var matrix = this.GetMatrix();
            matrix.Invert();
            return matrix;
        }

        public override void Drawing(IView view, DrawingContext dc, Pen pen, Brush fill = null)
        {
            dc.PushTransform(new MatrixTransform(this.GetMatrix()));
            this.MatrixDrawing(view, dc, pen, fill);
            dc.Pop();
        }

        public abstract void MatrixDrawing(IView view, DrawingContext dc, Pen pen, Brush fill = null);
    }

}
