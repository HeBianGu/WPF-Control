// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")
using H.Controls.ShapeBox.Shapes;
using H.Services.Setting;
using System.ComponentModel.DataAnnotations;

namespace H.Controls.ShapeBox
{
    [Display(Name = "标尺样式", GroupName = SettingGroupNames.GroupStyle, Description = "设置标尺样式信息")]
    public class RulerLineShapeStyleSetting : LineShapeStyleSetting<ROIRectStateStyleSetting>
    {
        [Display(Name = "主卡尺数量", GroupName = "样式")]
        public int MajorRularCount { get; set; } = 10;

        [Display(Name = "次卡尺数量", GroupName = "样式")]
        public int MinorRularCount { get; set; } = 5;

        [Display(Name = "卡尺位移", GroupName = "样式")]
        public double MajorOffset { get; set; } = 20.0;

        public ROIRectShape CreateROIRectShape()
        {
            return this.Create<ROIRectShape>();
        }
    }
}
