// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using H.Extensions.TypeConverter;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace H.Themes.Default
{
    [TypeConverter(typeof(DisplayEnumConverter))]
    public enum LayoutThemeType
    {
        [Display(Name = "常规")]
        Default = 0,
        [Display(Name = "宽松")]
        Large,
        [Display(Name = "紧凑")]
        Small
    }
}
