// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control
using H.Controls.Form.PropertyItem.PropertyItems;
using System.ComponentModel.DataAnnotations;

namespace H.Controls.Form.PropertyItem
{
    public class FormPropertyItemDemoModel : SourcePropertyItemDemoModel
    {
        [Display(Name = "FormPropertyItem", Description = "演示应用PropertyItemAttribute自定义显示样式")]
        [PropertyItem(typeof(FormPropertyItem))]
        public DemoModelItem DemoModelItem { get; set; } = new DemoModelItem();

        [Display(Name = "Default", Description = "演示应用PropertyItemAttribute自定义显示样式")]
        public DemoModelItem DemoModelItemDefault { get; set; } = new DemoModelItem();
    }
}
