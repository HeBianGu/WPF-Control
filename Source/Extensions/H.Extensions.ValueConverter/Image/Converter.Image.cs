// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

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

    public class GetImageSourceFromFilePathConverter : MarkupValueConverterBase
    {
        public int DecodePixel { get; set; } = 100;
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;
            string str = value.ToString();
            if (string.IsNullOrEmpty(str))
                return null;
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(str);
            bitmap.DecodePixelWidth = this.DecodePixel;  // 设置图片宽度为 100 像素
            bitmap.DecodePixelHeight = this.DecodePixel; // 设置图片高度为 100 像素
            bitmap.EndInit();
            return bitmap;
        }
    }

    public class GetImageSourceFromBase64Converter : MarkupValueConverterBase
    {
        public int DecodePixel { get; set; } = 100;
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;
            string base64String=value.ToString();
            // 将 Base64 字符串转换为字节数组
            byte[] imageBytes = System.Convert.FromBase64String(base64String);

            // 创建 BitmapImage
            BitmapImage bitmap = new BitmapImage();
            using (MemoryStream stream = new MemoryStream(imageBytes))
            {
                bitmap.BeginInit();
                bitmap.StreamSource = stream;
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.DecodePixelWidth = this.DecodePixel;  // 设置图片宽度为 100 像素
                bitmap.DecodePixelHeight = this.DecodePixel; // 设置图片高度为 100 像素
                bitmap.EndInit();
            }
            return bitmap;
        }
    }
}



