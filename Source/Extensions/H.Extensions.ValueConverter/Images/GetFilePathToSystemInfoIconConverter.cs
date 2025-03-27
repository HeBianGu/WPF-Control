// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Globalization;

namespace H.Extensions.ValueConverter.Images;

public class GetFilePathToSystemInfoIconConverter : MarkupValueConverterBase
{
    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null)
            return null;
        string str = value.ToString();
        //if (File.Exists(str) == false)
        //    return null;
        var icon = IconHelper.GetSystemInfoIcon(str);
        return IconHelper.GetIconToImageSource(icon);
    }
}



