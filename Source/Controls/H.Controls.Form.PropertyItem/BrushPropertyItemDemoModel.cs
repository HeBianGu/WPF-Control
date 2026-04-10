// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Controls.Form.PropertyItem.Attribute;
using H.Controls.Form.PropertyItem.ComboBoxPropertyItems;

namespace H.Controls.Form.PropertyItem
{
    public class BrushPropertyItemDemoModel
    {
        [Display(Name = "BrushesPropertyItem", Description = "演示应用BrushesComboBoxPropertyItem自定义显示样式")]
        [PropertyItem(typeof(BrushComboBoxPropertyItem))]
        public Brush Brush { get; set; } = Brushes.Red;

        [Display(Name = "GetStandardBrushesSource", Description = "演示应用StandardBrushesComboBoxPropertyItem自定义显示样式")]
        [GetStandardBrushesSource]
        [PropertyItem(typeof(BrushComboBoxPropertyItem))]
        public Brush Standard { get; set; } = Brushes.Red;

        [Display(Name = "ColorComboBoxPropertyItem", Description = "演示应用BrushesComboBoxPropertyItem自定义显示样式")]
        [PropertyItem(typeof(ColorComboBoxPropertyItem))]
        public Color Color { get; set; } = Colors.Red;

        [Display(Name = "GetStandardColorsSource", Description = "演示应用BrushesComboBoxPropertyItem自定义显示样式")]
        [GetStandardColorsSource]
        [PropertyItem(typeof(ColorComboBoxPropertyItem))]
        public Color StandardColor { get; set; } = Colors.Red;
    }
}
