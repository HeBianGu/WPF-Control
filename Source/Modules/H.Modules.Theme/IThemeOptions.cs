// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Themes.Backgrounds;
using H.Themes.FontSizes;
using H.Themes.Layouts;
using System.Windows.Media;

namespace H.Modules.Theme;
public interface IThemeOptions : IIconFontFamilysOptions, IColorThemeOptions
{
    IBackgroundResource BackgroundResource { get; set; }
    List<IBackgroundResource> BackgroundResources { get; }
    FontFamily FontFamily { get; set; }
    List<FontFamily> FontFamilys { get; }
    FontSizeThemeType FontSize { get; set; }
    LayoutThemeType Layout { get; set; }
}
