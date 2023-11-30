// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System;
using System.Globalization;
using System.Windows.Data;

namespace H.Extensions.ValueConverter
{
    public class GetBoolToValueConverter : MarkupValueConverterBase
    {
        public object TrueValue { get; set; }
        public object FalseValue { get; set; }
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool b)
                return b ? TrueValue : FalseValue;
            return DefaultValue;
        }
    }
}
