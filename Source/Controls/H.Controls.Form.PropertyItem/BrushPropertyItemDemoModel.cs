// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Controls.Form.PropertyItem.ComboBoxPropertyItems;

namespace H.Controls.Form.PropertyItem
{
    public class BrushPropertyItemDemoModel
    {
        [Display(Name = "BrushesPropertyItem", Description = "演示应用BrushesComboBoxPropertyItem自定义显示样式")]
        [PropertyItem(typeof(BrushComboBoxPropertyItem))]
        public Brush Brush { get; set; } = Brushes.Red;

        [Display(Name = "StandardBrushesPropertyItem", Description = "演示应用StandardBrushesComboBoxPropertyItem自定义显示样式")]
        [StandardBrushesPropertyItem(typeof(BrushComboBoxPropertyItem))]
        public Brush Standard { get; set; } = Brushes.Red;

        [Display(Name = "ColorsPropertyItem", Description = "演示应用BrushesComboBoxPropertyItem自定义显示样式")]
        [PropertyItem(typeof(ColorComboBoxPropertyItem))]
        public Color Color { get; set; } = Colors.Red;

        [Display(Name = "StandardColorsPropertyItem", Description = "演示应用BrushesComboBoxPropertyItem自定义显示样式")]
        [StandardColorsPropertyItem(typeof(ColorComboBoxPropertyItem))]
        public Color StandardColor { get; set; } = Colors.Red;
    }
}
