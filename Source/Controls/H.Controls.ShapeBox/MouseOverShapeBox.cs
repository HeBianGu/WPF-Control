// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Controls.ShapeBox.Presenters;
using H.Extensions.Common;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Input;

namespace H.Controls.ShapeBox;
public class MouseOverShapeBox : ROIShapeBox
{
    private DrawingVisual _MouseOverableShapeDrawingVisual = new DrawingVisual();

    protected override IEnumerable<Visual> CreateVisuals()
    {
        return base.CreateVisuals().Concat(this._MouseOverableShapeDrawingVisual.ToEnumerable());
    }

    public Brush MouseOverStroke
    {
        get { return (Brush)GetValue(MouseOverStrokeProperty); }
        set { SetValue(MouseOverStrokeProperty, value); }
    }

    public static readonly DependencyProperty MouseOverStrokeProperty =
        DependencyProperty.Register("MouseOverStroke", typeof(Brush), typeof(MouseOverShapeBox), new FrameworkPropertyMetadata(Brushes.Chartreuse, (d, e) =>
        {
            MouseOverShapeBox control = d as MouseOverShapeBox;

            if (control == null) return;

            if (e.OldValue is Brush o)
            {

            }

            if (e.NewValue is Brush n)
            {

            }

        }));


    public double MouseOverStrokeThickness
    {
        get { return (double)GetValue(MouseOverStrokeThicknessProperty); }
        set { SetValue(MouseOverStrokeThicknessProperty, value); }
    }

    public static readonly DependencyProperty MouseOverStrokeThicknessProperty =
        DependencyProperty.Register("MouseOverStrokeThickness", typeof(double), typeof(MouseOverShapeBox), new FrameworkPropertyMetadata(1.0, (d, e) =>
        {
            MouseOverShapeBox control = d as MouseOverShapeBox;

            if (control == null) return;

            if (e.OldValue is double o)
            {

            }

            if (e.NewValue is double n)
            {

            }

        }));



    public Brush MouseOverFill
    {
        get { return (Brush)GetValue(MouseOverFillProperty); }
        set { SetValue(MouseOverFillProperty, value); }
    }

    // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty MouseOverFillProperty =
        DependencyProperty.Register("MouseOverFill", typeof(Brush), typeof(MouseOverShapeBox), new FrameworkPropertyMetadata(default(Brush), (d, e) =>
        {
            MouseOverShapeBox control = d as MouseOverShapeBox;

            if (control == null) return;

            if (e.OldValue is Brush o)
            {

            }

            if (e.NewValue is Brush n)
            {

            }

        }));


    protected override void OnScaleChanged()
    {
        base.OnScaleChanged();
        if (this.MouseOverStrokeThickness > 0)
            this.DrawMouseOverableShapes();
    }

    protected override void OnShapesChanged()
    {
        base.OnShapesChanged();
        this.MouseOverShapes();
    }

    public override void UpdateAll()
    {
        base.UpdateAll();
        this.DrawMouseOverableShapes();
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
        base.OnMouseMove(e);
        if (this.Shapes == null)
            return;
        Point point = e.GetPosition(this);
        var mouses = this.GetShapes().OfType<IMouseOverShape>();
        var finds = mouses.Where(x => x.Hit(this, point));
        this.MouseOverShapes(finds.ToArray());
    }

    private List<IMouseOverShape> _MouseOverShapes = new List<IMouseOverShape>();
    protected virtual void MouseOverShapes(params IMouseOverShape[] MouseOverableShapes)
    {
        this._MouseOverShapes.Clear();
        if (MouseOverableShapes != null)
            this._MouseOverShapes.AddRange(MouseOverableShapes);
        this.DrawMouseOverableShapes();
    }

    private void DrawMouseOverableShapes()
    {
        using var drawingContext = this._MouseOverableShapeDrawingVisual.RenderOpen();
        if (this._MouseOverShapes == null || this._MouseOverShapes.Count() == 0)
            return;
        var strokeThickness = this.ToViewThickness(this.MouseOverStrokeThickness);
        var mouseOverShapes = this._MouseOverShapes.Where(x => this.Shapes.Contains(x));
        foreach (var item in mouseOverShapes)
        {
            item.DrawMouseOver(this, drawingContext, this.MouseOverStroke, strokeThickness, this.MouseOverFill);
        }
        var presenter = this.GetToolTipShapePresenter(mouseOverShapes);
        if (presenter != null)
            this.ToolTip = presenter;
    }

    protected override void ShapesCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        base.ShapesCollectionChanged(sender, e);
        this.Dispatcher.Invoke(() => this.DrawMouseOverableShapes());
    }

    protected virtual IToolTipShapePresenter GetToolTipShapePresenter(IEnumerable<IMouseOverShape> shapes)
    {
        return new ToolTipShapePresenter(shapes);
    }
}
