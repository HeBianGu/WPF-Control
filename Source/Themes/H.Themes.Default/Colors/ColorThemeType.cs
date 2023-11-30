// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.ComponentModel.DataAnnotations;

namespace H.Themes.Default
{
    public enum ColorThemeType
    {
        [Display(Name = "常规")]
        Default = 0,
        [Display(Name = "深色")]
        Dark,
        [Display(Name = "浅色")]
        Light
    }
}
