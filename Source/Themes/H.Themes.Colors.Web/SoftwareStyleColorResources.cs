// Copyright (c) HeBianGu Authors. All Rights Reserved.
// Author: HeBianGu
// Github: https://github.com/HeBianGu/WPF-Control
// Document: https://hebiangu.github.io/WPF-Control-Docs
// QQ:908293466 Group:971261058
// bilibili: https://space.bilibili.com/370266611
// Licensed under the MIT License (the "License")

using System.ComponentModel.DataAnnotations;
using System.Windows;

namespace H.Themes.Colors.Web;

[Display(Name = "微信风格", GroupName = "主流软件风格", Description = "参考主流软件视觉氛围生成的配色方案，不包含品牌素材", Order = 800, Prompt = "新增")]
public class WeChatStyleColorResource : ResxColorResourceBase { public WeChatStyleColorResource() => this.IsDark = false; public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Web;component/WeChatStyle.xaml") }; }
[Display(Name = "QQ风格", GroupName = "主流软件风格", Description = "参考主流软件视觉氛围生成的配色方案，不包含品牌素材", Order = 801, Prompt = "新增")]
public class QQStyleColorResource : ResxColorResourceBase { public QQStyleColorResource() => this.IsDark = false; public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Web;component/QQStyle.xaml") }; }
[Display(Name = "支付宝风格", GroupName = "主流软件风格", Description = "参考主流软件视觉氛围生成的配色方案，不包含品牌素材", Order = 802, Prompt = "新增")]
public class AlipayStyleColorResource : ResxColorResourceBase { public AlipayStyleColorResource() => this.IsDark = false; public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Web;component/AlipayStyle.xaml") }; }
[Display(Name = "淘宝风格", GroupName = "主流软件风格", Description = "参考主流软件视觉氛围生成的配色方案，不包含品牌素材", Order = 803, Prompt = "新增")]
public class TaobaoStyleColorResource : ResxColorResourceBase { public TaobaoStyleColorResource() => this.IsDark = false; public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Web;component/TaobaoStyle.xaml") }; }
[Display(Name = "天猫风格", GroupName = "主流软件风格", Description = "参考主流软件视觉氛围生成的配色方案，不包含品牌素材", Order = 804, Prompt = "新增")]
public class TmallStyleColorResource : ResxColorResourceBase { public TmallStyleColorResource() => this.IsDark = false; public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Web;component/TmallStyle.xaml") }; }
[Display(Name = "闲鱼风格", GroupName = "主流软件风格", Description = "参考主流软件视觉氛围生成的配色方案，不包含品牌素材", Order = 805, Prompt = "新增")]
public class XianyuStyleColorResource : ResxColorResourceBase { public XianyuStyleColorResource() => this.IsDark = false; public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Web;component/XianyuStyle.xaml") }; }
[Display(Name = "京东风格", GroupName = "主流软件风格", Description = "参考主流软件视觉氛围生成的配色方案，不包含品牌素材", Order = 806, Prompt = "新增")]
public class JDStyleColorResource : ResxColorResourceBase { public JDStyleColorResource() => this.IsDark = false; public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Web;component/JDStyle.xaml") }; }
[Display(Name = "企业微信风格", GroupName = "主流软件风格", Description = "参考主流软件视觉氛围生成的配色方案，不包含品牌素材", Order = 807, Prompt = "新增")]
public class WeComStyleColorResource : ResxColorResourceBase { public WeComStyleColorResource() => this.IsDark = false; public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Web;component/WeComStyle.xaml") }; }
[Display(Name = "CSDN风格", GroupName = "主流软件风格", Description = "参考主流软件视觉氛围生成的配色方案，不包含品牌素材", Order = 808, Prompt = "新增")]
public class CSDNStyleColorResource : ResxColorResourceBase { public CSDNStyleColorResource() => this.IsDark = false; public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Web;component/CSDNStyle.xaml") }; }
[Display(Name = "美团风格", GroupName = "主流软件风格", Description = "参考主流软件视觉氛围生成的配色方案，不包含品牌素材", Order = 809, Prompt = "新增")]
public class MeituanStyleColorResource : ResxColorResourceBase { public MeituanStyleColorResource() => this.IsDark = false; public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Web;component/MeituanStyle.xaml") }; }
[Display(Name = "大众点评风格", GroupName = "主流软件风格", Description = "参考主流软件视觉氛围生成的配色方案，不包含品牌素材", Order = 810, Prompt = "新增")]
public class DianpingStyleColorResource : ResxColorResourceBase { public DianpingStyleColorResource() => this.IsDark = false; public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Web;component/DianpingStyle.xaml") }; }
[Display(Name = "优酷风格", GroupName = "主流软件风格", Description = "参考主流软件视觉氛围生成的配色方案，不包含品牌素材", Order = 811, Prompt = "新增")]
public class YoukuStyleColorResource : ResxColorResourceBase { public YoukuStyleColorResource() => this.IsDark = true; public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Web;component/YoukuStyle.xaml") }; }
[Display(Name = "腾讯视频风格", GroupName = "主流软件风格", Description = "参考主流软件视觉氛围生成的配色方案，不包含品牌素材", Order = 812, Prompt = "新增")]
public class TencentVideoStyleColorResource : ResxColorResourceBase { public TencentVideoStyleColorResource() => this.IsDark = true; public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Web;component/TencentVideoStyle.xaml") }; }
[Display(Name = "爱奇艺风格", GroupName = "主流软件风格", Description = "参考主流软件视觉氛围生成的配色方案，不包含品牌素材", Order = 813, Prompt = "新增")]
public class IQIYIStyleColorResource : ResxColorResourceBase { public IQIYIStyleColorResource() => this.IsDark = true; public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Web;component/IQIYIStyle.xaml") }; }
[Display(Name = "WPS Office风格", GroupName = "主流软件风格", Description = "参考主流软件视觉氛围生成的配色方案，不包含品牌素材", Order = 814, Prompt = "新增")]
public class WPSOfficeStyleColorResource : ResxColorResourceBase { public WPSOfficeStyleColorResource() => this.IsDark = false; public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Web;component/WPSOfficeStyle.xaml") }; }
[Display(Name = "Microsoft Office风格", GroupName = "主流软件风格", Description = "参考主流软件视觉氛围生成的配色方案，不包含品牌素材", Order = 815, Prompt = "新增")]
public class MicrosoftOfficeStyleColorResource : ResxColorResourceBase { public MicrosoftOfficeStyleColorResource() => this.IsDark = false; public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Web;component/MicrosoftOfficeStyle.xaml") }; }
[Display(Name = "360安全卫士风格", GroupName = "主流软件风格", Description = "参考主流软件视觉氛围生成的配色方案，不包含品牌素材", Order = 816, Prompt = "新增")]
public class QihooSecurityStyleColorResource : ResxColorResourceBase { public QihooSecurityStyleColorResource() => this.IsDark = false; public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Web;component/QihooSecurityStyle.xaml") }; }
[Display(Name = "酷狗风格", GroupName = "主流软件风格", Description = "参考主流软件视觉氛围生成的配色方案，不包含品牌素材", Order = 817, Prompt = "新增")]
public class KugouStyleColorResource : ResxColorResourceBase { public KugouStyleColorResource() => this.IsDark = true; public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Web;component/KugouStyle.xaml") }; }
[Display(Name = "B站风格", GroupName = "主流软件风格", Description = "参考主流软件视觉氛围生成的配色方案，不包含品牌素材", Order = 818, Prompt = "新增")]
public class BilibiliStyleColorResource : ResxColorResourceBase { public BilibiliStyleColorResource() => this.IsDark = false; public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Web;component/BilibiliStyle.xaml") }; }
[Display(Name = "海康VisionMaster风格", GroupName = "主流软件风格", Description = "参考主流软件视觉氛围生成的配色方案，不包含品牌素材", Order = 819, Prompt = "新增")]
public class HikVisionMasterStyleColorResource : ResxColorResourceBase { public HikVisionMasterStyleColorResource() => this.IsDark = true; public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Web;component/HikVisionMasterStyle.xaml") }; }
[Display(Name = "VS2022蓝色风格", GroupName = "主流软件风格", Description = "参考主流软件视觉氛围生成的配色方案，不包含品牌素材", Order = 820, Prompt = "新增")]
public class VS2022BlueStyleColorResource : ResxColorResourceBase { public VS2022BlueStyleColorResource() => this.IsDark = true; public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Web;component/VS2022BlueStyle.xaml") }; }
[Display(Name = "VS2022深色风格", GroupName = "主流软件风格", Description = "参考主流软件视觉氛围生成的配色方案，不包含品牌素材", Order = 821, Prompt = "新增")]
public class VS2022DarkStyleColorResource : ResxColorResourceBase { public VS2022DarkStyleColorResource() => this.IsDark = true; public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Web;component/VS2022DarkStyle.xaml") }; }
[Display(Name = "VS2022浅色风格", GroupName = "主流软件风格", Description = "参考主流软件视觉氛围生成的配色方案，不包含品牌素材", Order = 822, Prompt = "新增")]
public class VS2022LightStyleColorResource : ResxColorResourceBase { public VS2022LightStyleColorResource() => this.IsDark = false; public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Web;component/VS2022LightStyle.xaml") }; }
[Display(Name = "王者荣耀风格", GroupName = "主流软件风格", Description = "参考主流软件视觉氛围生成的配色方案，不包含品牌素材", Order = 823, Prompt = "新增")]
public class HonorOfKingsStyleColorResource : ResxColorResourceBase { public HonorOfKingsStyleColorResource() => this.IsDark = true; public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Web;component/HonorOfKingsStyle.xaml") }; }
[Display(Name = "蛋仔派对风格", GroupName = "主流软件风格", Description = "参考主流软件视觉氛围生成的配色方案，不包含品牌素材", Order = 824, Prompt = "新增")]
public class EggyPartyStyleColorResource : ResxColorResourceBase { public EggyPartyStyleColorResource() => this.IsDark = false; public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Web;component/EggyPartyStyle.xaml") }; }
[Display(Name = "和平精英风格", GroupName = "主流软件风格", Description = "参考主流软件视觉氛围生成的配色方案，不包含品牌素材", Order = 825, Prompt = "新增")]
public class PeaceEliteStyleColorResource : ResxColorResourceBase { public PeaceEliteStyleColorResource() => this.IsDark = true; public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Web;component/PeaceEliteStyle.xaml") }; }
[Display(Name = "抖音风格", GroupName = "主流软件风格", Description = "参考主流软件视觉氛围生成的配色方案，不包含品牌素材", Order = 826, Prompt = "新增")]
public class DouyinStyleColorResource : ResxColorResourceBase { public DouyinStyleColorResource() => this.IsDark = true; public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Web;component/DouyinStyle.xaml") }; }
[Display(Name = "快手风格", GroupName = "主流软件风格", Description = "参考主流软件视觉氛围生成的配色方案，不包含品牌素材", Order = 827, Prompt = "新增")]
public class KuaishouStyleColorResource : ResxColorResourceBase { public KuaishouStyleColorResource() => this.IsDark = false; public override ResourceDictionary Resource => new ResourceDictionary() { Source = new Uri("pack://application:,,,/H.Themes.Colors.Web;component/KuaishouStyle.xaml") }; }
