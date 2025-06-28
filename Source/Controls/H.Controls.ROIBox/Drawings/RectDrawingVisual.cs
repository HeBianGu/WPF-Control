// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")
using System.Diagnostics;
using System.Windows.Media;

namespace H.Controls.ROIBox.Drawings
{
    public class RectDrawingVisual : DrawingVisual
    {
        private readonly ROIBox _box;
        public RectDrawingVisual(ROIBox box)
        {
            this._box = box;
        }
        public Rect Rect
        {
            get { return (Rect)GetValue(RectProperty); }
            set { SetValue(RectProperty, value); }
        }

        public static readonly DependencyProperty RectProperty =
            DependencyProperty.Register("Rect", typeof(Rect), typeof(RectDrawingVisual), new FrameworkPropertyMetadata(default(Rect), (d, e) =>
            {
                RectDrawingVisual control = d as RectDrawingVisual;

                if (control == null) return;

                if (e.OldValue is Rect o)
                {

                }

                if (e.NewValue is Rect n)
                {

                }
                control.Draw();
            }));

        public Brush Stroke
        {
            get { return (Brush)GetValue(StrokeProperty); }
            set { SetValue(StrokeProperty, value); }
        }

        public static readonly DependencyProperty StrokeProperty =
            DependencyProperty.Register("Stroke", typeof(Brush), typeof(RectDrawingVisual), new FrameworkPropertyMetadata(Brushes.Chartreuse, (d, e) =>
            {
                RectDrawingVisual control = d as RectDrawingVisual;

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
            DependencyProperty.Register("StrokeThickness", typeof(double), typeof(RectDrawingVisual), new FrameworkPropertyMetadata(1.0, (d, e) =>
            {
                RectDrawingVisual control = d as RectDrawingVisual;

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
            DependencyProperty.Register("Fill", typeof(Brush), typeof(RectDrawingVisual), new FrameworkPropertyMetadata(new SolidColorBrush(Colors.Black) { Opacity = 0.5 }, (d, e) =>
            {
                RectDrawingVisual control = d as RectDrawingVisual;

                if (control == null) return;

                if (e.OldValue is Brush o)
                {

                }

                if (e.NewValue is Brush n)
                {

                }

            }));

        public double HandleLength
        {
            get { return (double)GetValue(HandleLengthProperty); }
            set { SetValue(HandleLengthProperty, value); }
        }

        public static readonly DependencyProperty HandleLengthProperty =
            DependencyProperty.Register("HandleLength", typeof(double), typeof(RectDrawingVisual), new FrameworkPropertyMetadata(6.0, (d, e) =>
            {
                ROIBox control = d as ROIBox;

                if (control == null) return;

                if (e.OldValue is double o)
                {

                }

                if (e.NewValue is double n)
                {

                }

            }));

        public void Draw()
        {
            using var dc = this.RenderOpen();
            var combine = new CombinedGeometry(GeometryCombineMode.Exclude, new RectangleGeometry(this._box.BoundingBox), new RectangleGeometry(this.Rect));
            dc.PushClip(combine);
            dc.DrawRectangle(this.Fill, null, this._box.BoundingBox);
            dc.Pop();
            dc.DrawRectangle(Brushes.Transparent, new Pen(this.Stroke, this.StrokeThickness), this.Rect);

            var center = new Point(this.Rect.Left + this.Rect.Width / 2, this.Rect.Top + this.Rect.Height / 2);

          

            if (this._box.UseCross)
            {
                DashStyle dashStyle = new DashStyle();
                dashStyle.Dashes = new DoubleCollection(new double[] { this.StrokeThickness * 1, this.StrokeThickness * 1 });
                var dashpen = new Pen(this.Stroke, this.StrokeThickness / 2) { DashStyle = dashStyle };
                //dc.DrawLine(dashpen, new Point(0, center.Y), new Point(this._box.BoundingBox.Right, center.Y));
                //dc.DrawLine(dashpen, new Point(center.X, 0), new Point(center.X, this._box.BoundingBox.Bottom));
                dc.DrawLine(dashpen, new Point(0, center.Y), new Point(this.Rect.Left, center.Y));
                dc.DrawLine(dashpen, new Point(this.Rect.Right, center.Y), new Point(this._box.BoundingBox.Right, center.Y));
                dc.DrawLine(dashpen, new Point(center.X, 0), new Point(center.X, this.Rect.Top));
                dc.DrawLine(dashpen, new Point(center.X, this.Rect.Bottom), new Point(center.X, this._box.BoundingBox.Bottom));
            }


            double hthickness = this.HandleLength / 3;
            dc.DrawHandle(this.Rect.TopLeft, this.Stroke, hthickness, this.HandleLength);
            dc.DrawHandle(this.Rect.BottomRight, this.Stroke, hthickness, this.HandleLength);
            dc.DrawHandle(this.Rect.TopRight, this.Stroke, hthickness, this.HandleLength);
            dc.DrawHandle(this.Rect.BottomLeft, this.Stroke, hthickness, this.HandleLength);

            dc.DrawHandle(new Point(this.Rect.Left, center.Y), this.Stroke, hthickness, this.HandleLength);
            dc.DrawHandle(new Point(this.Rect.Right, center.Y), this.Stroke, hthickness, this.HandleLength);
            dc.DrawHandle(new Point(center.X, this.Rect.Top), this.Stroke, hthickness, this.HandleLength);
            dc.DrawHandle(new Point(center.X, this.Rect.Bottom), this.Stroke, hthickness, this.HandleLength);

            FormattedText formattedText = new FormattedText(
                $"{(int)this.Rect.Left},{(int)this.Rect.Top},{(int)this.Rect.Width},{(int)this.Rect.Height}",
                System.Globalization.CultureInfo.CurrentCulture,
                FlowDirection.LeftToRight,
                new Typeface("Microsoft YaHei"),
                10,
                Brushes.White,
                VisualTreeHelper.GetDpi(this).PixelsPerDip);
            dc.DrawText(formattedText, this.Rect.BottomLeft);

        }
    }

    public static class DrawingContextExtension
    {
        public static void DrawHandle(this DrawingContext dc, Point point, Brush stroke, double strokeThickness = 1, double len = 6)
        {
            double s = len / 2;
            dc.DrawRectangle(Brushes.White, new Pen(stroke, strokeThickness), new Rect(point.X - s, point.Y - s, len, len));
        }
    }
}
