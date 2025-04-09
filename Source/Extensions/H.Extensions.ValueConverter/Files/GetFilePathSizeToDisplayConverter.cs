// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using H.Extensions.Common;
using System.Globalization;

namespace H.Extensions.ValueConverter.Files;

public class GetFilePathSizeToDisplayConverter : MarkupValueConverterBase
{
    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null)
            return null;
        return value.ToString().ToFileEx().GetFileSizeToDisplay();
    }
}
