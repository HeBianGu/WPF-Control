﻿// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace H.Extensions.ValueConverter
{
    /// <summary> 百分比转换为角度值 </summary>
    public class PercentToAngleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double percent = double.Parse(value.ToString());
            if (percent >= 1) return 360.0D;
            return percent * 360;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


    public class ThicknessToDoubleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Thickness thickness = (Thickness)value;
            return thickness.Left;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }

    public class CornerRadiusToDouble : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter != null)
            {
                return ((CornerRadius)value).TopLeft * System.Convert.ToDouble(parameter);
            }
            return ((CornerRadius)value).TopLeft;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter != null)
            {
                return new CornerRadius((double)value / System.Convert.ToDouble(parameter));
            }
            return new CornerRadius((double)value);
        }
    }
}
