// Copyright (c) HeBianGu Authors. All Rights Reserved.
// Author: HeBianGu
// Github: https://github.com/HeBianGu/WPF-Control
// Document: https://hebiangu.github.io/WPF-Control-Docs
// QQ:908293466 Group:971261058
// bilibili: https://space.bilibili.com/370266611
// Licensed under the MIT License (the "License")

using System.ComponentModel.DataAnnotations;
using System.Windows;

namespace H.Themes.Colors.Solid;

[Display(Name = "午夜蓝", GroupName = "纯色主题", Description = "沉稳的午夜蓝纯色主题", Order = 162, Prompt = "新增")]
public class MidnightNavyDarkColorResource : ResxColorResourceBase
{
    public MidnightNavyDarkColorResource()
    {
        this.IsDark = true;
    }

    public override ResourceDictionary Resource => new ResourceDictionary()
    {
        Source = new Uri("pack://application:,,,/H.Themes.Colors.Solid;component/MidnightNavyDark.xaml")
    };
}

[Display(Name = "翡翠绿", GroupName = "纯色主题", Description = "低饱和翡翠绿深色主题", Order = 163, Prompt = "新增")]
public class EmeraldDarkColorResource : ResxColorResourceBase
{
    public EmeraldDarkColorResource()
    {
        this.IsDark = true;
    }

    public override ResourceDictionary Resource => new ResourceDictionary()
    {
        Source = new Uri("pack://application:,,,/H.Themes.Colors.Solid;component/EmeraldDark.xaml")
    };
}

[Display(Name = "深莓红", GroupName = "纯色主题", Description = "红莓强调的深色主题", Order = 164, Prompt = "新增")]
public class CrimsonDarkColorResource : ResxColorResourceBase
{
    public CrimsonDarkColorResource()
    {
        this.IsDark = true;
    }

    public override ResourceDictionary Resource => new ResourceDictionary()
    {
        Source = new Uri("pack://application:,,,/H.Themes.Colors.Solid;component/CrimsonDark.xaml")
    };
}

[Display(Name = "琥珀橙", GroupName = "纯色主题", Description = "温暖琥珀橙深色主题", Order = 165, Prompt = "新增")]
public class AmberDarkColorResource : ResxColorResourceBase
{
    public AmberDarkColorResource()
    {
        this.IsDark = true;
    }

    public override ResourceDictionary Resource => new ResourceDictionary()
    {
        Source = new Uri("pack://application:,,,/H.Themes.Colors.Solid;component/AmberDark.xaml")
    };
}

[Display(Name = "皇家紫", GroupName = "纯色主题", Description = "紫色强调的深色主题", Order = 166, Prompt = "新增")]
public class RoyalVioletDarkColorResource : ResxColorResourceBase
{
    public RoyalVioletDarkColorResource()
    {
        this.IsDark = true;
    }

    public override ResourceDictionary Resource => new ResourceDictionary()
    {
        Source = new Uri("pack://application:,,,/H.Themes.Colors.Solid;component/RoyalVioletDark.xaml")
    };
}

[Display(Name = "青绿色", GroupName = "纯色主题", Description = "青绿色科技感深色主题", Order = 167, Prompt = "新增")]
public class TealDarkColorResource : ResxColorResourceBase
{
    public TealDarkColorResource()
    {
        this.IsDark = true;
    }

    public override ResourceDictionary Resource => new ResourceDictionary()
    {
        Source = new Uri("pack://application:,,,/H.Themes.Colors.Solid;component/TealDark.xaml")
    };
}

[Display(Name = "石墨黑", GroupName = "纯色主题", Description = "中性石墨黑深色主题", Order = 168, Prompt = "新增")]
public class GraphiteDarkColorResource : ResxColorResourceBase
{
    public GraphiteDarkColorResource()
    {
        this.IsDark = true;
    }

    public override ResourceDictionary Resource => new ResourceDictionary()
    {
        Source = new Uri("pack://application:,,,/H.Themes.Colors.Solid;component/GraphiteDark.xaml")
    };
}

[Display(Name = "咖啡棕", GroupName = "纯色主题", Description = "温润咖啡棕深色主题", Order = 169, Prompt = "新增")]
public class CoffeeDarkColorResource : ResxColorResourceBase
{
    public CoffeeDarkColorResource()
    {
        this.IsDark = true;
    }

    public override ResourceDictionary Resource => new ResourceDictionary()
    {
        Source = new Uri("pack://application:,,,/H.Themes.Colors.Solid;component/CoffeeDark.xaml")
    };
}

[Display(Name = "赛博青柠", GroupName = "纯色主题", Description = "青柠强调的赛博深色主题", Order = 170, Prompt = "新增")]
public class CyberLimeDarkColorResource : ResxColorResourceBase
{
    public CyberLimeDarkColorResource()
    {
        this.IsDark = true;
    }

    public override ResourceDictionary Resource => new ResourceDictionary()
    {
        Source = new Uri("pack://application:,,,/H.Themes.Colors.Solid;component/CyberLimeDark.xaml")
    };
}

[Display(Name = "靛蓝夜", GroupName = "纯色主题", Description = "靛蓝强调的深色主题", Order = 171, Prompt = "新增")]
public class IndigoDarkColorResource : ResxColorResourceBase
{
    public IndigoDarkColorResource()
    {
        this.IsDark = true;
    }

    public override ResourceDictionary Resource => new ResourceDictionary()
    {
        Source = new Uri("pack://application:,,,/H.Themes.Colors.Solid;component/IndigoDark.xaml")
    };
}

[Display(Name = "极地蓝", GroupName = "纯色主题", Description = "清爽极地蓝浅色主题", Order = 172, Prompt = "新增")]
public class ArcticLightColorResource : ResxColorResourceBase
{
    public ArcticLightColorResource()
    {
        this.IsDark = false;
    }

    public override ResourceDictionary Resource => new ResourceDictionary()
    {
        Source = new Uri("pack://application:,,,/H.Themes.Colors.Solid;component/ArcticLight.xaml")
    };
}

[Display(Name = "薄荷绿", GroupName = "纯色主题", Description = "柔和薄荷绿浅色主题", Order = 173, Prompt = "新增")]
public class MintLightColorResource : ResxColorResourceBase
{
    public MintLightColorResource()
    {
        this.IsDark = false;
    }

    public override ResourceDictionary Resource => new ResourceDictionary()
    {
        Source = new Uri("pack://application:,,,/H.Themes.Colors.Solid;component/MintLight.xaml")
    };
}

[Display(Name = "珊瑚橙", GroupName = "纯色主题", Description = "温暖珊瑚橙浅色主题", Order = 174, Prompt = "新增")]
public class CoralLightColorResource : ResxColorResourceBase
{
    public CoralLightColorResource()
    {
        this.IsDark = false;
    }

    public override ResourceDictionary Resource => new ResourceDictionary()
    {
        Source = new Uri("pack://application:,,,/H.Themes.Colors.Solid;component/CoralLight.xaml")
    };
}

[Display(Name = "薰衣草", GroupName = "纯色主题", Description = "柔和薰衣草紫浅色主题", Order = 175, Prompt = "新增")]
public class LavenderLightColorResource : ResxColorResourceBase
{
    public LavenderLightColorResource()
    {
        this.IsDark = false;
    }

    public override ResourceDictionary Resource => new ResourceDictionary()
    {
        Source = new Uri("pack://application:,,,/H.Themes.Colors.Solid;component/LavenderLight.xaml")
    };
}

[Display(Name = "玫瑰粉", GroupName = "纯色主题", Description = "玫瑰粉强调浅色主题", Order = 176, Prompt = "新增")]
public class RoseLightColorResource : ResxColorResourceBase
{
    public RoseLightColorResource()
    {
        this.IsDark = false;
    }

    public override ResourceDictionary Resource => new ResourceDictionary()
    {
        Source = new Uri("pack://application:,,,/H.Themes.Colors.Solid;component/RoseLight.xaml")
    };
}

[Display(Name = "暖沙色", GroupName = "纯色主题", Description = "暖沙色浅色主题", Order = 177, Prompt = "新增")]
public class SandLightColorResource : ResxColorResourceBase
{
    public SandLightColorResource()
    {
        this.IsDark = false;
    }

    public override ResourceDictionary Resource => new ResourceDictionary()
    {
        Source = new Uri("pack://application:,,,/H.Themes.Colors.Solid;component/SandLight.xaml")
    };
}
