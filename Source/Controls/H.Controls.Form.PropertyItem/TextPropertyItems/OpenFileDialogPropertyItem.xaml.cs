// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control
using H.Controls.Form.PropertyItem.TextPropertyItems.Base;
using H.Mvvm;
using H.Services.Common;
using Microsoft.Win32;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Reflection;

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
            if (File.Exists(this.Value))
                openFileDialog.InitialDirectory = Path.GetDirectoryName(this.Value);
            var r = IocMessage.IOFileDialog.ShowOpenFile();
            this.Value = openFileDialog.FileName;
        })
        { Name = "浏览" };
    }

}
