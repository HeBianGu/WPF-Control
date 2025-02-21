﻿// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control
using H.Controls.Form.PropertyItem.TextPropertyItems.Base;
using H.Mvvm;
using H.Services.Common;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace H.Controls.Form.PropertyItem.TextPropertyItems
{
    public class OpenDeleteSystemPathTextPropertyItem : CommandsTextPropertyItemBase
    {
        public OpenDeleteSystemPathTextPropertyItem(PropertyInfo property, object obj) : base(property, obj)
        {

        }

        public RelayCommand OpenCommand => new RelayCommand(l =>
        {
            Process.Start(new ProcessStartInfo(this.Value) { UseShellExecute = true });
        }, l =>
        {
            return File.Exists(this.Value) | Directory.Exists(this.Value);
        })
        { Name = "打开文件" };

        public RelayCommand OpenFolderCommand => new RelayCommand(l =>
        {
            string folder = Path.GetDirectoryName(this.Value);
            if (Directory.Exists(folder))
                Process.Start(new ProcessStartInfo(folder) { UseShellExecute = true });
        }, l =>
        {
            return File.Exists(this.Value) | Directory.Exists(this.Value);
        })
        { Name = "打开文件夹" };

        public RelayCommand ClearCommand => new RelayCommand(async l =>
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
        })
        {
            Name = "删除"
        };

    }
}
