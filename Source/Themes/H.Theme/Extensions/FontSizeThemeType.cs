// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace H.Themes.Extensions;
public enum FontSizeThemeType
{
    [Display(Name = "常规")]
    Default = 0,
    [Display(Name = "大")]
    Large,
    [Display(Name = "小")]
    Small
}
