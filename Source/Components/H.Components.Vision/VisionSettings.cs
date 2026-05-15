// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Controls.Form.PropertyItem.ComboBoxPropertyItems;
using H.Extensions.Setting;
using H.Services.Setting;

namespace H.Components.Vision;
[Display(Name = "视觉参数设置", GroupName = SettingGroupNames.GroupBase, Description = "设置向导页面信息")]
public class VisionSettings : Settable<VisionSettings>
{
    private Color _outputColor = Colors.Chartreuse;
    [GetHightlightColorsSource]
    [PropertyItem(typeof(ColorComboBoxPropertyItem))]
    [Display(Name = "绘制颜色", GroupName = "输出样式")]
    public Color OutputColor
    {
        get { return _outputColor; }
        set
        {
            _outputColor = value;
            RaisePropertyChanged();
        }
    }

    private Color _outputLabelColor = Colors.Black;
    [GetHightlightColorsSource]
    [PropertyItem(typeof(ColorComboBoxPropertyItem))]
    [Display(Name = "标签颜色", GroupName = "输出样式")]
    public Color OutputLabelColor
    {
        get { return _outputLabelColor; }
        set
        {
            _outputLabelColor = value;
            RaisePropertyChanged();
        }
    }

    private double _OutputThickness = 1.0;
    [DefaultValue(1.0)]
    [Display(Name = "标签线条参数", GroupName = "输出样式")]
    public double OutputThickness
    {
        get { return _OutputThickness; }
        set
        {
            _OutputThickness = value;
            RaisePropertyChanged();
        }
    }

    public override void LoadDefault()
    {
        base.LoadDefault();
        this.OutputColor = Colors.Chartreuse;
        this.OutputLabelColor = Colors.Black;
    }
}
