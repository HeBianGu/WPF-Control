// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace H.Extensions.TypeConverter
{
    public class SizeToDisplayTypeConverter : System.ComponentModel.TypeConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (long.TryParse(value?.ToString(), out long result))
            {
                return FormatBytes(result);
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            return FormatToBytes(value?.ToString());
        }

        private string FormatBytes(long bytes)
        {
            string[] Suffix = { "Byte", "KB", "MB", "GB", "TB" };
            int i = 0;
            double dblSByte = bytes;

            if (bytes > 1024)
                for (i = 0; (bytes / 1024) > 0; i++, bytes /= 1024)
                    dblSByte = bytes / 1024.0;

            return String.Format("{0:0.##}{1}", dblSByte, Suffix[i]);
        }

        private long FormatToBytes(string str)
        {
            if (str == null)
                return 0;
            str = str.Trim();
            if (str.EndsWith("Byte", StringComparison.OrdinalIgnoreCase))
            {
                if (long.TryParse(str.Trim("Byte".ToArray()), out long value))
                    return value;
            }
            if (str.EndsWith("KB", StringComparison.OrdinalIgnoreCase))
            {
                if (long.TryParse(str.Trim("KB".ToArray()), out long value))
                    return value * 1024;
            }
            if (str.EndsWith("MB", StringComparison.OrdinalIgnoreCase))
            {
                if (long.TryParse(str.Trim("MB".ToArray()), out long value))
                    return value * 1024 * 1024;
            }
            if (str.EndsWith("GB", StringComparison.OrdinalIgnoreCase))
            {
                if (long.TryParse(str.Trim("GB".ToArray()), out long value))
                    return value * 1024 * 1024 * 1024;
            }
            if (str.EndsWith("TB", StringComparison.OrdinalIgnoreCase))
            {
                if (long.TryParse(str.Trim("TB".ToArray()), out long value))
                    return value * 1024 * 1024 * 1024 * 1024;
            }
            return 0;
        }
    }
}
