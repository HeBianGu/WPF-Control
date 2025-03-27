// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control



using H.Extensions.ValueConverter;
using System.Globalization;
using System.Reflection;

namespace H.Modules.Guide;

public class GetIsNewAssemblyVersionConverter : MarkupValueConverterBase
{
    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var version = Assembly.GetEntryAssembly().GetName().Version.ToString();
        return version == value?.ToString();
    }
}
