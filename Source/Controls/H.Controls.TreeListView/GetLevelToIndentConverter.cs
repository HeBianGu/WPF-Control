// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using H.Extensions.ValueConverter;
using System;
using System.Globalization;
using System.Windows;

namespace H.Controls.TreeListView
{
    //public class LevelToIndentConverter : IValueConverter
    //{
    //    public object Convert(object o, Type type, object parameter, CultureInfo culture)
    //    {
    //        return new Thickness((int)o * c_IndentSize, 0, 0, 0);
    //    }

    //    public object ConvertBack(object o, Type type, object parameter, CultureInfo culture)
    //    {
    //        throw new NotSupportedException();
    //    }

    //    private const double c_IndentSize = 19.0;
    //}

    public class GetLevelToIndentConverter : MarkupValueConverterBase
    {
        public double IndentSize { get; set; } = 19.0;

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new Thickness((int)value * this.IndentSize, 0, 0, 0);
        }
    }
}
