// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Windows;

namespace H.Extensions.Attach
{
    public static partial class Cattach
    {

        public static double GetFromDouble(DependencyObject obj)
        {
            return (double)obj.GetValue(FromDoubleProperty);
        }

        public static void SetFromDouble(DependencyObject obj, double value)
        {
            obj.SetValue(FromDoubleProperty, value);
        }

       
        public static readonly DependencyProperty FromDoubleProperty =
            DependencyProperty.RegisterAttached("FromDouble", typeof(double), typeof(Cattach), new PropertyMetadata(default(double), OnFromDoubleChanged));

        public static void OnFromDoubleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d;

            double n = (double)e.NewValue;

            double o = (double)e.OldValue;
        }


        public static double GetToDouble(DependencyObject obj)
        {
            return (double)obj.GetValue(ToDoubleProperty);
        }

        public static void SetToDouble(DependencyObject obj, double value)
        {
            obj.SetValue(ToDoubleProperty, value);
        }

       
        public static readonly DependencyProperty ToDoubleProperty =
            DependencyProperty.RegisterAttached("ToDouble", typeof(double), typeof(Cattach), new PropertyMetadata(default(double), OnToDoubleChanged));

        public static void OnToDoubleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DependencyObject control = d;

            double n = (double)e.NewValue;

            double o = (double)e.OldValue;
        }


    }
}
