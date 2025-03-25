using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace H.Extensions.ValueConverter;

public abstract class DependencyConverterBase : DependencyObject, IValueConverter
{
    public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);
    public abstract object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture);
}
