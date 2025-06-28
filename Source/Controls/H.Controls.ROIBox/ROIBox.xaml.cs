// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")
global using System.Windows;
using H.Controls.ROIBox.Drawings;
using H.Controls.ROIBox.State;
using System.Windows.Input;
using System.Windows.Media;

namespace H.Controls.ROIBox
{

    public class ROIBoxCommands
    {
        public static RoutedUICommand ClearRect = new RoutedUICommand() { Text = "清除" };
    }
    public class ROIBox : FrameworkElement
    {
        static ROIBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ROIBox), new FrameworkPropertyMetadata(typeof(ROIBox)));
        }

        private VisualCollection _visualCollection;
        private ImageDrawingVisual _imageDrawingVisual = new ImageDrawingVisual();
        private RectDrawingVisual _rectDrawingVisual;
        private IState _currentState;
        public ROIBox()
        {
            this._currentState = new DefaultRectState(this);
            this._visualCollection = new VisualCollection(this);
            this._rectDrawingVisual = new RectDrawingVisual(this);
            this._visualCollection.Add(_imageDrawingVisual);
            this._visualCollection.Add(_rectDrawingVisual);
            this.MouseDown += this.ROIBox_MouseDown;
            this.MouseMove += this.ROIBox_MouseMove;
            this.MouseUp += this.ROIBox_MouseUp;
            this.MouseLeave += this.ROIBox_MouseLeave;
            this.CommandBindings.Add(new CommandBinding(ROIBoxCommands.ClearRect, (s, e) =>
            {
                this.Rect = Rect.Empty;
            }));
            this.Loaded += this.ROIBox_Loaded;
        }

        private void ROIBox_Loaded(object sender, RoutedEventArgs e)
        {
            this.UpdateImage();
            this.UpdateRect();
        }

        private void ROIBox_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {

            _currentState?.MouseLeave(sender, e);
        }

        private void ROIBox_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            _currentState?.MouseUp(sender, e);
        }

        private void ROIBox_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {

            _currentState?.MouseMove(sender, e);
        }

        private void ROIBox_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            _currentState?.MouseDown(sender, e);
            e.Handled = true;
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
            DependencyProperty.Register("ImageSource", typeof(ImageSource), typeof(ROIBox), new FrameworkPropertyMetadata(default(ImageSource), (d, e) =>
            {
                ROIBox control = d as ROIBox;

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
            this._imageDrawingVisual.Draw();
        }

        public bool IsRectValid => !this.Rect.IsEmpty;

        public Rect BoundingBox
        {
            get
            {
                if (this.ImageSource == null)
                    return Rect.Empty;
                return new Rect(0, 0, this.ImageSource.Width, this.ImageSource.Height);
            }
        }

        public Rect Rect
        {
            get { return (Rect)GetValue(RectProperty); }
            set { SetValue(RectProperty, value); }
        }

        public static readonly DependencyProperty RectProperty =
            DependencyProperty.Register("Rect", typeof(Rect), typeof(ROIBox), new FrameworkPropertyMetadata(default(Rect), (d, e) =>
            {
                ROIBox control = d as ROIBox;

                if (control == null) return;

                if (e.OldValue is Rect o)
                {

                }

                if (e.NewValue is Rect n)
                {

                }
                control.UpdateRect();
            }));

        public double HandleLength
        {
            get { return (double)GetValue(HandleLengthProperty); }
            set { SetValue(HandleLengthProperty, value); }
        }

        public static readonly DependencyProperty HandleLengthProperty =
            DependencyProperty.Register("HandleLength", typeof(double), typeof(ROIBox), new FrameworkPropertyMetadata(6.0, (d, e) =>
            {
                ROIBox control = d as ROIBox;

                if (control == null) return;

                if (e.OldValue is double o)
                {

                }

                if (e.NewValue is double n)
                {

                }
                control.UpdateRect();
            }));

        public Brush Stroke
        {
            get { return (Brush)GetValue(StrokeProperty); }
            set { SetValue(StrokeProperty, value); }
        }

        public static readonly DependencyProperty StrokeProperty =
            DependencyProperty.Register("Stroke", typeof(Brush), typeof(ROIBox), new FrameworkPropertyMetadata(Brushes.Chartreuse, (d, e) =>
            {
                ROIBox control = d as ROIBox;

                if (control == null) return;

                if (e.OldValue is Brush o)
                {

                }

                if (e.NewValue is Brush n)
                {

                }
                control.UpdateRect();
            }));

        public double StrokeThickness
        {
            get { return (double)GetValue(StrokeThicknessProperty); }
            set { SetValue(StrokeThicknessProperty, value); }
        }

        public static readonly DependencyProperty StrokeThicknessProperty =
            DependencyProperty.Register("StrokeThickness", typeof(double), typeof(ROIBox), new FrameworkPropertyMetadata(1.0, (d, e) =>
            {
                ROIBox control = d as ROIBox;

                if (control == null) return;

                if (e.OldValue is double o)
                {

                }

                if (e.NewValue is double n)
                {

                }
                control.UpdateRect();
            }));

        public Brush Fill
        {
            get { return (Brush)GetValue(FillProperty); }
            set { SetValue(FillProperty, value); }
        }

        public static readonly DependencyProperty FillProperty =
            DependencyProperty.Register("Fill", typeof(Brush), typeof(ROIBox), new FrameworkPropertyMetadata(new SolidColorBrush(Colors.Black) { Opacity = 0.5 }, (d, e) =>
            {
                ROIBox control = d as ROIBox;

                if (control == null) return;

                if (e.OldValue is Brush o)
                {

                }

                if (e.NewValue is Brush n)
                {

                }
                control.UpdateRect();
            }));

        public bool UseCross
        {
            get { return (bool)GetValue(UseCrossProperty); }
            set { SetValue(UseCrossProperty, value); }
        }

        public static readonly DependencyProperty UseCrossProperty =
            DependencyProperty.Register("UseCross", typeof(bool), typeof(ROIBox), new FrameworkPropertyMetadata(true, (d, e) =>
            {
                ROIBox control = d as ROIBox;

                if (control == null) return;

                if (e.OldValue is bool o)
                {

                }

                if (e.NewValue is bool n)
                {

                }
                control.UpdateRect();
            }));

        private void UpdateRect()
        {
            this._rectDrawingVisual.Stroke = this.Stroke;
            this._rectDrawingVisual.StrokeThickness = this.StrokeThickness;
            this._rectDrawingVisual.HandleLength = this.HandleLength;
            this._rectDrawingVisual.Fill = this.Fill;
            this._rectDrawingVisual.Rect = this.Rect; 
            this._rectDrawingVisual.Draw();
        }

    }
}
