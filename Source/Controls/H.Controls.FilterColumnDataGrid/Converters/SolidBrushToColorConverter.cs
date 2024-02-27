
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace H.Controls.FilterColumnDataGrid
{
    [ValueConversion(typeof(SolidColorBrush), typeof(Color))]
    public class SolidBrushToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is SolidColorBrush)) return null;
            var result = (SolidColorBrush)value;
            return result.Color;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}