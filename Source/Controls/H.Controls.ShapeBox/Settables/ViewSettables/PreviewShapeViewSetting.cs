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
[Display(Name = "预览绘制样式设置", GroupName = SettingGroupNames.GroupStyle, Description = "绘制样式设置设置的信息")]
public class PreviewShapeViewSetting : DrawingStyleSettable<PreviewShapeViewSetting>
{
    public override void LoadDefault()
    {
        base.LoadDefault();
        this.Stroke = "#00FFFF".ToFreezeSolid();
        this.StrokeThickness = 1.5;
    }
}
