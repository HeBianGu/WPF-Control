// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Common.Attributes;
using H.Controls.Form.PropertyItem.TextPropertyItems.Base;
using H.Services.Message;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace H.Controls.Form.PropertyItem.TextPropertyItems
{
    public class DeleteTextPropertyItem : CommandsTextPropertyItemBase
    {
        public DeleteTextPropertyItem(PropertyInfo property, object obj) : base(property, obj)
        {

        }

        [Display(Name = "删除")]
        public DisplayCommand ClearCommand => new DisplayCommand(async l =>
        {
            this.Value = null;
        }, l => !string.IsNullOrEmpty(this.Value));

        protected override IEnumerable<IDisplayCommand> CreateCommands()
        {
            return this.CreateCommands(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.DeclaredOnly);
        }
    }
}
