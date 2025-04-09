// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Windows;

namespace H.Themes;

public static class BrushKeys
{
    #region - Background -
    public static ComponentResourceKey CaptionBackground => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.CaptionBackground");

    public static ComponentResourceKey Background => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.TextBackground");

    [Obsolete]
    public static ComponentResourceKey BackgroundDisabled => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.TextBackground.Disabled");
    public static ComponentResourceKey AlternatingRowBackground => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.RowIndex.BackGround");
    #endregion

    public static ComponentResourceKey CaptionForeground => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.CaptionForeground");
    public static ComponentResourceKey Foreground => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.TextForeground");
    public static ComponentResourceKey ForegroundTitle => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.TextForeground.Title");
    public static ComponentResourceKey ForegroundSelect => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.TextForeground.Select");

    #region - Foreground -

    public static ComponentResourceKey MouseOver => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.TextMouseOver");
    public static ComponentResourceKey Selected => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.TextSelected");
    public static ComponentResourceKey ForegroundAssist => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.TextForeground.Assist");
    public static ComponentResourceKey ForegroundLink => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.TextForeground.Link");

    [Obsolete]
    public static ComponentResourceKey ForegroundWhite => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.TextForeground.White");
    [Obsolete]
    public static ComponentResourceKey ForegroundWhiteOpacity9 => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.TextForeground.White.Opacity.9");
    [Obsolete]
    public static ComponentResourceKey ForegroundWhiteOpacity8 => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.TextForeground.White.Opacity.8");
    [Obsolete]
    public static ComponentResourceKey ForegroundWhiteOpacity7 => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.TextForeground.White.Opacity.7");
    [Obsolete]
    public static ComponentResourceKey ForegroundWhiteOpacity6 => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.TextForeground.White.Opacity.6");
    public static ComponentResourceKey ForegroundWhiteOpacity5 => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.TextForeground.White.Opacity.5");
    [Obsolete]
    public static ComponentResourceKey ForegroundWhiteOpacity4 => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.TextForeground.White.Opacity.4");
    [Obsolete]
    public static ComponentResourceKey ForegroundWhiteOpacity3 => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.TextForeground.White.Opacity.3");
    [Obsolete]
    public static ComponentResourceKey ForegroundWhiteOpacity2 => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.TextForeground.White.Opacity.2");
    [Obsolete]
    public static ComponentResourceKey ForegroundWhiteOpacity1 => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.TextForeground.White.Opacity.1");

    #endregion

    #region - BorderBrush -

    public static ComponentResourceKey BorderBrush => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.TextBorderBrush");
    public static ComponentResourceKey BorderBrushTitle => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.TextBorderBrush.Title");
    public static ComponentResourceKey BorderBrushAssist => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.TextBorderBrush.Assist");
    [Obsolete]
    public static ComponentResourceKey BorderBrushDisabled => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.TextBorderBrush.Disabled");
    #endregion


    #region - Accent -
    public static ComponentResourceKey Accent => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Accent");
    #endregion 

    #region - Dark - 
    public static ComponentResourceKey Dark10 => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Dark.10");
    public static ComponentResourceKey Dark9_5 => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Dark.9.5");
    public static ComponentResourceKey Dark9 => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Dark.9");
    public static ComponentResourceKey Dark8_5 => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Dark.8.5");
    public static ComponentResourceKey Dark8 => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Dark.8");
    public static ComponentResourceKey Dark7_5 => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Dark.7.5");
    public static ComponentResourceKey Dark7 => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Dark.7");
    public static ComponentResourceKey Dark6_5 => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Dark.6.5");
    public static ComponentResourceKey Dark6 => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Dark.6");
    public static ComponentResourceKey Dark5_5 => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Dark.5.5");
    public static ComponentResourceKey Dark5 => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Dark.5");
    public static ComponentResourceKey Dark4_5 => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Dark.4.5");
    public static ComponentResourceKey Dark4 => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Dark.4");
    public static ComponentResourceKey Dark3_5 => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Dark.3.5");
    public static ComponentResourceKey Dark3 => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Dark.3");
    public static ComponentResourceKey Dark2_5 => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Dark.2.5");
    public static ComponentResourceKey Dark2 => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Dark.2");
    public static ComponentResourceKey Dark1_5 => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Dark.1.5");
    public static ComponentResourceKey Dark1 => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Dark.1");
    public static ComponentResourceKey Dark0_9 => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Dark.0.9");
    public static ComponentResourceKey Dark0_8 => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Dark.0.8");
    public static ComponentResourceKey Dark0_7 => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Dark.0.7");
    public static ComponentResourceKey Dark0_6 => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Dark.0.6");
    public static ComponentResourceKey Dark0_5 => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Dark.0.5");
    public static ComponentResourceKey Dark0_4 => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Dark.0.4");
    public static ComponentResourceKey Dark0_3 => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Dark.0.3");
    public static ComponentResourceKey Dark0_2 => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Dark.0.2");
    public static ComponentResourceKey Dark0_1 => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Dark.0.1");
    public static ComponentResourceKey Dark0 => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Dark.0");
    #endregion 

    #region - Color -
    public static ComponentResourceKey LightGray => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.LightGray");
    [Obsolete]
    public static ComponentResourceKey LightGrayOpacity5 => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.LightGrayOpacity.5");
    public static ComponentResourceKey Gray => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Gray");
    [Obsolete]
    public static ComponentResourceKey GrayOpacity5 => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Gray.Opacity.5,");
    public static ComponentResourceKey Black => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Black");
    public static ComponentResourceKey Orange => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Orange");
    public static ComponentResourceKey Red => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Red");
    public static ComponentResourceKey Green => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Green");
    public static ComponentResourceKey Yellow => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Yellow");
    public static ComponentResourceKey Blue => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Blue");
    public static ComponentResourceKey Purple => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Purple");
    public static ComponentResourceKey Brown => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Brown");
    public static ComponentResourceKey LightBlue => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.LightBlue");
    public static ComponentResourceKey Pink => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Pink");

    public static ComponentResourceKey White => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.White");
    //public static ComponentResourceKey WhiteOpactiy9 => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.White.Opactiy.9");
    //public static ComponentResourceKey WhiteOpactiy8 => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.White.Opactiy.8");
    //public static ComponentResourceKey WhiteOpactiy7 => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.White.Opactiy.7");
    //public static ComponentResourceKey WhiteOpactiy6 => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.White.Opactiy.6");
    //public static ComponentResourceKey WhiteOpactiy5 => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.White.Opactiy.5");
    //public static ComponentResourceKey WhiteOpactiy4 => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.White.Opactiy.4");
    //public static ComponentResourceKey WhiteOpactiy3 => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.White.Opactiy.3");
    //public static ComponentResourceKey WhiteOpactiy2 => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.White.Opactiy.2");
    //public static ComponentResourceKey WhiteOpactiy1 => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.White.Opactiy.1");

    #endregion

    #region - System -
    public static ComponentResourceKey DialogCover => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Dialog.Cover");

    #endregion

    public static ComponentResourceKey Tranparent => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Tranparent");

    public static ComponentResourceKey Tile => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Tile");
    public static ComponentResourceKey Tile25 => new ComponentResourceKey(typeof(BrushKeys), "S.Brush.Tile.25");
}
