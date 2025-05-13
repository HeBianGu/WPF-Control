// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Styles;

public class LogoDataProvider
{
    public static ImageSource Logo
    {
        get
        {
            ImageSourceConverter converter = new ImageSourceConverter();
            string path = "pack://application:,,,/H.Style;component/Assets/Logo.ico";
            return converter.ConvertFromString(path) as ImageSource;
        }
    }
}
