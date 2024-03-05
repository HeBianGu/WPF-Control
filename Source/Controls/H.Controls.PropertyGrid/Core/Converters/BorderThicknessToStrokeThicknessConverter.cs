// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System;
using System.Windows;
using System.Windows.Data;

namespace H.Controls.PropertyGrid
{
    public class BorderThicknessToStrokeThicknessConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Thickness thickness = (Thickness)value;
            return (thickness.Bottom + thickness.Left + thickness.Right + thickness.Top) / 4;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int? thick = (int?)value;
            int thickValue = thick.HasValue ? thick.Value : 0;

            return new Thickness(thickValue, thickValue, thickValue, thickValue);
        }

        #endregion
    }
}
