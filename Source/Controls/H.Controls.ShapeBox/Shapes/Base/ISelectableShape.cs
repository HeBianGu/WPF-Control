// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.ShapeBox.Shapes.Base;
public interface ISelectableShape : IHitableShape
{
    void DrawSelect(IView view, DrawingContext drawingContext, Brush stroke, double strokeThickness = 1, Brush fill = null);
}

public abstract class SelectableShapeBase : MouseOverShapeBase, ISelectableShape
{
    public virtual void DrawSelect(IView view, DrawingContext drawingContext, Brush stroke, double strokeThickness = 1, Brush fill = null)
    {
        var pen = new Pen(stroke, strokeThickness) { StartLineCap = PenLineCap.Round, EndLineCap = PenLineCap.Round, LineJoin = PenLineJoin.Round };
        if (pen.CanFreeze)
            pen.Freeze();
        this.Drawing(view, drawingContext, pen, fill);
    }
}


