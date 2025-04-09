using System.Globalization;
using System.Windows.Media;

namespace H.Extensions.ValueConverter;

public class GetGemeotryFromStringConverter : MarkupValueConverterBase
{
    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null)
            return null;
        return Geometry.Parse(value.ToString());
    }
}
