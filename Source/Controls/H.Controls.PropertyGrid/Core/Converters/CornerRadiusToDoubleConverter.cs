// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System;
using System.Windows;
using System.Windows.Data;

namespace H.Controls.PropertyGrid
{
    public class CornerRadiusToDoubleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double radius = 0.0;

            if (value != null)
                radius = ((CornerRadius)value).TopLeft;

            return radius;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double radius = 0.0;

            if (value != null)
                radius = (double)value;

            return new CornerRadius(radius);
        }
    }
}
