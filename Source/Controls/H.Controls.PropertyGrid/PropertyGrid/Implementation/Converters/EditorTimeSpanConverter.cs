// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System;
using System.Globalization;
using System.Windows.Data;

namespace H.Controls.PropertyGrid
{
    /// <summary>
    /// Converts a TimeSpan value to a DateTime value.
    /// 
    /// This converter can be used in conjunction with a TimePicker in order 
    /// to create a TimeSpan edit control. 
    /// </summary>
    public sealed class EditorTimeSpanConverter : IValueConverter
    {
        public bool AllowNulls { get; set; }

        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (this.AllowNulls && value == null)
                return null;

            TimeSpan timeSpan = (value != null) ? (TimeSpan)value : TimeSpan.Zero;
            return DateTime.Today + timeSpan;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (this.AllowNulls && value == null)
                return null;

            return (value != null)
              ? ((DateTime)value).TimeOfDay
              : TimeSpan.Zero;
        }
    }
}
