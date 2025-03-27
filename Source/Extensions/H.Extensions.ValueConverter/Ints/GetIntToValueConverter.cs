// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Globalization;
using System.Windows;

namespace H.Extensions.ValueConverter.Ints;

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
            return this.F2;
        if (v == -1)
            return this.F1;
        if (v == 0)
            return this.Zore;
        if (v == 1)
            return this.Z1;
        if (v == 2)
            return this.Z2;
        if (v == 3)
            return this.Z3;
        if (v == 4)
            return this.Z4;
        if (v == 5)
            return this.Z5;
        return DependencyProperty.UnsetValue;
    }
}
