// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Extensions.Common;
using System.Windows.Input;

namespace H.Controls.ShapeBox;

public class ROIShapeBox : ShapeBox
{
    private DrawingVisual _ROIShapeDrawingVisual = new DrawingVisual();
    public ROIShapeBox()
    {
        this.ROIFill = Colors.Black.ToFreezeSolid(x => x.Opacity = 0.5);
    }
    protected override IEnumerable<Visual> CreateVisuals()
    {
        return base.CreateVisuals().Concat(this._ROIShapeDrawingVisual.ToEnumerable());
    }
    public bool UseROI
    {
        get { return (bool)GetValue(UseROIProperty); }
        set { SetValue(UseROIProperty, value); }
    }

    public static readonly DependencyProperty UseROIProperty =
        DependencyProperty.Register("UseROI", typeof(bool), typeof(ROIShapeBox), new FrameworkPropertyMetadata(true, (d, e) =>
        {
            ROIShapeBox control = d as ROIShapeBox;

            if (control == null) return;

            if (e.OldValue is bool o)
            {

            }

            if (e.NewValue is bool n)
            {

            }
            control.UpdateAll();
        }));

    public Brush ROIStroke
    {
        get { return (Brush)GetValue(ROIStrokeProperty); }
        set { SetValue(ROIStrokeProperty, value); }
    }

    public static readonly DependencyProperty ROIStrokeProperty =
        DependencyProperty.Register("ROIStroke", typeof(Brush), typeof(ROIShapeBox), new FrameworkPropertyMetadata(SystemColors.HighlightBrush, (d, e) =>
        {
            ROIShapeBox control = d as ROIShapeBox;

            if (control == null) return;

            if (e.OldValue is Brush o)
            {

            }

            if (e.NewValue is Brush n)
            {

            }
            control.DrawROI();
        }));


    public Brush ROIFill
    {
        get { return (Brush)GetValue(ROIFillProperty); }
        set { SetValue(ROIFillProperty, value); }
    }

    public static readonly DependencyProperty ROIFillProperty =
        DependencyProperty.Register("ROIFill", typeof(Brush), typeof(ROIShapeBox), new FrameworkPropertyMetadata(null, (d, e) =>
        {
            ROIShapeBox control = d as ROIShapeBox;

            if (control == null) return;

            if (e.OldValue is Brush o)
            {

            }

            if (e.NewValue is Brush n)
            {

            }
            control.DrawROI();
        }));



    public double ROIStrokeThickness
    {
        get { return (double)GetValue(ROIStrokeThicknessProperty); }
        set { SetValue(ROIStrokeThicknessProperty, value); }
    }

    public static readonly DependencyProperty ROIStrokeThicknessProperty =
        DependencyProperty.Register("ROIStrokeThickness", typeof(double), typeof(ROIShapeBox), new FrameworkPropertyMetadata(1.0, (d, e) =>
        {
            ROIShapeBox control = d as ROIShapeBox;

            if (control == null) return;

            if (e.OldValue is double o)
            {

            }

            if (e.NewValue is double n)
            {

            }
            control.DrawROI();
        }));


    public bool UseBackground
    {
        get { return (bool)GetValue(UseBackgroundProperty); }
        set { SetValue(UseBackgroundProperty, value); }
    }

    public static readonly DependencyProperty UseBackgroundProperty =
        DependencyProperty.Register("UseBackground", typeof(bool), typeof(ROIShapeBox), new FrameworkPropertyMetadata(true, (d, e) =>
        {
            ROIShapeBox control = d as ROIShapeBox;

            if (control == null) return;

            if (e.OldValue is bool o)
            {

            }

            if (e.NewValue is bool n)
            {

            }
            control.DrawROI();
        }));


    public Rect ROI
    {
        get { return (Rect)GetValue(ROIProperty); }
        set { SetValue(ROIProperty, value); }
    }

    public static readonly DependencyProperty ROIProperty =
        DependencyProperty.Register("ROI", typeof(Rect), typeof(ROIShapeBox), new FrameworkPropertyMetadata(default(Rect), (d, e) =>
        {
            ROIShapeBox control = d as ROIShapeBox;

            if (control == null) return;

            if (e.OldValue is Rect o)
            {

            }

            if (e.NewValue is Rect n)
            {

            }
            control.UpdateAll();
        }));

    protected override void UpdateSize()
    {
        if (this.UseROI == false || this.ROI == default || this.ROI.IsEmpty)
        {
            base.UpdateSize();
            return;
        }
        this.Width = this.ROI.Width;
        this.Height = this.ROI.Height;
    }

    public override void UpdateAll()
    {
        base.UpdateAll();
        this.DrawROI();
    }

    protected override Vector GetDrawingOffset()
    {
        if (this.UseROI == false || this.ROI == default || this.ROI.IsEmpty)
            return default;
        return new Vector(-this.ROI.X, -this.ROI.Y);
    }

    protected override Vector GetShapeDrawingOffset()
    {
        return default;
    }

    protected override Point ToImagePoint(Point point)
    {
        if (this.UseROI == false || this.ROI == default || this.ROI.IsEmpty)
            return base.ToImagePoint(point);
        point.Offset(this.ROI.X, this.ROI.Y);
        return point;
    }

    public void DrawROI()
    {
        using var dc = this._ROIShapeDrawingVisual.RenderOpen();
        if (this.UseROI == false || this.ROI == default || this.ROI.IsEmpty)
            return;
        var roi = new Rect(0, 0, this.ROI.Width, this.ROI.Height);
        var pen = this.ROIStroke.ToPen(x => x.Thickness = this.ROIStrokeThickness);
        if (this.UseBackground)
        {
            var boundingBox = new Rect(-this.ROI.X, -this.ROI.Y, this.GetImagePixelRenderWidth(), this.GetImagePixelRenderHeight());
            var combine = new CombinedGeometry(GeometryCombineMode.Exclude, new RectangleGeometry(boundingBox), new RectangleGeometry(roi));
            dc.PushClip(combine);
            dc.DrawRectangle(this.ROIFill, null, boundingBox);
            dc.Pop();
        }
        this.DrawROIStroke(dc, roi, pen.Thickness);
    }

    private void DrawROIStroke(DrawingContext dc, Rect roi, double thickness)
    {
        var blackPen = new Pen(Brushes.White, thickness);
        blackPen.DashStyle = new DashStyle(new double[] { 4, 4 }, 0);
        blackPen.Freeze();

        var whitePen = new Pen(Brushes.Black, thickness);
        whitePen.DashStyle = new DashStyle(new double[] { 4, 4 }, 4);
        whitePen.Freeze();

        dc.DrawRoundedRectangle(null, blackPen, roi, thickness, thickness);
        dc.DrawRoundedRectangle(null, whitePen, roi, thickness, thickness);
    }
}
