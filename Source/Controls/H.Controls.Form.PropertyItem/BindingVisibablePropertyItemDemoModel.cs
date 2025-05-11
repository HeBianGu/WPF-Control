// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Controls.Form.PropertyItem.ComboBoxPropertyItems;
using System.ComponentModel.DataAnnotations;
using System.Windows;

namespace H.Controls.Form.PropertyItem
{
    public class BindingVisibablePropertyItemDemoModel : SourcePropertyItemDemoModel
    {
        [Display(Name = "EnumSourcePropertyItem", Description = "演示应用PropertyItemAttribute自定义显示样式")]
        [RefreshOnValueChanged]
        [EnumSourcePropertyItem(typeof(ComboBoxPropertyItem))]
        public HorizontalAlignment HorizontalAlignment { get; set; }

        [Display(Name = "EnumComboBoxPropertyItem", Description = "演示应用PropertyItemAttribute自定义显示样式")]
        [RefreshOnValueChanged]
        [PropertyItem(typeof(EnumComboBoxPropertyItem))]
        public VerticalAlignment VerticalAlignment { get; set; }

        [BindingVisiblableMethodName(nameof(GetBindingHorizontalAlignmentVisible))]
        [Display(Name = "HorizontalAlignment=Right", Description = "演示应用PropertyItemAttribute自定义显示样式")]
        public string BindingHorizontalAlignment { get; set; } = "HorizontalAlignment=Right时我会显示";

        [BindingVisiblableMethodName(nameof(GetBindingVerticalAlignmentVisible))]
        [Display(Name = "VerticalAlignment=Bottom", Description = "演示应用PropertyItemAttribute自定义显示样式")]
        public string BindingVerticalAlignment { get; set; } = "VerticalAlignment=Bottom时我会显示";

        [BindingVisiblableMethodName(nameof(GetBindingBothVisible))]
        [Display(Name = "都选择Center时启用", Description = "演示应用PropertyItemAttribute自定义显示样式")]
        public string BindingBoth { get; set; } = "都选择Center时启用";

        public bool GetBindingVerticalAlignmentVisible()
        {
            return this.VerticalAlignment == VerticalAlignment.Bottom;
        }

        public bool GetBindingHorizontalAlignmentVisible()
        {
            return this.HorizontalAlignment == HorizontalAlignment.Right;
        }

        public bool GetBindingBothVisible()
        {
            return this.HorizontalAlignment == HorizontalAlignment.Center && this.VerticalAlignment == VerticalAlignment.Center;
        }
    }
}
