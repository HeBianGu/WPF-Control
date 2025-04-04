// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Windows;

namespace H.Themes.Colors;

/// <summary>
/// 提供颜色键的静态类
/// </summary>
public static class ColorKeys
{
    #region - Background -
    /// <summary>
    /// 标题背景色
    /// </summary>
    public static ComponentResourceKey CaptionBackground => new ComponentResourceKey(typeof(ColorKeys), "S.Color.CaptionBackground");

    /// <summary>
    /// 背景色
    /// </summary>
    public static ComponentResourceKey Background => new ComponentResourceKey(typeof(ColorKeys), "S.Color.TextBackground");

    /// <summary>
    /// 交替行背景色
    /// </summary>
    public static ComponentResourceKey AlternatingRowBackground => new ComponentResourceKey(typeof(ColorKeys), "S.Color.RowIndex.BackGround");
    #endregion

    /// <summary>
    /// 标题前景色
    /// </summary>
    public static ComponentResourceKey CaptionForeground => new ComponentResourceKey(typeof(ColorKeys), "S.Color.CaptionForeground");

    /// <summary>
    /// 前景色
    /// </summary>
    public static ComponentResourceKey Foreground => new ComponentResourceKey(typeof(ColorKeys), "S.Color.TextForeground");

    /// <summary>
    /// 标题前景色
    /// </summary>
    public static ComponentResourceKey ForegroundTitle => new ComponentResourceKey(typeof(ColorKeys), "S.Color.TextForeground.Title");

    /// <summary>
    /// 选中前景色
    /// </summary>
    public static ComponentResourceKey ForegroundSelect => new ComponentResourceKey(typeof(ColorKeys), "S.Color.TextForeground.Select");

    #region - Foreground -
    /// <summary>
    /// 鼠标悬停前景色
    /// </summary>
    public static ComponentResourceKey MouseOver => new ComponentResourceKey(typeof(ColorKeys), "S.Color.TextMouseOver");

    /// <summary>
    /// 选中前景色
    /// </summary>
    public static ComponentResourceKey Selected => new ComponentResourceKey(typeof(ColorKeys), "S.Color.TextSelected");

    /// <summary>
    /// 辅助前景色
    /// </summary>
    public static ComponentResourceKey ForegroundAssist => new ComponentResourceKey(typeof(ColorKeys), "S.Color.TextForeground.Assist");

    /// <summary>
    /// 链接前景色
    /// </summary>
    public static ComponentResourceKey ForegroundLink => new ComponentResourceKey(typeof(ColorKeys), "S.Color.TextForeground.Link");
    #endregion

    #region - BorderBrush -
    /// <summary>
    /// 边框画刷
    /// </summary>
    public static ComponentResourceKey BorderBrush => new ComponentResourceKey(typeof(ColorKeys), "S.Color.TextBorderBrush");

    /// <summary>
    /// 标题边框画刷
    /// </summary>
    public static ComponentResourceKey BorderBrushTitle => new ComponentResourceKey(typeof(ColorKeys), "S.Color.TextBorderBrush.Title");

    /// <summary>
    /// 辅助边框画刷
    /// </summary>
    public static ComponentResourceKey BorderBrushAssist => new ComponentResourceKey(typeof(ColorKeys), "S.Color.TextBorderBrush.Assist");
    #endregion

    #region - Accent -
    /// <summary>
    /// 强调色
    /// </summary>
    public static ComponentResourceKey Accent => new ComponentResourceKey(typeof(ColorKeys), "S.Color.Accent");
    #endregion 

    #region - Dark - 
    /// <summary>
    /// 深色10
    /// </summary>
    public static ComponentResourceKey Dark10 => new ComponentResourceKey(typeof(ColorKeys), "S.Color.Dark.28");

    /// <summary>
    /// 深色9.5
    /// </summary>
    public static ComponentResourceKey Dark9_5 => new ComponentResourceKey(typeof(ColorKeys), "S.Color.Dark.27");

    /// <summary>
    /// 深色9
    /// </summary>
    public static ComponentResourceKey Dark9 => new ComponentResourceKey(typeof(ColorKeys), "SS.Color.Dark.26");

    /// <summary>
    /// 深色8.5
    /// </summary>
    public static ComponentResourceKey Dark8_5 => new ComponentResourceKey(typeof(ColorKeys), "S.Color.Dark.25");

    /// <summary>
    /// 深色8
    /// </summary>
    public static ComponentResourceKey Dark8 => new ComponentResourceKey(typeof(ColorKeys), "S.Color.Dark.24");

    /// <summary>
    /// 深色7.5
    /// </summary>
    public static ComponentResourceKey Dark7_5 => new ComponentResourceKey(typeof(ColorKeys), "S.Color.Dark.23");

    /// <summary>
    /// 深色7
    /// </summary>
    public static ComponentResourceKey Dark7 => new ComponentResourceKey(typeof(ColorKeys), "SS.Color.Dark.22");

    /// <summary>
    /// 深色6.5
    /// </summary>
    public static ComponentResourceKey Dark6_5 => new ComponentResourceKey(typeof(ColorKeys), "S.Color.Dark.21");

    /// <summary>
    /// 深色6
    /// </summary>
    public static ComponentResourceKey Dark6 => new ComponentResourceKey(typeof(ColorKeys), "S.Color.Dark.20");

    /// <summary>
    /// 深色5.5
    /// </summary>
    public static ComponentResourceKey Dark5_5 => new ComponentResourceKey(typeof(ColorKeys), "S.Color.Dark.19");

    /// <summary>
    /// 深色5
    /// </summary>
    public static ComponentResourceKey Dark5 => new ComponentResourceKey(typeof(ColorKeys), "S.Color.Dark.18");

    /// <summary>
    /// 深色4.5
    /// </summary>
    public static ComponentResourceKey Dark4_5 => new ComponentResourceKey(typeof(ColorKeys), "SS.Color.Dark.17");

    /// <summary>
    /// 深色4
    /// </summary>
    public static ComponentResourceKey Dark4 => new ComponentResourceKey(typeof(ColorKeys), "S.Color.Dark.16");

    /// <summary>
    /// 深色3.5
    /// </summary>
    public static ComponentResourceKey Dark3_5 => new ComponentResourceKey(typeof(ColorKeys), "S.Color.Dark.15");

    /// <summary>
    /// 深色3
    /// </summary>
    public static ComponentResourceKey Dark3 => new ComponentResourceKey(typeof(ColorKeys), "S.Color.Dark.14");

    /// <summary>
    /// 深色2.5
    /// </summary>
    public static ComponentResourceKey Dark2_5 => new ComponentResourceKey(typeof(ColorKeys), "S.Color.Dark.13");

    /// <summary>
    /// 深色2
    /// </summary>
    public static ComponentResourceKey Dark2 => new ComponentResourceKey(typeof(ColorKeys), "S.Color.Dark.12");

    /// <summary>
    /// 深色1.5
    /// </summary>
    public static ComponentResourceKey Dark1_5 => new ComponentResourceKey(typeof(ColorKeys), "S.Color.Dark.11");

    /// <summary>
    /// 深色1
    /// </summary>
    public static ComponentResourceKey Dark1 => new ComponentResourceKey(typeof(ColorKeys), "S.Color.Dark.10");

    /// <summary>
    /// 深色0.9
    /// </summary>
    public static ComponentResourceKey Dark0_9 => new ComponentResourceKey(typeof(ColorKeys), "S.Color.Dark.9");

    /// <summary>
    /// 深色0.8
    /// </summary>
    public static ComponentResourceKey Dark0_8 => new ComponentResourceKey(typeof(ColorKeys), "S.Color.Dark.8");

    /// <summary>
    /// 深色0.7
    /// </summary>
    public static ComponentResourceKey Dark0_7 => new ComponentResourceKey(typeof(ColorKeys), "S.Color.Dark.7");

    /// <summary>
    /// 深色0.6
    /// </summary>
    public static ComponentResourceKey Dark0_6 => new ComponentResourceKey(typeof(ColorKeys), "S.Color.Dark.6");

    /// <summary>
    /// 深色0.5
    /// </summary>
    public static ComponentResourceKey Dark0_5 => new ComponentResourceKey(typeof(ColorKeys), "S.Color.Dark.5");

    /// <summary>
    /// 深色0.4
    /// </summary>
    public static ComponentResourceKey Dark0_4 => new ComponentResourceKey(typeof(ColorKeys), "S.Color.Dark.4");

    /// <summary>
    /// 深色0.3
    /// </summary>
    public static ComponentResourceKey Dark0_3 => new ComponentResourceKey(typeof(ColorKeys), "S.Color.Dark.3");

    /// <summary>
    /// 深色0.2
    /// </summary>
    public static ComponentResourceKey Dark0_2 => new ComponentResourceKey(typeof(ColorKeys), "S.Color.Dark.2");

    /// <summary>
    /// 深色0.1
    /// </summary>
    public static ComponentResourceKey Dark0_1 => new ComponentResourceKey(typeof(ColorKeys), "S.Color.Dark.1");

    /// <summary>
    /// 深色0
    /// </summary>
    public static ComponentResourceKey Dark0 => new ComponentResourceKey(typeof(ColorKeys), "S.Color.Dark.0");
    #endregion 

    #region - Color -
    /// <summary>
    /// 浅灰色
    /// </summary>
    public static ComponentResourceKey LightGray => new ComponentResourceKey(typeof(ColorKeys), "S.Color.LightGray");

    /// <summary>
    /// 灰色
    /// </summary>
    public static ComponentResourceKey Gray => new ComponentResourceKey(typeof(ColorKeys), "S.Color.Gray");

    /// <summary>
    /// 黑色
    /// </summary>
    public static ComponentResourceKey Black => new ComponentResourceKey(typeof(ColorKeys), "S.Color.Black");

    /// <summary>
    /// 橙色
    /// </summary>
    public static ComponentResourceKey Orange => new ComponentResourceKey(typeof(ColorKeys), "S.Color.Orange");

    /// <summary>
    /// 红色
    /// </summary>
    public static ComponentResourceKey Red => new ComponentResourceKey(typeof(ColorKeys), "S.Color.Red");

    /// <summary>
    /// 绿色
    /// </summary>
    public static ComponentResourceKey Green => new ComponentResourceKey(typeof(ColorKeys), "S.Color.Green");

    /// <summary>
    /// 黄色
    /// </summary>
    public static ComponentResourceKey Yellow => new ComponentResourceKey(typeof(ColorKeys), "S.Color.Yellow");

    /// <summary>
    /// 蓝色
    /// </summary>
    public static ComponentResourceKey Blue => new ComponentResourceKey(typeof(ColorKeys), "S.Color.Blue");

    /// <summary>
    /// 紫色
    /// </summary>
    public static ComponentResourceKey Purple => new ComponentResourceKey(typeof(ColorKeys), "S.Color.Purple");

    /// <summary>
    /// 棕色
    /// </summary>
    public static ComponentResourceKey Brown => new ComponentResourceKey(typeof(ColorKeys), "S.Color.Brown");

    /// <summary>
    /// 浅蓝色
    /// </summary>
    public static ComponentResourceKey LightBlue => new ComponentResourceKey(typeof(ColorKeys), "S.Color.LightBlue");

    /// <summary>
    /// 粉色
    /// </summary>
    public static ComponentResourceKey Pink => new ComponentResourceKey(typeof(ColorKeys), "S.Color.Pink");
    #endregion

    #region - System -
    /// <summary>
    /// 对话框覆盖色
    /// </summary>
    public static ComponentResourceKey DialogCover => new ComponentResourceKey(typeof(ColorKeys), "S.Color.Dialog.Cover");
    #endregion

    /// <summary>
    /// 透明色
    /// </summary>
    public static ComponentResourceKey Tranparent => new ComponentResourceKey(typeof(ColorKeys), "S.Color.Tranparent");
}
