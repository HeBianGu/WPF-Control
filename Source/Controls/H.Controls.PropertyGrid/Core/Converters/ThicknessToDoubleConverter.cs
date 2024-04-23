// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System;
using System.Windows;
using System.Windows.Data;

namespace H.Controls.PropertyGrid
{
    public class ThicknessToDoubleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double thickness = 1.0;

            if (value != null)
                thickness = ((Thickness)value).Top;

            return thickness;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double thickness = 1.0;

            if (value != null)
                thickness = (double)value;

            return new Thickness(thickness);
        }
    }
}
