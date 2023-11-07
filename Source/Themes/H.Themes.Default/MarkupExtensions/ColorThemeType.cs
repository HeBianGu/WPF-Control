// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using H.Extensions.TypeConverter;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace H.Themes.Default
{
    [TypeConverter(typeof(DisplayEnumConverter))]
    public enum ColorThemeType
    {
        [Display(Name = "常规")]
        Default = 0,
        [Display(Name = "深色")]
        Dark,
        [Display(Name = "深灰")]
        DarkGray,
        [Display(Name = "浅色")]
        Light,
        [Display(Name = "主色")]
        LightAccent
    }
}
