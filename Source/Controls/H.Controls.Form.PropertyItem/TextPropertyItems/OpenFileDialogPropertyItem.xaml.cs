// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Controls.Form.PropertyItem.TextPropertyItems.Base;
using H.Services.Message;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace H.Controls.Form.PropertyItem.TextPropertyItems
{
    public class OpenFileDialogPropertyItem : CommandsTextPropertyItemBase
    {
        public OpenFileDialogPropertyItem(PropertyInfo property, object obj) : base(property, obj)
        {

        }

        [Display(Name = "浏览", Order = 2)]
        public DisplayCommand OpenCommand => new DisplayCommand(l =>
        {
            var r = IocMessage.IOFileDialog.ShowOpenFile(x =>
            {
                if (File.Exists(this.Value))
                    x.InitialDirectory = Path.GetDirectoryName(this.Value);
            });
            if (!File.Exists(r))
                return;
            this.Value = r;
        })
        { Name = "浏览" };
    }

}
