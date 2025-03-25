// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Globalization;
using System.IO;
using System.Windows.Media.Imaging;

namespace H.Extensions.ValueConverter.Images;

public class GetImageSourceFromBase64Converter : MarkupValueConverterBase
{
    public int DecodePixel { get; set; } = 100;
    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null)
            return null;
        string base64String = value.ToString();
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



