// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Controls.Form.Attributes;
using H.Controls.Form.PropertyItem.Attribute;
using H.Controls.Form.PropertyItem.ComboBoxPropertyItems;
using H.Extensions.Common;
using H.Extensions.Setting;
using H.Services.Setting;

namespace H.Controls.ShapeBox.Settables.ViewSettables;
[Display(Name = "选中绘制样式设置", GroupName = SettingGroupNames.GroupStyle, Description = "绘制样式设置的信息")]
public class SelectShapeViewSetting : ShapeStyleSetting<SelectShapeViewSetting>
{
    public override void LoadDefault()
    {
        base.LoadDefault();
        this.Stroke = "#00FF00".ToFreezeSolid();
        this.StrokeThickness = 3;
    }
}