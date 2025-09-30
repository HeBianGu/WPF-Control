// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Controls.Form.PropertyItem.TextPropertyItems;
using System.ComponentModel.DataAnnotations;

namespace H.Controls.Form.PropertyItem
{

    public class TextPropertyItemDemoModel
    {
        [Display(Name = "OpenFileDialogPropertyItem", Description = "演示应用PropertyItemAttribute自定义显示样式")]
        [PropertyItem(typeof(OpenFileDialogPropertyItem))]
        public string Path { get; set; }

        [Display(Name = "OpenDeleteSystemPathTextPropertyItem", Description = "演示应用PropertyItemAttribute自定义显示样式")]
        [PropertyItem(typeof(OpenDeleteSystemPathTextPropertyItem))]
        public string Path1 { get; set; }

        [Display(Name = "DeleteSystemPathTextPropertyItem", Description = "演示应用PropertyItemAttribute自定义显示样式")]
        [PropertyItem(typeof(DeleteSystemPathTextPropertyItem))]
        public string Path2 { get; set; }

        [Display(Name = "OpenSystemPathTextPropertyItem", Description = "演示应用PropertyItemAttribute自定义显示样式")]
        [PropertyItem(typeof(OpenSystemPathTextPropertyItem))]
        public string Path3 { get; set; }

        [Display(Name = "PasswordTextPropertyItem", Description = "演示应用PropertyItemAttribute自定义显示样式")]
        [PropertyItem(typeof(PasswordTextPropertyItem))]
        public string Password { get; set; } = "Password";
    }
}
