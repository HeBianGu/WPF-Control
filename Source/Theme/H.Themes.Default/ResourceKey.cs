// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Windows;

namespace H.Themes.Default
{
    public static class ColorKeys
    {
        #region - Background -
        public static ComponentResourceKey Background => new ComponentResourceKey(typeof(ColorKeys), "S.Brush.TextBackground");
        public static ComponentResourceKey BackgroundDisabled => new ComponentResourceKey(typeof(ColorKeys), "S.Brush.TextBackground.Disabled");
        public static ComponentResourceKey AlternatingRowBackground => new ComponentResourceKey(typeof(ColorKeys), "S.Brush.RowIndex.BackGround");
        #endregion

        public static ComponentResourceKey Foreground => new ComponentResourceKey(typeof(ColorKeys), "S.Brush.TextForeground");
        public static ComponentResourceKey ForegroundTitle => new ComponentResourceKey(typeof(ColorKeys), "S.Brush.TextForeground.Title");
        public static ComponentResourceKey ForegroundDisabled => new ComponentResourceKey(typeof(ColorKeys), "S.Brush.TextForeground.Disabled");

        #region - Foreground -

        public static ComponentResourceKey MouseOver => new ComponentResourceKey(typeof(ColorKeys), "S.Brush.TextMouseOver");
        public static ComponentResourceKey Selected => new ComponentResourceKey(typeof(ColorKeys), "S.Brush.TextSelected");
        public static ComponentResourceKey ForegroundAssist => new ComponentResourceKey(typeof(ColorKeys), "S.Brush.TextForeground.Assist");
        public static ComponentResourceKey ForegroundLink => new ComponentResourceKey(typeof(ColorKeys), "S.Brush.TextForeground.Link");
        public static ComponentResourceKey ForegroundWhite => new ComponentResourceKey(typeof(ColorKeys), "S.Brush.TextForeground.White");

        public static ComponentResourceKey ForegroundWhiteOpacity9 => new ComponentResourceKey(typeof(ColorKeys), "S.Brush.TextForeground.White.Opacity.9");
        public static ComponentResourceKey ForegroundWhiteOpacity8 => new ComponentResourceKey(typeof(ColorKeys), "S.Brush.TextForeground.White.Opacity.8");
        public static ComponentResourceKey ForegroundWhiteOpacity7 => new ComponentResourceKey(typeof(ColorKeys), "S.Brush.TextForeground.White.Opacity.7");
        public static ComponentResourceKey ForegroundWhiteOpacity6 => new ComponentResourceKey(typeof(ColorKeys), "S.Brush.TextForeground.White.Opacity.6");
        public static ComponentResourceKey ForegroundWhiteOpacity5 => new ComponentResourceKey(typeof(ColorKeys), "S.Brush.TextForeground.White.Opacity.5");
        public static ComponentResourceKey ForegroundWhiteOpacity4 => new ComponentResourceKey(typeof(ColorKeys), "S.Brush.TextForeground.White.Opacity.4");
        public static ComponentResourceKey ForegroundWhiteOpacity3 => new ComponentResourceKey(typeof(ColorKeys), "S.Brush.TextForeground.White.Opacity.3");
        public static ComponentResourceKey ForegroundWhiteOpacity2 => new ComponentResourceKey(typeof(ColorKeys), "S.Brush.TextForeground.White.Opacity.2");
        public static ComponentResourceKey ForegroundWhiteOpacity1 => new ComponentResourceKey(typeof(ColorKeys), "S.Brush.TextForeground.White.Opacity.1");

        #endregion

        #region - BorderBrush -

        public static ComponentResourceKey BorderBrush => new ComponentResourceKey(typeof(ColorKeys), "S.Brush.TextBorderBrush");
        public static ComponentResourceKey BorderBrushTitle => new ComponentResourceKey(typeof(ColorKeys), "S.Brush.TextBorderBrush.Title");
        public static ComponentResourceKey BorderBrushAssist => new ComponentResourceKey(typeof(ColorKeys), "S.Brush.TextBorderBrush.Assist");
        public static ComponentResourceKey BorderBrushDisabled => new ComponentResourceKey(typeof(ColorKeys), "S.Brush.TextBorderBrush.Disabled");
        #endregion


        #region - Accent -
        public static ComponentResourceKey Accent => new ComponentResourceKey(typeof(ColorKeys), "S.Brush.Accent");
        #endregion 

        #region - Dark - 
        public static ComponentResourceKey Dark10 => new ComponentResourceKey(typeof(ColorKeys), "S.Brush.Dark.10");
        public static ComponentResourceKey Dark9_5 => new ComponentResourceKey(typeof(ColorKeys), "S.Brush.Dark.9.5");
        public static ComponentResourceKey Dark9 => new ComponentResourceKey(typeof(ColorKeys), "S.Brush.Dark.9");
        public static ComponentResourceKey Dark8_5 => new ComponentResourceKey(typeof(ColorKeys), "S.Brush.Dark.8.5");
        public static ComponentResourceKey Dark8 => new ComponentResourceKey(typeof(ColorKeys), "S.Brush.Dark.8");
        public static ComponentResourceKey Dark7_5 => new ComponentResourceKey(typeof(ColorKeys), "S.Brush.Dark.7.5");
        public static ComponentResourceKey Dark7 => new ComponentResourceKey(typeof(ColorKeys), "S.Brush.Dark.7");
        public static ComponentResourceKey Dark6_5 => new ComponentResourceKey(typeof(ColorKeys), "S.Brush.Dark.6.5");
        public static ComponentResourceKey Dark6 => new ComponentResourceKey(typeof(ColorKeys), "S.Brush.Dark.6");
        public static ComponentResourceKey Dark5_5 => new ComponentResourceKey(typeof(ColorKeys), "S.Brush.Dark.5.5");
        public static ComponentResourceKey Dark5 => new ComponentResourceKey(typeof(ColorKeys), "S.Brush.Dark.5");
        public static ComponentResourceKey Dark4_5 => new ComponentResourceKey(typeof(ColorKeys), "S.Brush.Dark.4.5");
        public static ComponentResourceKey Dark4 => new ComponentResourceKey(typeof(ColorKeys), "S.Brush.Dark.4");
        public static ComponentResourceKey Dark3_5 => new ComponentResourceKey(typeof(ColorKeys), "S.Brush.Dark.3.5");
        public static ComponentResourceKey Dark3 => new ComponentResourceKey(typeof(ColorKeys), "S.Brush.Dark.3");
        public static ComponentResourceKey Dark2_5 => new ComponentResourceKey(typeof(ColorKeys), "S.Brush.Dark.2.5");
        public static ComponentResourceKey Dark2 => new ComponentResourceKey(typeof(ColorKeys), "S.Brush.Dark.2");
        public static ComponentResourceKey Dark1_5 => new ComponentResourceKey(typeof(ColorKeys), "S.Brush.Dark.1.5");
        public static ComponentResourceKey Dark1 => new ComponentResourceKey(typeof(ColorKeys), "S.Brush.Dark.1");
        public static ComponentResourceKey Dark0_9 => new ComponentResourceKey(typeof(ColorKeys), "S.Brush.Dark.0.9");
        public static ComponentResourceKey Dark0_8 => new ComponentResourceKey(typeof(ColorKeys), "S.Brush.Dark.0.8");
        public static ComponentResourceKey Dark0_7 => new ComponentResourceKey(typeof(ColorKeys), "S.Brush.Dark.0.7");
        public static ComponentResourceKey Dark0_6 => new ComponentResourceKey(typeof(ColorKeys), "S.Brush.Dark.0.6");
        public static ComponentResourceKey Dark0_5 => new ComponentResourceKey(typeof(ColorKeys), "S.Brush.Dark.0.5");
        public static ComponentResourceKey Dark0_4 => new ComponentResourceKey(typeof(ColorKeys), "S.Brush.Dark.0.4");
        public static ComponentResourceKey Dark0_3 => new ComponentResourceKey(typeof(ColorKeys), "S.Brush.Dark.0.3");
        public static ComponentResourceKey Dark0_2 => new ComponentResourceKey(typeof(ColorKeys), "S.Brush.Dark.0.2");
        public static ComponentResourceKey Dark0_1 => new ComponentResourceKey(typeof(ColorKeys), "S.Brush.Dark.0.1");
        public static ComponentResourceKey Dark0 => new ComponentResourceKey(typeof(ColorKeys), "S.Brush.Dark.0");
        #endregion 

        #region - Color -
        public static ComponentResourceKey LightGray => new ComponentResourceKey(typeof(ColorKeys), "S.Brush.LightGray.Notice");
        public static ComponentResourceKey LightGrayOpacity5 => new ComponentResourceKey(typeof(ColorKeys), "S.Brush.LightGray.NoticeOpacity.5");
        public static ComponentResourceKey Gray => new ComponentResourceKey(typeof(ColorKeys), "S.Brush.Gray.Notice");
        public static ComponentResourceKey GrayOpacity5 => new ComponentResourceKey(typeof(ColorKeys), "S.Brush.Gray.Notice.Opacity.5,");
        public static ComponentResourceKey Black => new ComponentResourceKey(typeof(ColorKeys), "S.Brush.Black.Notice");
        public static ComponentResourceKey Orange => new ComponentResourceKey(typeof(ColorKeys), "S.Brush.Orange.Notice");
        public static ComponentResourceKey Red => new ComponentResourceKey(typeof(ColorKeys), "S.Brush.Red.Notice");
        public static ComponentResourceKey Green => new ComponentResourceKey(typeof(ColorKeys), "S.Brush.Green.Notice");
        public static ComponentResourceKey Yellow => new ComponentResourceKey(typeof(ColorKeys), "S.Brush.Yellow.Notice");
        public static ComponentResourceKey Blue => new ComponentResourceKey(typeof(ColorKeys), "S.Brush.Blue.Notice");
        public static ComponentResourceKey Purple => new ComponentResourceKey(typeof(ColorKeys), "S.Brush.Purple.Notice");
        public static ComponentResourceKey Brown => new ComponentResourceKey(typeof(ColorKeys), "S.Brush.Brown.Notice");
        public static ComponentResourceKey LightBlue => new ComponentResourceKey(typeof(ColorKeys), "S.Brush.LightBlue.Notice");
        public static ComponentResourceKey Pink => new ComponentResourceKey(typeof(ColorKeys), "S.Brush.Pink.Notice");

        public static ComponentResourceKey White => new ComponentResourceKey(typeof(ColorKeys), "S.Brush.White.Notice");
        public static ComponentResourceKey WhiteOpactiy9 => new ComponentResourceKey(typeof(ColorKeys), "S.Brush.White.Opactiy.9");
        public static ComponentResourceKey WhiteOpactiy8 => new ComponentResourceKey(typeof(ColorKeys), "S.Brush.White.Opactiy.8");
        public static ComponentResourceKey WhiteOpactiy7 => new ComponentResourceKey(typeof(ColorKeys), "S.Brush.White.Opactiy.7");
        public static ComponentResourceKey WhiteOpactiy6 => new ComponentResourceKey(typeof(ColorKeys), "S.Brush.White.Opactiy.6");
        public static ComponentResourceKey WhiteOpactiy5 => new ComponentResourceKey(typeof(ColorKeys), "S.Brush.White.Opactiy.5");
        public static ComponentResourceKey WhiteOpactiy4 => new ComponentResourceKey(typeof(ColorKeys), "S.Brush.White.Opactiy.4");
        public static ComponentResourceKey WhiteOpactiy3 => new ComponentResourceKey(typeof(ColorKeys), "S.Brush.White.Opactiy.3");
        public static ComponentResourceKey WhiteOpactiy2 => new ComponentResourceKey(typeof(ColorKeys), "S.Brush.White.Opactiy.2");
        public static ComponentResourceKey WhiteOpactiy1 => new ComponentResourceKey(typeof(ColorKeys), "S.Brush.White.Opactiy.1");

        #endregion

        #region - System -
        public static ComponentResourceKey DialogCover => new ComponentResourceKey(typeof(ColorKeys), "S.Brush.Dialog.Cover");

        #endregion

        public static ComponentResourceKey Tranparent => new ComponentResourceKey(typeof(ColorKeys), "S.Brush.Tranparent");
    }

    public static class LayoutKeys
    {
        public static ComponentResourceKey WindowCaptionHeight => new ComponentResourceKey(typeof(LayoutKeys), "S.Layout.WindowCaptionHeight");
        public static ComponentResourceKey RowHeight => new ComponentResourceKey(typeof(LayoutKeys), "S.Layout.RowHeight");
        public static ComponentResourceKey ItemHeight => new ComponentResourceKey(typeof(LayoutKeys), "S.Layout.ItemHeight");
    }

    public static class FontSizeKeys
    {
        public static ComponentResourceKey Header => new ComponentResourceKey(typeof(FontSizeKeys), "S.FontSize.Header");
        public static ComponentResourceKey Header1 => new ComponentResourceKey(typeof(FontSizeKeys), "S.FontSize.Header.1");
        public static ComponentResourceKey Header2 => new ComponentResourceKey(typeof(FontSizeKeys), "S.FontSize.Header.2");
        public static ComponentResourceKey Header3 => new ComponentResourceKey(typeof(FontSizeKeys), "S.FontSize.Header.3");
        public static ComponentResourceKey Header4 => new ComponentResourceKey(typeof(FontSizeKeys), "S.FontSize.Header.4");
        public static ComponentResourceKey Header5 => new ComponentResourceKey(typeof(FontSizeKeys), "S.FontSize.Header.5");
        public static ComponentResourceKey Header6 => new ComponentResourceKey(typeof(FontSizeKeys), "S.FontSize.Header.6");
        public static ComponentResourceKey Header7 => new ComponentResourceKey(typeof(FontSizeKeys), "S.FontSize.Header.7");
        public static ComponentResourceKey Header8 => new ComponentResourceKey(typeof(FontSizeKeys), "S.FontSize.Header.8");
        public static ComponentResourceKey Header9 => new ComponentResourceKey(typeof(FontSizeKeys), "S.FontSize.Header.9");
        public static ComponentResourceKey Default => new ComponentResourceKey(typeof(FontSizeKeys), "S.FontSize.Default");
    }
}
