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
    public class OpenDeleteSystemPathTextPropertyItem : CommandsTextPropertyItemBase
    {
        public OpenDeleteSystemPathTextPropertyItem(PropertyInfo property, object obj) : base(property, obj)
        {

        }

        [Display(Name = "打开文件")]
        public DisplayCommand OpenCommand => new DisplayCommand(l =>
        {
            Process.Start(new ProcessStartInfo(this.Value) { UseShellExecute = true });
        }, l =>
        {
            return File.Exists(this.Value) | Directory.Exists(this.Value);
        });

        [Display(Name = "打开文件夹")]
        public DisplayCommand OpenFolderCommand => new DisplayCommand(l =>
        {
            string folder = Path.GetDirectoryName(this.Value);
            if (Directory.Exists(folder))
                Process.Start(new ProcessStartInfo(folder) { UseShellExecute = true });
        }, l =>
        {
            return File.Exists(this.Value) | Directory.Exists(this.Value);
        });
        [Display(Name = "删除")]
        public DisplayCommand ClearCommand => new DisplayCommand(async l =>
        {
            bool? r = await IocMessage.ShowDialogMessage("删除文件无法恢复，确定删除?");
            if (r != true)
                return;
            //try
            //{
            if (File.Exists(this.Value))
                File.Delete(this.Value);

            if (Directory.Exists(this.Value))
                Directory.Delete(this.Value, true);

            //MessageProxy.Snacker?.ShowTime("删除成功");
            //}
            //catch (Exception ex)
            //{
            //    MessageProxy.Snacker?.ShowTime("删除失败,详情请查看日志");
            //    Logger.Instance?.Error(ex);
            //}
        }, l =>
        {
            return File.Exists(this.Value) | Directory.Exists(this.Value);
        });

    }
}
