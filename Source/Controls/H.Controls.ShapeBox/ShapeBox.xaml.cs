// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

global using H.Controls.ShapeBox.Drawings;
global using H.Controls.ShapeBox.Shapes.Base;
global using H.Controls.ShapeBox.State.Base;
global using System;
global using System.Collections.ObjectModel;
global using System.Windows;
global using System.Windows.Media;
global using System.Windows.Media.Imaging;
global using System.Windows.Threading;

namespace H.Controls.ShapeBox;
public class ShapeBox : FrameworkElement, IShapeView, IImageView
{
    static ShapeBox()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(ShapeBox), new FrameworkPropertyMetadata(typeof(ShapeBox)));
    }

    private VisualCollection _visualCollection;
    private ImageDrawingVisual _imageDrawingVisual = new ImageDrawingVisual();
    private ShapeDrawingVisual _shapeDrawingVisual = new ShapeDrawingVisual();
    public ShapeBox()
    {
        this._visualCollection = new VisualCollection(this);
        foreach (var item in this.CreateVisuals())
        {
            this._visualCollection.Add(item);
        }
    }

    protected virtual IEnumerable<Visual> CreateVisuals()
    {
        yield return _imageDrawingVisual;
        yield return _shapeDrawingVisual;
    }

    private void ShapeBox_Loaded(object sender, RoutedEventArgs e)
    {
        this.UpdateImage();
    }

    #region - VisualCollection -

    protected override Visual GetVisualChild(int index)
    {
        return _visualCollection[index];
    }
    protected override int VisualChildrenCount
    {
        get
        {
            return _visualCollection.Count;
        }
    }
    #endregion

    public ImageSource ImageSource
    {
        get { return (ImageSource)GetValue(ImageSourceProperty); }
        set { SetValue(ImageSourceProperty, value); }
    }

    public static readonly DependencyProperty ImageSourceProperty =
        DependencyProperty.Register("ImageSource", typeof(ImageSource), typeof(ShapeBox), new FrameworkPropertyMetadata(default(ImageSource), (d, e) =>
        {
            ShapeBox control = d as ShapeBox;

            if (control == null) return;

            if (e.OldValue is ImageSource o)
            {

            }

            if (e.NewValue is ImageSource n)
            {

            }
            control.UpdateImage();
        }));


    public double ImagePixelWidth
    {
        get { return (double)GetValue(ImagePixelWidthProperty); }
        set { SetValue(ImagePixelWidthProperty, value); }
    }

    public static readonly DependencyProperty ImagePixelWidthProperty =
        DependencyProperty.Register("ImagePixelWidth", typeof(double), typeof(ShapeBox), new FrameworkPropertyMetadata(double.NaN, (d, e) =>
        {
            ShapeBox control = d as ShapeBox;

            if (control == null) return;

            if (e.OldValue is double o)
            {

            }

            if (e.NewValue is double n)
            {

            }
            control.UpdateImage();
        }));

    public double ImagePixelHeight
    {
        get { return (double)GetValue(ImagePixelHeightProperty); }
        set { SetValue(ImagePixelHeightProperty, value); }
    }

    public static readonly DependencyProperty ImagePixelHeightProperty =
        DependencyProperty.Register("ImagePixelHeight", typeof(double), typeof(ShapeBox), new FrameworkPropertyMetadata(double.NaN, (d, e) =>
        {
            ShapeBox control = d as ShapeBox;

            if (control == null) return;

            if (e.OldValue is double o)
            {

            }

            if (e.NewValue is double n)
            {

            }
            control.UpdateImage();
        }));



    private void UpdateImage()
    {
        this.UpdateSize();
        this.UpdateDrawingVisualsTransform();
        this._imageDrawingVisual.ImageSource = this.ImageSource;
        this._imageDrawingVisual.Width = this.GetImagePixelRenderWidth();
        this._imageDrawingVisual.Height = this.GetImagePixelRenderHeight();
        this._imageDrawingVisual.Draw();
    }

    protected virtual Vector GetDrawingOffset()
    {
        return default;
    }

    protected virtual Vector GetShapeDrawingOffset()
    {
        return this.GetDrawingOffset();
    }

    private void UpdateDrawingVisualsTransform()
    {
        var offset = this.GetDrawingOffset();
        Transform imageTransform = offset == default ? null : new TranslateTransform(offset.X, offset.Y);
        var shapeOffset = this.GetShapeDrawingOffset();
        Transform shapeTransform = shapeOffset == default ? null : new TranslateTransform(shapeOffset.X, shapeOffset.Y);
        this._imageDrawingVisual.Transform = imageTransform;
        this._shapeDrawingVisual.Transform = shapeTransform;
    }

    protected virtual void UpdateSize()
    {
        this.Width = this.GetImagePixelRenderWidth();
        this.Height = this.GetImagePixelRenderHeight();
    }

    protected double GetImagePixelRenderWidth()
    {
        return double.IsNaN(this.ImagePixelWidth) ? this.ImageSource?.Width ?? 0 : this.ImagePixelWidth;
    }

    protected double GetImagePixelRenderHeight()
    {
        return double.IsNaN(this.ImagePixelHeight) ? this.ImageSource?.Height ?? 0 : this.ImagePixelHeight;
    }

    public Brush Stroke
    {
        get { return (Brush)GetValue(StrokeProperty); }
        set { SetValue(StrokeProperty, value); }
    }

    public static readonly DependencyProperty StrokeProperty =
        DependencyProperty.Register("Stroke", typeof(Brush), typeof(ShapeBox), new FrameworkPropertyMetadata(Brushes.Chartreuse, (d, e) =>
        {
            ShapeBox control = d as ShapeBox;

            if (control == null) return;

            if (e.OldValue is Brush o)
            {

            }

            if (e.NewValue is Brush n)
            {

            }

        }));

    public double StrokeThickness
    {
        get { return (double)GetValue(StrokeThicknessProperty); }
        set { SetValue(StrokeThicknessProperty, value); }
    }

    public static readonly DependencyProperty StrokeThicknessProperty =
        DependencyProperty.Register("StrokeThickness", typeof(double), typeof(ShapeBox), new FrameworkPropertyMetadata(-1.0, (d, e) =>
        {
            ShapeBox control = d as ShapeBox;

            if (control == null) return;

            if (e.OldValue is double o)
            {

            }

            if (e.NewValue is double n)
            {

            }

        }));

    public Brush Fill
    {
        get { return (Brush)GetValue(FillProperty); }
        set { SetValue(FillProperty, value); }
    }

    public static readonly DependencyProperty FillProperty =
        DependencyProperty.Register("Fill", typeof(Brush), typeof(ShapeBox), new FrameworkPropertyMetadata(null, (d, e) =>
        {
            ShapeBox control = d as ShapeBox;

            if (control == null) return;

            if (e.OldValue is Brush o)
            {

            }

            if (e.NewValue is Brush n)
            {

            }

        }));

    public Point Position
    {
        get { return (Point)GetValue(PositionProperty); }
        set { SetValue(PositionProperty, value); }
    }

    public static readonly DependencyProperty PositionProperty =
        DependencyProperty.Register("Position", typeof(Point), typeof(ShapeBox), new FrameworkPropertyMetadata(default(Point), (d, e) =>
        {
            ShapeBox control = d as ShapeBox;

            if (control == null) return;

            if (e.OldValue is Point o)
            {

            }

            if (e.NewValue is Point n)
            {

            }

        }));


    public double Scale
    {
        get { return (double)GetValue(ScaleProperty); }
        set { SetValue(ScaleProperty, value); }
    }

    public static readonly DependencyProperty ScaleProperty =
        DependencyProperty.Register("Scale", typeof(double), typeof(ShapeBox), new FrameworkPropertyMetadata(1.0, (d, e) =>
        {
            ShapeBox control = d as ShapeBox;

            if (control == null) return;

            if (e.OldValue is double o)
            {

            }

            if (e.NewValue is double n)
            {

            }
            control.OnScaleChanged();
        }));

    protected virtual void OnScaleChanged()
    {
        if (this.StrokeThickness > 0)
            this.DrawShapes();
    }

    public Size Size => this.RenderSize;

    public ObservableCollection<IShape> Shapes
    {
        get { return (ObservableCollection<IShape>)GetValue(ShapesProperty); }
        set { SetValue(ShapesProperty, value); }
    }

    public static readonly DependencyProperty ShapesProperty =
        DependencyProperty.Register("Shapes", typeof(ObservableCollection<IShape>), typeof(ShapeBox), new FrameworkPropertyMetadata(default(ObservableCollection<IShape>), (d, e) =>
        {
            ShapeBox control = d as ShapeBox;

            if (control == null) return;

            if (e.OldValue is ObservableCollection<IShape> o)
            {
                o.CollectionChanged -= control.ShapesCollectionChanged;
            }

            if (e.NewValue is ObservableCollection<IShape> n)
            {
                n.CollectionChanged += control.ShapesCollectionChanged;
            }
            control.DrawShapes();
            control.OnShapesChanged();
        }));


    public IShape Shape
    {
        get { return (IShape)GetValue(ShapeProperty); }
        set { SetValue(ShapeProperty, value); }
    }

    public static readonly DependencyProperty ShapeProperty =
        DependencyProperty.Register("Shape", typeof(IShape), typeof(ShapeBox), new FrameworkPropertyMetadata(default(IShape), (d, e) =>
        {
            ShapeBox control = d as ShapeBox;

            if (control == null) return;

            if (e.OldValue is IShape o)
            {

            }

            if (e.NewValue is IShape n)
            {

            }

            control.DrawShapes();
            control.OnShapesChanged();
        }));

    protected virtual void OnShapesChanged()
    {
    }

    protected virtual void ShapesCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        this.Dispatcher.Invoke(() => this.DrawShapes());
    }

    public void DelayUpdateAll()
    {
        this.DelayInvoke(() => this.UpdateAll());
    }

    public virtual void UpdateAll()
    {
        this.UpdateImage();
        this.DrawShapes();
    }

    protected override void OnRender(DrawingContext drawingContext)
    {
        base.OnRender(drawingContext);
        this.DelayUpdateAll();
    }

    public virtual IEnumerable<IShape> GetShapes()
    {
        if (this.Shape != null)
            yield return this.Shape;
        if (this.Shapes != null)
        {
            foreach (var item in this.Shapes)
            {
                yield return item;
            }
        }
    }
    public virtual void DrawShapes()
    {
        this.UpdateDrawingVisualsTransform();
        using var drawingContext = this._shapeDrawingVisual.RenderOpen();
        var shapes = this.GetShapes()?.ToList();
        if (shapes == null || shapes.Count == 0)
            return;
        double strokeThickness = this.ToViewThickness(this.StrokeThickness);
        foreach (var shape in shapes)
        {
            this.DrawShape(shape, drawingContext, this.Stroke, strokeThickness, this.Fill);
        }
    }

    protected virtual void DrawShape(IShape shape, DrawingContext drawingContext, Brush stroke, double strokeThickness = 1, Brush fill = null)
    {
        shape.Draw(this, drawingContext, this.Stroke, strokeThickness, this.Fill);
    }

    public double ToThickness(ImageSource image)
    {
        if (image == null)
            return 1.0;
        double s = Math.Sqrt(image.Height * image.Height + image.Width * image.Width);
        return s / 200;
    }

    public double ToViewThickness(double thickness)
    {
        return thickness < 0 ? this.ToThickness(this.ImageSource) : thickness / this.Scale;

    }

    public Color PickColor(Point point)
    {
        point = this.ToImagePoint(point);
        if (this.ImageSource is BitmapSource bitmapSource)
            return this.GetPixelColor(bitmapSource, (int)point.X, (int)point.Y);
        return default;
    }

    protected virtual Point ToImagePoint(Point point)
    {
        return point;
    }

    private Color GetPixelColor(BitmapSource bitmap, int x, int y)
    {
        if (x < 0 || x >= bitmap.PixelWidth || y < 0 || y >= bitmap.PixelHeight)
            return default;
        byte[] pixel = new byte[4];
        bitmap.CopyPixels(new Int32Rect(x, y, 1, 1), pixel, 4, 0);
        return Color.FromArgb(255, pixel[2], pixel[1], pixel[0]);
    }
}
