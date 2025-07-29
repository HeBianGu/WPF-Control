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
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace H.Controls.ShapeBox
{
    public class ShapeBox : FrameworkElement
    {
        static ShapeBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ShapeBox), new FrameworkPropertyMetadata(typeof(ShapeBox)));
        }

        private VisualCollection _visualCollection;
        private ImageDrawingVisual _imageDrawingVisual = new ImageDrawingVisual();
        private ShapeDrawingVisual _shapeDrawingVisual = new ShapeDrawingVisual();
        private IState _currentState;
        public ShapeBox()
        {
            this._currentState = new DefaultRectState(this);
            this._visualCollection = new VisualCollection(this);
            this._visualCollection.Add(_imageDrawingVisual);
            this._visualCollection.Add(_shapeDrawingVisual);
            this.MouseDown += this.ShapeBox_MouseDown;
            this.MouseMove += this.ShapeBox_MouseMove;
            this.MouseUp += this.ShapeBox_MouseUp;
            this.MouseLeave += this.ShapeBox_MouseLeave;
            this.Loaded += this.ShapeBox_Loaded;
        }

        private void ShapeBox_Loaded(object sender, RoutedEventArgs e)
        {
            this.UpdateImage();
        }

        private void ShapeBox_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {

            _currentState?.MouseLeave(sender, e);
        }

        private void ShapeBox_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            _currentState?.MouseUp(sender, e);
        }

        private void ShapeBox_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {

            _currentState?.MouseMove(sender, e);
        }

        private void ShapeBox_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            _currentState?.MouseDown(sender, e);
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
            }));

        private void ShapesCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            this.DrawShapes();
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            this.DrawShapes();
        }

        void DrawShapes()
        {
            using var drawingContext = this._shapeDrawingVisual.RenderOpen();
            if (this.Shapes == null)
                return;
            double strokeThickness = this.StrokeThickness < 0 ? this.ToThickness(this.ImageSource) : this.StrokeThickness;
            foreach (var shape in this.Shapes)
            {
                shape.Draw(drawingContext, this.Stroke, strokeThickness, this.Fill);
            }
        }

        public double ToThickness(ImageSource image)
        {
            double s = Math.Sqrt(image.Height * image.Height + image.Width * image.Width);
            return s / 200;
        }
    }
}
