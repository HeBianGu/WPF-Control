// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System;
using System.Windows.Media;

namespace H.Extensions.Color;

public static class TupleColors
{
    private static readonly ColorConverter s_colorConverter = new ColorConverter();

    public static Tuple<System.Windows.Media.Color, System.Windows.Media.Color> BluePurple
    {
        get
        {
            return new Tuple<System.Windows.Media.Color, System.Windows.Media.Color>(
                (System.Windows.Media.Color)s_colorConverter.ConvertFromInvariantString("#4F8CFF"),
                (System.Windows.Media.Color)s_colorConverter.ConvertFromInvariantString("#7A5CFF"));
        }
    }

    public static Tuple<System.Windows.Media.Color, System.Windows.Media.Color> TealBlue
    {
        get
        {
            return new Tuple<System.Windows.Media.Color, System.Windows.Media.Color>(
                (System.Windows.Media.Color)s_colorConverter.ConvertFromInvariantString("#2DD4BF"),
                (System.Windows.Media.Color)s_colorConverter.ConvertFromInvariantString("#3B82F6"));
        }
    }

    public static Tuple<System.Windows.Media.Color, System.Windows.Media.Color> PinkPurple
    {
        get
        {
            return new Tuple<System.Windows.Media.Color, System.Windows.Media.Color>(
                (System.Windows.Media.Color)s_colorConverter.ConvertFromInvariantString("#F472B6"),
                (System.Windows.Media.Color)s_colorConverter.ConvertFromInvariantString("#A78BFA"));
        }
    }

    public static Tuple<System.Windows.Media.Color, System.Windows.Media.Color> SunsetOrangePink
    {
        get
        {
            return new Tuple<System.Windows.Media.Color, System.Windows.Media.Color>(
                (System.Windows.Media.Color)s_colorConverter.ConvertFromInvariantString("#FB923C"),
                (System.Windows.Media.Color)s_colorConverter.ConvertFromInvariantString("#F472B6"));
        }
    }

    public static Tuple<System.Windows.Media.Color, System.Windows.Media.Color> SlateNavy
    {
        get
        {
            return new Tuple<System.Windows.Media.Color, System.Windows.Media.Color>(
                (System.Windows.Media.Color)s_colorConverter.ConvertFromInvariantString("#64748B"),
                (System.Windows.Media.Color)s_colorConverter.ConvertFromInvariantString("#0F172A"));
        }
    }

    public static Tuple<System.Windows.Media.Color, System.Windows.Media.Color> BlackGold
    {
        get
        {
            return new Tuple<System.Windows.Media.Color, System.Windows.Media.Color>(
                (System.Windows.Media.Color)s_colorConverter.ConvertFromInvariantString("#111827"),
                (System.Windows.Media.Color)s_colorConverter.ConvertFromInvariantString("#F59E0B"));
        }
    }

    public static Tuple<System.Windows.Media.Color, System.Windows.Media.Color> BluePurpleAlpha
    {
        get
        {
            return new Tuple<System.Windows.Media.Color, System.Windows.Media.Color>(
                (System.Windows.Media.Color)s_colorConverter.ConvertFromInvariantString("#CC4F8CFF"),
                (System.Windows.Media.Color)s_colorConverter.ConvertFromInvariantString("#CC7A5CFF"));
        }
    }
}
