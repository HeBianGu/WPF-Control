// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")
using H.Controls.ShapeBox.Drawings;
using System.Windows.Media;

namespace H.Controls.ShapeBox.Shapes.Base
{
    public abstract class TitleShapeBase : PreviewShapeBase
    {
        public string Title { get; set; }

        public virtual void DrawTitle(IView view, DrawingContext drawingContext, Point point, Brush brush)
        {
            if (string.IsNullOrEmpty(this.Title))
                return;
            drawingContext.DrawTextAt(this.Title, point, brush);
        }
    }


}
