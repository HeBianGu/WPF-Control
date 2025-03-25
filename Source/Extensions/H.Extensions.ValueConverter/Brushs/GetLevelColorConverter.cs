// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace H.Extensions.ValueConverter.Brushs;

[ValueConversion(typeof(string), typeof(int))]
public class GetLevelColorConverter : MarkupValueConverterBase
{
    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null)
            return Colors.Gray;
        if (string.IsNullOrEmpty(value.ToString().Trim()))
            return Colors.Gray;
        int v = int.Parse(value.ToString());
        switch (v)
        {
            case 1:
                return Colors.Purple.ToString();
            case 2:
                return Colors.Red.ToString();
            case 3:
                return Colors.Yellow.ToString();
            case 4:
                return Colors.Blue.ToString();
            case 5:
                return Colors.Gray.ToString();
            default:
                return Colors.Gray.ToString();
        }
    }
}
