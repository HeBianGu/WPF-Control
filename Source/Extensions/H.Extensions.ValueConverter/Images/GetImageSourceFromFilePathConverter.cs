// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Globalization;
using System.Windows.Media.Imaging;

namespace H.Extensions.ValueConverter.Images;

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
        try
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(str, UriKind.RelativeOrAbsolute);
            bitmap.DecodePixelWidth = this.DecodePixel;  // 设置图片宽度为 100 像素
                                                         //bitmap.DecodePixelHeight = this.DecodePixel; // 设置图片高度为 100 像素
            bitmap.EndInit(); return bitmap;
        }
        catch (Exception)
        {
            return null;
        }

    }
}



