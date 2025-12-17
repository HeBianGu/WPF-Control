// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Extensions.ValueConverter.Files;

public class GetFileImageSourceInMemoryConverter : MarkupValueConverterBase
{
    public int DecodePixelWidth { get; set; } = 0;
    public int DecodePixelHeight { get; set; } = 0;
    public bool UseCache { get; set; } = true;
    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null)
            return null;
        string filePath = value.ToString();
        return filePath.ToImageEx().GetImageSource(this.DecodePixelWidth, this.DecodePixelHeight, this.UseCache);
    }
}
