// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Common.Attributes;
using H.Controls.Form.PropertyItem.ComboBoxPropertyItems;
using H.Controls.Form.PropertyItem.TextPropertyItems;
using H.Extensions.FontIcon;
using System.Windows.Input;

namespace H.Controls.Form.PropertyItem;

public class PropertyItemDemoModel
{
    [Display(Name = "RadioButtonEnumPropertyItem", GroupName = "其他", Description = "演示应用PropertyItemAttribute自定义显示样式")]
    [PropertyItem(typeof(RadioButtonEnumPropertyItem))]
    public HorizontalAlignment HorizontalAlignment { get; set; }

    [PropertyItem(typeof(DoubleSliderTextPropertyItem))]
    [Range(0, 100)]
    [Display(Name = "DoubleSliderTextPropertyItem", GroupName = "Slider", Description = "演示应用PropertyItemAttribute自定义显示样式")]
    public double Value1 { get; set; }

    [PropertyItem(typeof(Int32SliderTextPropertyItem))]
    [Range(0, 100)]
    [Display(Name = "Int32SliderTextPropertyItem", GroupName = "Slider", Description = "演示应用PropertyItemAttribute自定义显示样式")]
    public int Value2 { get; set; }

    [DefaultValue(2)]
    [Display(Name = "DefaultValue", GroupName = "其他", Description = "演示应用PropertyItemAttribute自定义显示样式")]
    public int Value3 { get; set; }


    [PropertyItem(typeof(DeleteTextPropertyItem))]
    [Display(Name = "DeleteTextPropertyItem", GroupName = "其他", Description = "演示应用PropertyItemAttribute自定义显示样式")]
    public string Value4 { get; set; } = "DeleteTextPropertyItem";

    [PropertyItem(typeof(HyperlinkPropertyItem))]
    [Display(Name = "HyperlinkPropertyItem", GroupName = "其他", Description = "演示应用PropertyItemAttribute自定义显示样式")]
    public string Value5 { get; set; } = "https://github.com/HeBianGu";

    [Unit("kg")]
    [PropertyItem(typeof(UnitTextPropertyItem))]
    [Display(Name = "UnitTextPropertyItem", GroupName = "其他", Description = "演示应用PropertyItemAttribute自定义显示样式")]
    public double Value6 { get; set; } = 0.25;

    [PropertyItem(typeof(FormPropertyItem))]
    [Display(Name = "UseTitle = true", GroupName = "UseTitle", Description = "演示应用PropertyItemAttribute自定义显示样式")]
    public ChildPropertyItemDemoModel ChildPropertyItemDemoModel1 { get; set; } = new ChildPropertyItemDemoModel();

    [PropertyStyle(UseTitle = false)]
    [PropertyItem(typeof(FormPropertyItem))]
    [Display(Name = "UseTitle = false", GroupName = "UseTitle", Description = "演示应用PropertyItemAttribute自定义显示样式")]
    public ChildPropertyItemDemoModel ChildPropertyItemDemoModel2 { get; set; } = new ChildPropertyItemDemoModel();

    [Display(Name = "MyCommand", GroupName = "命令", Description = "演示应用PropertyItemAttribute自定义显示样式")]
    public RelayCommand MyCommand => new RelayCommand(x =>
    {
        if (x is string t)
        {

        }
    });
    [Icon(FontIcons.Cancel)]
    [PropertyItem(typeof(FontIconButtonCommandPropertyItem))]
    [Display(Name = "FontIconButtonCommandPropertyItem", GroupName = "命令", Description = "演示应用PropertyItemAttribute自定义显示样式")]
    public RelayCommand MyCommand1 => new RelayCommand(x =>
    {
        if (x is string t)
        {

        }
    });

}


public class ChildPropertyItemDemoModel
{
    [Display(Name = "EnumPropertyItem", Description = "演示应用PropertyItemAttribute自定义显示样式")]
    public VerticalAlignment VerticalAlignment { get; set; }
}


public class TabPropertyItemDemoModel
{
    [Tab(CommandGroupNames.MenuBar)]
    [Display(Name = "参数1", GroupName = "分组1", Description = "演示应用PropertyItemAttribute自定义显示样式")]
    public bool Value1 { get; set; }

    [Tab(CommandGroupNames.ToolBar)]
    [Display(Name = "参数2", GroupName = "分组1", Description = "演示应用PropertyItemAttribute自定义显示样式")]
    public bool Value2 { get; set; }
    [Tab(CommandGroupNames.MenuBar)]
    [Display(Name = "参数3", GroupName = "分组2", Description = "演示应用PropertyItemAttribute自定义显示样式")]
    public bool Value3 { get; set; }
    [Tab(CommandGroupNames.ToolBar)]
    [Display(Name = "参数4", GroupName = "分组2", Description = "演示应用PropertyItemAttribute自定义显示样式")]
    public bool Value4 { get; set; }
    [Tab(CommandGroupNames.ToolBar)]
    [Display(Name = "参数5", GroupName = "分组1", Description = "演示应用PropertyItemAttribute自定义显示样式")]
    public bool Value5 { get; set; }
}
