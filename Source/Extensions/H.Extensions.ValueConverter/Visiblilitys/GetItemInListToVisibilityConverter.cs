// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Collections;
using System.Windows;

namespace H.Extensions.ValueConverter.Visiblilitys;

public class GetItemInListToVisibilityConverter : MarkupValueConverterBase
{
    public Visibility Visibility { get; set; } = Visibility.Visible;
    public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        if (parameter is IList list)
        {
            if (list.Contains(value))
                return this.Visibility;
        }
        return this.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
    }
}
