// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Extensions.Common;
using H.Extensions.Setting;
using H.Services.Setting;

namespace H.Controls.ShapeBox.Settables.ViewSettables;
[Display(Name = "悬停绘制样式设置", GroupName = SettingGroupNames.GroupStyle, Description = "悬停绘制样式设置的信息")]
public class MouseOverShapeViewSetting : DrawingStyleSettable<MouseOverShapeViewSetting>
{
    public override void LoadDefault()
    {
        base.LoadDefault();
        this.Stroke = "#FFFF00".ToFreezeSolid();
        this.StrokeThickness = 3;
    }
}
