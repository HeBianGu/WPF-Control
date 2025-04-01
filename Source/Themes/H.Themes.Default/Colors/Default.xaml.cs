// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Windows;

namespace H.Themes.Default.Colors;

public static class ColorKeys
{
    #region - Background -
    public static ComponentResourceKey CaptionBackground => new ComponentResourceKey(typeof(ColorKeys), "S.Color.CaptionBackground");
    public static ComponentResourceKey Background => new ComponentResourceKey(typeof(ColorKeys), "S.Color.TextBackground");
    public static ComponentResourceKey AlternatingRowBackground => new ComponentResourceKey(typeof(ColorKeys), "S.Color.RowIndex.BackGround");
    #endregion

    public static ComponentResourceKey CaptionForeground => new ComponentResourceKey(typeof(ColorKeys), "S.Color.CaptionForeground");
    public static ComponentResourceKey Foreground => new ComponentResourceKey(typeof(ColorKeys), "S.Color.TextForeground");
    public static ComponentResourceKey ForegroundTitle => new ComponentResourceKey(typeof(ColorKeys), "S.Color.TextForeground.Title");
    public static ComponentResourceKey ForegroundSelect => new ComponentResourceKey(typeof(ColorKeys), "S.Color.TextForeground.Select");

    #region - Foreground -

    public static ComponentResourceKey MouseOver => new ComponentResourceKey(typeof(ColorKeys), "S.Color.TextMouseOver");
    public static ComponentResourceKey Selected => new ComponentResourceKey(typeof(ColorKeys), "S.Color.TextSelected");
    public static ComponentResourceKey ForegroundAssist => new ComponentResourceKey(typeof(ColorKeys), "S.Color.TextForeground.Assist");
    public static ComponentResourceKey ForegroundLink => new ComponentResourceKey(typeof(ColorKeys), "S.Color.TextForeground.Link");
   
    #endregion

    #region - BorderBrush -

    public static ComponentResourceKey BorderBrush => new ComponentResourceKey(typeof(ColorKeys), "S.Color.TextBorderBrush");
    public static ComponentResourceKey BorderBrushTitle => new ComponentResourceKey(typeof(ColorKeys), "S.Color.TextBorderBrush.Title");
    public static ComponentResourceKey BorderBrushAssist => new ComponentResourceKey(typeof(ColorKeys), "S.Color.TextBorderBrush.Assist");
    #endregion

    #region - Accent -
    public static ComponentResourceKey Accent => new ComponentResourceKey(typeof(ColorKeys), "S.Color.Accent");
    #endregion 

    #region - Dark - 
    public static ComponentResourceKey Dark10 => new ComponentResourceKey(typeof(ColorKeys), "S.Color.Dark.28");
    public static ComponentResourceKey Dark9_5 => new ComponentResourceKey(typeof(ColorKeys), "S.Color.Dark.27");
    public static ComponentResourceKey Dark9 => new ComponentResourceKey(typeof(ColorKeys), "SS.Color.Dark.26");
    public static ComponentResourceKey Dark8_5 => new ComponentResourceKey(typeof(ColorKeys), "S.Color.Dark.25");
    public static ComponentResourceKey Dark8 => new ComponentResourceKey(typeof(ColorKeys), "S.Color.Dark.24");
    public static ComponentResourceKey Dark7_5 => new ComponentResourceKey(typeof(ColorKeys), "S.Color.Dark.23");
    public static ComponentResourceKey Dark7 => new ComponentResourceKey(typeof(ColorKeys), "SS.Color.Dark.22");
    public static ComponentResourceKey Dark6_5 => new ComponentResourceKey(typeof(ColorKeys), "S.Color.Dark.21");
    public static ComponentResourceKey Dark6 => new ComponentResourceKey(typeof(ColorKeys), "S.Color.Dark.20");
    public static ComponentResourceKey Dark5_5 => new ComponentResourceKey(typeof(ColorKeys), "S.Color.Dark.19");
    public static ComponentResourceKey Dark5 => new ComponentResourceKey(typeof(ColorKeys), "S.Color.Dark.18");
    public static ComponentResourceKey Dark4_5 => new ComponentResourceKey(typeof(ColorKeys), "SS.Color.Dark.17");
    public static ComponentResourceKey Dark4 => new ComponentResourceKey(typeof(ColorKeys), "S.Color.Dark.16");
    public static ComponentResourceKey Dark3_5 => new ComponentResourceKey(typeof(ColorKeys), "S.Color.Dark.15");
    public static ComponentResourceKey Dark3 => new ComponentResourceKey(typeof(ColorKeys), "S.Color.Dark.14");
    public static ComponentResourceKey Dark2_5 => new ComponentResourceKey(typeof(ColorKeys), "S.Color.Dark.13");
    public static ComponentResourceKey Dark2 => new ComponentResourceKey(typeof(ColorKeys), "S.Color.Dark.12");
    public static ComponentResourceKey Dark1_5 => new ComponentResourceKey(typeof(ColorKeys), "S.Color.Dark.11");
    public static ComponentResourceKey Dark1 => new ComponentResourceKey(typeof(ColorKeys), "S.Color.Dark.10");
    public static ComponentResourceKey Dark0_9 => new ComponentResourceKey(typeof(ColorKeys), "S.Color.Dark.9");
    public static ComponentResourceKey Dark0_8 => new ComponentResourceKey(typeof(ColorKeys), "S.Color.Dark.8");
    public static ComponentResourceKey Dark0_7 => new ComponentResourceKey(typeof(ColorKeys), "S.Color.Dark.7");
    public static ComponentResourceKey Dark0_6 => new ComponentResourceKey(typeof(ColorKeys), "S.Color.Dark.6");
    public static ComponentResourceKey Dark0_5 => new ComponentResourceKey(typeof(ColorKeys), "S.Color.Dark.5");
    public static ComponentResourceKey Dark0_4 => new ComponentResourceKey(typeof(ColorKeys), "S.Color.Dark.4");
    public static ComponentResourceKey Dark0_3 => new ComponentResourceKey(typeof(ColorKeys), "S.Color.Dark.3");
    public static ComponentResourceKey Dark0_2 => new ComponentResourceKey(typeof(ColorKeys), "S.Color.Dark.2");
    public static ComponentResourceKey Dark0_1 => new ComponentResourceKey(typeof(ColorKeys), "S.Color.Dark.1");
    public static ComponentResourceKey Dark0 => new ComponentResourceKey(typeof(ColorKeys), "S.Color.Dark.0");
    #endregion 

    #region - Color -
    public static ComponentResourceKey LightGray => new ComponentResourceKey(typeof(ColorKeys), "S.Color.LightGray");
    public static ComponentResourceKey Gray => new ComponentResourceKey(typeof(ColorKeys), "S.Color.Gray");
    public static ComponentResourceKey Black => new ComponentResourceKey(typeof(ColorKeys), "S.Color.Black");
    public static ComponentResourceKey Orange => new ComponentResourceKey(typeof(ColorKeys), "S.Color.Orange");
    public static ComponentResourceKey Red => new ComponentResourceKey(typeof(ColorKeys), "S.Color.Red");
    public static ComponentResourceKey Green => new ComponentResourceKey(typeof(ColorKeys), "S.Color.Green");
    public static ComponentResourceKey Yellow => new ComponentResourceKey(typeof(ColorKeys), "S.Color.Yellow");
    public static ComponentResourceKey Blue => new ComponentResourceKey(typeof(ColorKeys), "S.Color.Blue");
    public static ComponentResourceKey Purple => new ComponentResourceKey(typeof(ColorKeys), "S.Color.Purple");
    public static ComponentResourceKey Brown => new ComponentResourceKey(typeof(ColorKeys), "S.Color.Brown");
    public static ComponentResourceKey LightBlue => new ComponentResourceKey(typeof(ColorKeys), "S.Color.LightBlue");
    public static ComponentResourceKey Pink => new ComponentResourceKey(typeof(ColorKeys), "S.Color.Pink");

    #endregion

    #region - System -
    public static ComponentResourceKey DialogCover => new ComponentResourceKey(typeof(ColorKeys), "S.Color.Dialog.Cover");

    #endregion

    public static ComponentResourceKey Tranparent => new ComponentResourceKey(typeof(ColorKeys), "S.Color.Tranparent");
}
