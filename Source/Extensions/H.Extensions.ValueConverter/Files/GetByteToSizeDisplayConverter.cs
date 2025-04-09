// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Globalization;

namespace H.Extensions.ValueConverter.Files;

/// <summary> 转换成GB MB KB 显示 </summary> 
public class GetByteToSizeDisplayConverter : MarkupValueConverterBase
{
    private const double TB = 1024 * 1024 * 1024 * 1024.0;
    private const int GB = 1024 * 1024 * 1024;
    private const int MB = 1024 * 1024;
    private const int KB = 1024;
    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (long.TryParse(value?.ToString(), out long KSize))
        {
            bool isMinus = KSize < 0;
            string result;
            KSize = Math.Abs(KSize);
            if (KSize / TB >= 1)
                result = Math.Round(KSize / (float)TB, 2).ToString() + "T";
            else if (KSize / GB >= 1)
                result = Math.Round(KSize / (float)GB, 2).ToString() + "G";
            else if (KSize / MB >= 1)

                result = Math.Round(KSize / (float)MB, 2).ToString() + "MB";
            else if (KSize / KB >= 1)

                result = Math.Round(KSize / (float)KB, 2).ToString() + "KB";
            else
                result = KSize.ToString() + "Byte";
            return isMinus ? "-" + result : result;
        }

        return value;
    }
}
