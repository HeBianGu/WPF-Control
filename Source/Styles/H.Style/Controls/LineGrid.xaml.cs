// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")


using System.Windows.Controls;

namespace H.Styles.Controls;

public class LineGrid : Grid
{
    public Brush LineStroke
    {
        get { return (Brush)GetValue(LineStrokeProperty); }
        set { SetValue(LineStrokeProperty, value); }
    }

    public static readonly DependencyProperty LineStrokeProperty =
        DependencyProperty.Register("LineStroke", typeof(Brush), typeof(LineGrid), new FrameworkPropertyMetadata(SystemColors.ActiveBorderBrush, FrameworkPropertyMetadataOptions.AffectsRender, (d, e) =>
        {
            LineGrid control = d as LineGrid;

            if (control == null) return;

            if (e.OldValue is Brush o)
            {

            }

            if (e.NewValue is Brush n)
            {

            }

        }));

    public double LineThickness
    {
        get { return (double)GetValue(LineThicknessProperty); }
        set { SetValue(LineThicknessProperty, value); }
    }

    public static readonly DependencyProperty LineThicknessProperty =
        DependencyProperty.Register("LineThickness", typeof(double), typeof(LineGrid), new FrameworkPropertyMetadata(1d, FrameworkPropertyMetadataOptions.AffectsRender, (d, e) =>
        {
            LineGrid control = d as LineGrid;

            if (control == null) return;

            if (e.OldValue is double o)
            {

            }

            if (e.NewValue is double n)
            {

            }

        }));

    public CornerRadius LineCornerRadius
    {
        get { return (CornerRadius)GetValue(LineCornerRadiusProperty); }
        set { SetValue(LineCornerRadiusProperty, value); }
    }

    public static readonly DependencyProperty LineCornerRadiusProperty =
        DependencyProperty.Register("LineCornerRadius", typeof(CornerRadius), typeof(LineGrid), new FrameworkPropertyMetadata(default(CornerRadius), (d, e) =>
        {
            LineGrid control = d as LineGrid;

            if (control == null) return;

            if (e.OldValue is CornerRadius o)
            {

            }

            if (e.NewValue is CornerRadius n)
            {

            }
            control.UpdateClip();
        }));

    protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
    {
        base.OnRenderSizeChanged(sizeInfo);
        this.UpdateClip();
    }

    protected override void OnRender(DrawingContext dc)
    {
        base.OnRender(dc);
        var pen = new Pen(this.LineStroke ?? SystemColors.ActiveBorderBrush, this.LineThickness);
        if (pen.CanFreeze)
            pen.Freeze();
        dc.DrawRoundedRectangle(null, pen, new Rect(0, 0, this.ActualWidth, this.ActualHeight), this.LineCornerRadius.TopLeft, this.LineCornerRadius.BottomRight);
        foreach (RowDefinition item in this.RowDefinitions)
            dc.DrawLine(pen, new Point(0, item.Offset), new Point(this.ActualWidth, item.Offset));
        foreach (ColumnDefinition item in this.ColumnDefinitions)
            dc.DrawLine(pen, new Point(item.Offset, 0), new Point(item.Offset, this.ActualHeight));
    }

    private void UpdateClip()
    {
        if (this.LineCornerRadius == default(CornerRadius))
        {
            this.Clip = null;
            return;
        }
        Rect rect = new Rect(this.RenderSize);
        double radius = this.LineCornerRadius.TopLeft; // Assuming uniform corner radius
        this.Clip = new RectangleGeometry(rect, radius, radius);
    }
}
