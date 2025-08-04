// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")
using H.Services.Setting;
using System.ComponentModel.DataAnnotations;
using System.Windows.Media;

namespace H.Controls.ShapeBox
{
    public class CaliperShapeStyleSetting<T> : ShapeStyleSetting<T> where T : new()
    {
        [Display(Name = "卡尺颜色", GroupName = "数据")]
        public Brush MinorStroke { get; set; } = Brushes.LightBlue;
    }

    [Display(Name = "卡尺样式", GroupName = SettingGroupNames.GroupStyle, Description = "设置卡尺样式信息")]
    public class CaliperShapeStyleSetting : CaliperShapeStyleSetting<CaliperShapeStyleSetting>
    {

    }
}
