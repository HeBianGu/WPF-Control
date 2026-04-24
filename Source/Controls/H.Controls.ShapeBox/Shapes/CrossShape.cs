// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Windows.Ink;

namespace H.Controls.ShapeBox.Shapes;
public class CrossShape : CommonShapeBase, IPreviewShape
{
    public Point Point { get; set; }

    public bool UseMousePosition => false;

    protected override Pen GetPen(Brush stroke, double strokeThickness, IView view)
    {
        var pen = base.GetPen(stroke, strokeThickness, view);
        pen.DashStyle = DashStyles.Dash;
        if (pen.CanFreeze)
            pen.Freeze();
        return pen;
    }
    public override void MatrixDrawing(IView view, DrawingContext dc, Pen pen, Brush fill = null)
    {
        dc.DrawLine(pen, new Point(0, Point.Y), new Point(view.Size.Width, Point.Y));
        dc.DrawLine(pen, new Point(Point.X, 0), new Point(Point.X, view.Size.Height));
    }

    public void DrawPreview(IView view, DrawingContext drawingContext, Brush stroke, double strokeThickness = 1, Brush fill = null, double offset = 0)
    {

    }

    public void DrawPreview(IView view, DrawingContext drawingContext, Brush stroke, double strokeThickness = 1, Brush fill = null)
    {
        this.Draw(view, drawingContext, stroke, strokeThickness, fill);
    }
}
