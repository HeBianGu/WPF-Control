// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

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

