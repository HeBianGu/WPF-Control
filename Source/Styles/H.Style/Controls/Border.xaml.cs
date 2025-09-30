// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Styles.Controls;

public class BorderKeys
{
    public static ComponentResourceKey Default => new ComponentResourceKey(typeof(BorderKeys), "S.Border.Default");
    public static ComponentResourceKey Effect => new ComponentResourceKey(typeof(BorderKeys), "S.Border.Effect");
    public static ComponentResourceKey BorderThickness => new ComponentResourceKey(typeof(BorderKeys), "S.Border.BorderThickness");
    public static ComponentResourceKey BorderBrush => new ComponentResourceKey(typeof(BorderKeys), "S.Border.BorderBrush");
    public static ComponentResourceKey Background => new ComponentResourceKey(typeof(BorderKeys), "S.Border.Background");
}
