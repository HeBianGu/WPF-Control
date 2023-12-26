// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using HeBianGu.App.Tool;
using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace H.Extensions.ValueConverter
{
    [ValueConversion(typeof(Icon), typeof(ImageSource))]
    public class GetIconToImageSourceConverter : MarkupValueConverterBase
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;
            Icon icon = (Icon)value;
            ImageSource imageSource =
                System.Windows.Interop.Imaging.CreateBitmapSourceFromHIcon(
                icon.Handle,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());
            return imageSource;
        }
    }

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
}



