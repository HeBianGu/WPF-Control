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
using System.Windows.Media;

namespace H.Controls.ShapeBox
{
    [Display(Name = "ROI样式", GroupName = SettingGroupNames.GroupStyle, Description = "设置ROI样式信息")]
    public class ROIRectStateStyleSetting : ShapeStyleSetting<ROIRectStateStyleSetting>
    {
        [Display(Name = "句柄长度", GroupName = "样式")]
        public double HandleLength { get; set; } = 6.0;
        [Display(Name = "启用交线", GroupName = "样式")]
        public bool UseCross { get; set; } = true;
        [Display(Name = "启用文本", GroupName = "样式")]
        public bool UseText { get; set; } = true;

        public ROIRectShape CreateROIRectShape()
        {
            return this.Create<ROIRectShape>();
        }
    }
}
