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
    public interface ISelectableShape
    {
        void DrawSelect(IView view, DrawingContext drawingContext, Brush stroke, double strokeThickness = 1, Brush fill = null);
    }


    public abstract class SelectableShapeBase : CommonShapeBase, ISelectableShape
    {
        public virtual void DrawSelect(IView view, DrawingContext drawingContext, Brush stroke, double strokeThickness = 1, Brush fill = null)
        {
            this.Draw(view, drawingContext, stroke, strokeThickness, fill);
        }
    }
}
