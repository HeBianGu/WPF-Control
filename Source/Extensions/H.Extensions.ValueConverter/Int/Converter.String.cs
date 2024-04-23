// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace H.Extensions.ValueConverter
{
    public class GetIntToValueConverter : MarkupValueConverterBase
    {
        public object F2 { get; set; }
        public object F1 { get; set; }
        public object Zore { get; set; }
        public object Z1 { get; set; }
        public object Z2 { get; set; }
        public object Z3 { get; set; }
        public object Z4 { get; set; }
        public object Z5 { get; set; }
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;
            int v = (int)value;
            if (v == -2)
                return F2;
            if (v == -1)
                return F1;
            if (v == 0)
                return Zore;
            if (v == 1)
                return Z1;
            if (v == 2)
                return Z2;
            if (v == 3)
                return Z3;
            if (v == 4)
                return Z4;
            if (v == 5)
                return Z5;
            return DependencyProperty.UnsetValue;
        }
    }

    /// <summary> 设置文本框PropertyChanged 可以输入小数点 </summary>
    public class GetDoubleTextConverter : MarkupValueConverterBase
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string str = value.ToString();
            if (str.EndsWith("."))
                return ".";
            if (str.Contains(".") && str.EndsWith("0"))
                return ".";
            return value;
        }
    }

    /// <summary> 分割字符串取第一个值 </summary>
    public class GetStringSplitIndexConverter : MarkupValueConverterBase
    {
        public int Index { get; set; } = 0;
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;
            if (parameter == null)
                return value.ToString().Split(' ')[Index];
            return value.ToString().Split(parameter.ToString().ToCharArray())[Index];
        }
    }

    /// <summary> 替换字符串 </summary>
    public class StringReplaceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;
            if (parameter == null)
                return value;
            return value.ToString().Replace(value.ToString().Split(' ')[0], value.ToString().Split(' ')[1]);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
