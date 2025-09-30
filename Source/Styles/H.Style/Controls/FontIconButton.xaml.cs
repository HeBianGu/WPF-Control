// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Styles.Controls;

public class FontIconButton : Button
{
    static FontIconButton()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(FontIconButton), new FrameworkPropertyMetadata(typeof(FontIconButton)));
    }
}

public class FontIconButtonKeys
{
    public static ComponentResourceKey Default => new ComponentResourceKey(typeof(FontIconButtonKeys), "S.FontIconButton.Default");

    public static ComponentResourceKey Command => new ComponentResourceKey(typeof(FontIconButtonKeys), "S.FontIconButton.Command");

}
