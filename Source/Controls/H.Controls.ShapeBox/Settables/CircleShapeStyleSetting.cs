// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")
using H.Services.Setting;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace H.Controls.ShapeBox
{
    public class CircleShapeStyleSetting<T> : ShapeStyleSetting<T> where T : new()
    {
        [DefaultValue(false)]
        [Display(Name = "启用交线", GroupName = "样式")]
        public bool UseCross { get; set; } = false;
        [DefaultValue(true)]
        [Display(Name = "启用标尺", GroupName = "样式")]
        public bool UseDimension { get; set; } = true;
    }

    [Display(Name = "圆样式", GroupName = SettingGroupNames.GroupStyle, Description = "设置圆样式信息")]
    public class CircleShapeStyleSetting : CircleShapeStyleSetting<CircleShapeStyleSetting>
    {
     
    }
}
