// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace H.Controls.PropertyGrid
{
    /// <summary>
    /// Sets the margin for the thumb grip, the top buttons, or for the content border in the WindowControl.
    /// </summary>
    public class WindowContentBorderMarginConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            double horizontalContentBorderOffset = (double)values[0];
            double verticalContentBorderOffset = (double)values[1];

            switch ((string)parameter)
            {
                // Content Border Margin in the WindowControl
                case "0":
                    return new Thickness(horizontalContentBorderOffset
                                        , 0d
                                        , horizontalContentBorderOffset
                                        , verticalContentBorderOffset);
                // Thumb Grip Margin in the WindowControl
                case "1":
                    return new Thickness(0d
                                        , 0d
                                        , horizontalContentBorderOffset
                                        , verticalContentBorderOffset);
                // Header Buttons Margin in the WindowControl
                case "2":
                    return new Thickness(0d
                                        , 0d
                                        , horizontalContentBorderOffset
                                        , 0d);
                default:
                    throw new NotSupportedException("'parameter' for WindowContentBorderMarginConverter is not valid.");
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
