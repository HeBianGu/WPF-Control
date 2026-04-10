// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Globalization;
using System.Windows.Media.Imaging;

namespace H.ValueConverter.Images;

public class GetImageSourceFromFilePathConverter : MarkupValueConverterBase
{
    private static List<Tuple<string, int, int, BitmapImage>> fileCache = new List<Tuple<string, int, int, BitmapImage>>();

    public int DecodePixel { get; set; } = 100;
    public bool UseCache { get; set; }
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

        if (this.UseCache)
        {
            Tuple<string, int, int, BitmapImage> find = fileCache.FirstOrDefault(k => k.Item1 == str && k.Item2 == DecodePixel && k.Item3 == DecodePixel);
            if (find != null && find.Item4 != null)
                return find.Item4;
        }
        try
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(str, UriKind.RelativeOrAbsolute);
            bitmap.DecodePixelWidth = this.DecodePixel;  // 设置图片宽度为 100 像素
                                                         //bitmap.DecodePixelHeight = this.DecodePixel; // 设置图片高度为 100 像素
            bitmap.EndInit();
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.Freeze();
            System.Diagnostics.Debug.WriteLine("加载图像:" + str);
            fileCache.Add(Tuple.Create(str, this.DecodePixel, this.DecodePixel, bitmap));
            return bitmap;
        }
        catch (Exception)
        {
            return null;
        }

    }

    public static void Clear()
    {
        fileCache.Clear();
    }
}

