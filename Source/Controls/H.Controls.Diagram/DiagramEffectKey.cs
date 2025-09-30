// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.Diagram;

public class DiagramEffectKey
{
    public static ComponentResourceKey IsDragEnter => new ComponentResourceKey(typeof(DiagramEffectKey), "S.EffectShadow.IsDragEnter");
    public static ComponentResourceKey IsCanDrop => new ComponentResourceKey(typeof(DiagramEffectKey), "S.EffectShadow.IsCanDrop");
    public static ComponentResourceKey IsSelected => new ComponentResourceKey(typeof(DiagramEffectKey), "S.EffectShadow.IsSelected");
    public static ComponentResourceKey MouseOver => new ComponentResourceKey(typeof(DiagramEffectKey), "S.EffectShadow.MouseOver");
    public static ComponentResourceKey IsDragging => new ComponentResourceKey(typeof(DiagramEffectKey), "S.EffectShadow.IsDragging");

}
