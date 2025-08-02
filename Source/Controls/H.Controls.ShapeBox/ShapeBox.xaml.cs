// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")
global using System.Windows;
using H.Controls.ShapeBox.Drawings;
using H.Controls.ShapeBox.Shapes.Base;
using H.Controls.ShapeBox.State;
using H.Controls.ShapeBox.State.Base;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using System.Windows.Media.TextFormatting;
using System.Windows.Threading;

namespace H.Controls.ShapeBox
{
    public class ShapeBox : FrameworkElement, IView
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

        private void UpdateImage()
        {
            this.Width = this.ImageSource?.Width ?? 0;
            this.Height = this.ImageSource?.Height ?? 0;
            this._imageDrawingVisual.ImageSource = this.ImageSource;
            //this.Shapes.Clear();
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

        protected virtual void OnShapesChanged()
        {
        }

        private void ShapesCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
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

        void DrawShapes()
        {
            using var drawingContext = this._shapeDrawingVisual.RenderOpen();
            if (this.Shapes == null || this.Shapes.Count == 0)
                return;
            double strokeThickness = this.ToViewThickness(this.StrokeThickness);
            foreach (var shape in this.Shapes)
            {
                shape.Draw(this, drawingContext, this.Stroke, strokeThickness, this.Fill);
            }
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
    }
}
