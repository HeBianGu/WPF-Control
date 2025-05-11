// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Modules.Theme;

namespace H.ApplicationBases.Themes
{
    public interface IDefaultThemeOptions
    {
        void UseColorThemeOptions(Action<IColorThemeOptions> action);
        void UseIconFontFamilysOptions(Action<IIconFontFamilysOptions> action);
        void UseThemeOptions(Action<IThemeOptions> action);
    }
}
