// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Themes;

public static class LayoutKeys
{
    public static ComponentResourceKey RowHeight => new ComponentResourceKey(typeof(LayoutKeys), "S.Layout.RowHeight");
    public static ComponentResourceKey ItemHeight => new ComponentResourceKey(typeof(LayoutKeys), "S.Layout.ItemHeight");
    public static ComponentResourceKey WindowCaptionHeight => new ComponentResourceKey(typeof(LayoutKeys), "S.Layout.WindowCaptionHeight");
    public static ComponentResourceKey CornerRadius => new ComponentResourceKey(typeof(LayoutKeys), "S.Layout.CornerRadius");

    public static ComponentResourceKey Padding => new ComponentResourceKey(typeof(LayoutKeys), "S.Layout.Padding");

    public static ComponentResourceKey Margin => new ComponentResourceKey(typeof(LayoutKeys), "S.Layout.Margin");

    public static ComponentResourceKey IconHeight => new ComponentResourceKey(typeof(LayoutKeys), "S.Layout.IconHeight");

}

//public class SystemParametersExtension : MarkupExtension
//{
//    public double Value { get; set; }
//    public override object ProvideValue(IServiceProvider serviceProvider)
//    {
//        return this.Value.ToString();
//    }
//}
