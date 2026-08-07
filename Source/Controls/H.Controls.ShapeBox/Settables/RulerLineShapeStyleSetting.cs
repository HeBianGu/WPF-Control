// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Controls.ShapeBox.Base;
using H.Services.Setting;

namespace H.Controls.ShapeBox.Settables;
[Display(Name = "标尺样式", GroupName = SettingGroupNames.GroupStyle, Description = "设置标尺样式信息")]
public class RulerLineShapeStyleSetting : LineShapeStyleSetting<ROIRectStateStyleSetting>
{
    [DefaultValue(10)]
    [Display(Name = "主卡尺数量", GroupName = ShapePropertyGroupNames.StyleGroup)]
    public int MajorRularCount { get; set; } = 10;
    [DefaultValue(5)]
    [Display(Name = "次卡尺数量", GroupName = ShapePropertyGroupNames.StyleGroup)]
    public int MinorRularCount { get; set; } = 5;
    [DefaultValue(20.0)]
    [Display(Name = "卡尺位移", GroupName = ShapePropertyGroupNames.StyleGroup)]
    public double MajorOffset { get; set; } = 20.0;

    public ROIRectShape CreateROIRectShape()
    {
        return this.Create<ROIRectShape>();
    }
}
