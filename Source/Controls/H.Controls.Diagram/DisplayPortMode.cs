﻿// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase



using H.Extensions.TypeConverter;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace H.Controls.Diagram
{
    [TypeConverter(typeof(DisplayEnumConverter))]
    public enum DisplayPortMode
    {
        [Display(Name = "始终")]
        Always = 0,
        [Display(Name = "悬停")]
        MouseOver,
        [Display(Name = "选中")]
        Selected
    }
}