// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using H.Extensions.ValueConverter;


using System;
using System.Globalization;
using System.Linq;
using System.Windows;

namespace H.Controls.PagerBox
{
    public class GetPageIndexsConverter : MarkupValueConverterBase
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int v)
                return Enumerable.Range(1, v);
            return DependencyProperty.UnsetValue;
        }
    }
}
