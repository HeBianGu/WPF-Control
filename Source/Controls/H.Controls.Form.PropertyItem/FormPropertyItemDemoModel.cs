// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

global using H.Controls.Form.Attributes;
using System.ComponentModel.DataAnnotations;

namespace H.Controls.Form.PropertyItem
{
    public class FormPropertyItemDemoModel : SourcePropertyItemDemoModel
    {
        [Display(Name = "名称")]
        public string Name { get; set; }

        [Display(Name = "值")]
        public string Value { get; set; }
        [Display(Name = "FormPropertyItem", Description = "演示应用PropertyItemAttribute自定义显示样式")]
        [PropertyItem(typeof(FormPropertyItem))]
        public DemoModelItem DemoModelItem { get; set; } = new DemoModelItem();

        [Display(Name = "ExpanderFormPropertyItem", Description = "演示应用PropertyItemAttribute自定义显示样式")]
        [PropertyItem(typeof(ExpanderFormPropertyItem))]
        public DemoModelItem DemoModelItemExpander { get; set; } = new DemoModelItem();

        [Display(Name = "Default", Description = "演示应用PropertyItemAttribute自定义显示样式")]
        public DemoModelItem DemoModelItemDefault { get; set; } = new DemoModelItem();
    }

}
