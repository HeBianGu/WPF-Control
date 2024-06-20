﻿// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Windows;
using System.Windows.Media;

namespace H.Controls.Panel
{
    public class CirclePanel : PanelBase
    {

        public CirclePanel()
        {
            this.Loaded += (l, k) =>
            {
                this.InvalidateArrange();
            };

        }
        public double Len
        {
            get { return (double)GetValue(LenProperty); }
            set { SetValue(LenProperty, value); }
        }

        public static readonly DependencyProperty LenProperty =
            DependencyProperty.Register("Len", typeof(double), typeof(CirclePanel), new PropertyMetadata(100.0, (d, e) =>
            {
                CirclePanel control = d as CirclePanel;
                if (control == null)
                    return;
                control.InvalidateArrange();
            }));


        public bool AngleToCenter
        {
            get { return (bool)GetValue(AngleToCenterProperty); }
            set { SetValue(AngleToCenterProperty, value); }
        }

        public static readonly DependencyProperty AngleToCenterProperty =
            DependencyProperty.Register("AngleToCenter", typeof(bool), typeof(CirclePanel), new PropertyMetadata(default(bool), (d, e) =>
             {
                 CirclePanel control = d as CirclePanel;
                 if (control == null)
                     return;
                 control.InvalidateArrange();

             }));


        protected override Size ArrangeOverride(Size finalSize)
        {
            System.Collections.Generic.List<UIElement> children = this.GetChildren();
            Point center = new Point(finalSize.Width / 2, finalSize.Height / 2);
            Point start = new Point(finalSize.Width / 2 + this.Len, finalSize.Height / 2);
            for (int i = 0; i < children.Count; i++)
            {
                UIElement elment = children[i];
                double angle = -(360.0 / children.Count) * i;
                Matrix matrix = new Matrix();
                matrix.RotateAt(angle, center.X, center.Y);
                Point end = matrix.Transform(start);
                elment.Measure(finalSize);
                elment.RenderTransformOrigin = new Point(0.5, 0.5);
                end = new Point(end.X - elment.DesiredSize.Width / 2, end.Y - elment.DesiredSize.Height / 2);
                elment.Arrange(new Rect(end, elment.DesiredSize));
            }
            return base.ArrangeOverride(finalSize);
        }
        protected override Size MeasureOverride(Size availableSize)
        {
            Size value = base.MeasureOverride(availableSize);
            bool isAll = false;
            foreach (UIElement item in this.Children)
            {
                if (item.Visibility == Visibility.Visible)
                {
                    isAll = true;
                    break;
                }
            }
            if (!isAll)
                return value;
            return new Size(this.Len * 2, this.Len * 2);
        }
    }
}
