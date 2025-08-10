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
    [Display(Name = "矩形样式", GroupName = SettingGroupNames.GroupStyle, Description = "设置直线样式信息")]
    public class RectShapeStyleSetting : RectShapeStyleSetting<RectShapeStyleSetting>
    {

    }

    public class RectShapeStyleSetting<T> : ShapeStyleSetting<T> where T : new()
    {
        [DefaultValue(true)]
        [Display(Name = "启用交线", GroupName = "样式")]
        public bool UseCross { get; set; } = true;
        [DefaultValue(false)]
        [Display(Name = "启用文本", GroupName = "样式")]
        public bool UseText { get; set; } = false;
    }
}
