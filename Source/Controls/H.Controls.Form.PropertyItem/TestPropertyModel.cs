// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control
using H.Controls.Form.PropertyItem.PropertyItems;
using System.ComponentModel.DataAnnotations;

namespace H.Controls.Form.PropertyItem
{
    public class FormDemoModel
    {
        [Display(Name = "路径", Description = "演示应用PropertyItemAttribute自定义显示样式")]
        [PropertyItem(typeof(OpenFileDialogPropertyItem))]
        public string Path { get; set; }

        [Display(Name = "路径", Description = "演示应用PropertyItemAttribute自定义显示样式")]
        [PropertyItem(typeof(OpenDeleteSystemPathTextPropertyItem))]
        public string Path1 { get; set; }

        [Display(Name = "路径", Description = "演示应用PropertyItemAttribute自定义显示样式")]
        [PropertyItem(typeof(ClearPathTextPropertyItem))]
        public string Path2 { get; set; }

        [Display(Name = "路径", Description = "演示应用PropertyItemAttribute自定义显示样式")]
        [PropertyItem(typeof(OpenSystemPathTextPropertyItem))]
        public string Path3 { get; set; }

    }
}
