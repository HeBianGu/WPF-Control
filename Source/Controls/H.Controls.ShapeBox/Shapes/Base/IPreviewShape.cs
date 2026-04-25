// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.ShapeBox.Shapes.Base;
public interface IPreviewShape
{
    void DrawPreview(IView view, DrawingContext drawingContext, Brush stroke, double strokeThickness = 1, Brush fill = null, double mousePotionsOffset = 0);

    void DrawPreview(IView view, DrawingContext drawingContext, Brush stroke, double strokeThickness = 1, Brush fill = null);
}

public abstract class PreviewShapeBase : StateShapeBase, IPreviewShape
{
    public bool UseMousePosition { get; protected set; }
    public virtual void DrawPreview(IView view, DrawingContext drawingContext, Brush stroke, double strokeThickness = 1, Brush fill = null, double mousePotionsOffset = 0)
    {
        this.Draw(view, drawingContext, stroke, strokeThickness, fill);
    }

    public void DrawPreview(IView view, DrawingContext drawingContext, Brush stroke, double strokeThickness = 1, Brush fill = null)
    {

    }
}
