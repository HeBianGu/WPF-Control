// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System;
using System.Windows;
using System.Windows.Data;

namespace H.Controls.PropertyGrid
{
    public class IntToThicknessConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int thickValue = 0;
            if (value != null)
                thickValue = (int)value;

            if (parameter != null)
            {
                if (parameter.ToString().ToUpper() == "LEFT")
                    return new Thickness(thickValue, 0, 0, 0);
            }

            return new Thickness(thickValue);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
