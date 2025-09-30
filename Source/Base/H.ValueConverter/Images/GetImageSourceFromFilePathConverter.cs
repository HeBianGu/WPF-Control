// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Globalization;

namespace H.ValueConverter.Images;

public class GetImageSourceFromFilePathConverter : MarkupValueConverterBase
{
    public int DecodePixel { get; set; } = 100;

    public string RelativeRoot { get; set; } = AppDomain.CurrentDomain.BaseDirectory;
    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null)
            return null;
        string str = value.ToString();
        if (string.IsNullOrEmpty(str))
            return null;

        if (!Path.IsPathRooted(str))
        {
            str = Path.Combine(this.RelativeRoot, str);
        }
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

