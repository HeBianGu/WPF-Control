// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Extensions.FontIcon;

namespace H.Presenters.Design.Presenter;

[Icon(FontIcons.Smartcard)]
[Display(Name = "卡片")]
public class CardPresenter : TitlePresenter
{
    public CardPresenter()
    {
        this.Title = "总计：";
        this.Text = "80.34%";
        this.Height = 80.0;
    }
    public override void LoadDefault()
    {
        base.LoadDefault();
        this.FromColor = Colors.Orange;
        this.ToColor = Colors.OrangeRed;
        this.DropShadowEffectOpacity = 0.5;
        this.Height = 90;
        this.Width = 250;
        this.CornerRadius = new CornerRadius(5);
        this.Foreground = Brushes.White;
        this.TitleForeground = Brushes.White;
        this.FontSize = 25;
        this.TitleFontSize = 15;
        this.HorizontalContentAlignment = HorizontalAlignment.Center;
        this.VerticalContentAlignment = VerticalAlignment.Center;
        this.Orientation = Orientation.Horizontal;
    }
    private Color _fromColor;
    [Display(Name = "起始颜色", GroupName = "常用,样式")]
    public Color FromColor
    {
        get { return _fromColor; }
        set
        {
            _fromColor = value;
            RaisePropertyChanged();
        }
    }

    private Color _toColor;
    [Display(Name = "终止颜色", GroupName = "常用,样式")]
    public Color ToColor
    {
        get { return _toColor; }
        set
        {
            _toColor = value;
            RaisePropertyChanged();
        }
    }

    private double _dropShadowEffectOpacity;
    [Display(Name = "阴影透明度", GroupName = "常用,样式")]
    public double DropShadowEffectOpacity
    {
        get { return _dropShadowEffectOpacity; }
        set
        {
            _dropShadowEffectOpacity = value;
            RaisePropertyChanged();
        }
    }

    private CornerRadius _CornerRadius;
    [Display(Name = "圆角", GroupName = "常用,样式")]
    public CornerRadius CornerRadius
    {
        get { return _CornerRadius; }
        set
        {
            _CornerRadius = value;
            RaisePropertyChanged();
        }
    }

    private Orientation _orientation;
    [Display(Name = "对齐方式", GroupName = "常用,样式")]
    public Orientation Orientation
    {
        get { return _orientation; }
        set
        {
            _orientation = value;
            RaisePropertyChanged();
        }
    }

    public static CardPresenter Create(Action<CardPresenter> action = null)
    {
        var result = new CardPresenter();
        action?.Invoke(result);
        return result;
    }


    public static CardPresenter CreateBluePurple(Action<CardPresenter> action = null)
    {
        ColorConverter s_colorConverter = new ColorConverter();
        var result = new CardPresenter();
        result.FromColor = (Color)s_colorConverter.ConvertFromInvariantString("#FF5CFF");
        result.ToColor = (Color)s_colorConverter.ConvertFromInvariantString("#7A5CFF");
        return result;
    }

    public static CardPresenter CreateTealBlue(Action<CardPresenter> action = null)
    {
        ColorConverter s_colorConverter = new ColorConverter();
        var result = new CardPresenter();
        result.FromColor = (Color)s_colorConverter.ConvertFromInvariantString("#2DD4BF");
        result.ToColor = (Color)s_colorConverter.ConvertFromInvariantString("#3B82F6");
        return result;
    }

    public static CardPresenter CreatePinkPurple(Action<CardPresenter> action = null)
    {
        ColorConverter s_colorConverter = new ColorConverter();
        var result = new CardPresenter();
        result.FromColor = (Color)s_colorConverter.ConvertFromInvariantString("#F472B6");
        result.ToColor = (Color)s_colorConverter.ConvertFromInvariantString("#A78BFA");
        return result;
    }

    public static CardPresenter CreateSunsetOrangePink(Action<CardPresenter> action = null)
    {
        ColorConverter s_colorConverter = new ColorConverter();
        var result = new CardPresenter();
        result.FromColor = (Color)s_colorConverter.ConvertFromInvariantString("#FB923C");
        result.ToColor = (Color)s_colorConverter.ConvertFromInvariantString("#F472B6");
        return result;
    }

    public static CardPresenter CreateSlateNavy(Action<CardPresenter> action = null)
    {
        ColorConverter s_colorConverter = new ColorConverter();
        var result = new CardPresenter();
        result.FromColor = (Color)s_colorConverter.ConvertFromInvariantString("#64748B");
        result.ToColor = (Color)s_colorConverter.ConvertFromInvariantString("#0F172A");
        return result;
    }

    public static CardPresenter CreateBlackGold(Action<CardPresenter> action = null)
    {
        ColorConverter s_colorConverter = new ColorConverter();
        var result = new CardPresenter();
        result.FromColor = (Color)s_colorConverter.ConvertFromInvariantString("#111827");
        result.ToColor = (Color)s_colorConverter.ConvertFromInvariantString("#F59E0B");
        return result;
    }
}
