// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Controls.Form.PropertyItem.TextPropertyItems.Base;
using H.Services.Message;
using System.IO;

namespace H.Controls.Form.PropertyItem.TextPropertyItems;

public class OpenFileDialogFilterAttribute : System.Attribute
{
    public OpenFileDialogFilterAttribute(string filter)
    {
        this.Filter = filter;
    }
    public string Filter { get; set; }
}

public class OpenFileDialogPropertyItem : CommandsTextPropertyItemBase
{
    public OpenFileDialogPropertyItem(PropertyInfo property, object obj) : base(property, obj)
    {

    }

    [Display(Name = "浏览", Order = 2)]
    public DisplayCommand OpenCommand => new DisplayCommand(l =>
    {
        var filter = this.PropertyInfo.GetCustomAttribute<OpenFileDialogFilterAttribute>();
        var r = IocMessage.IOFileDialog.ShowOpenFile(x =>
        {
            if (File.Exists(this.Value))
                x.InitialDirectory = Path.GetDirectoryName(this.Value).GetFullPath();
            if (filter != null)
                x.Filter = filter.Filter;
        });
        if (!File.Exists(r))
            return;
        this.OnFilePathChanged(r);
    })
    { Name = "浏览" };

    protected virtual void OnFilePathChanged(string filePath)
    {
        this.Value = filePath;
    }
}

public class OpenFileDialogAppDomainRelativePropertyItem : OpenFileDialogPropertyItem
{
    public OpenFileDialogAppDomainRelativePropertyItem(PropertyInfo property, object obj) : base(property, obj)
    {

    }

    protected override void OnFilePathChanged(string filePath)
    {
        this.Value = filePath.GetAppDomainRelativePath();
    }
}

public class OpenFolderDialogPropertyItem : CommandsTextPropertyItemBase
{
    public OpenFolderDialogPropertyItem(PropertyInfo property, object obj) : base(property, obj)
    {

    }

    [Display(Name = "浏览", Order = 2)]
    public DisplayCommand OpenCommand => new DisplayCommand(l =>
    {
        var r = IocMessage.IOFolderDialog.ShowOpenFolder();
        if (!Directory.Exists(r))
            return;
        this.OnFilePathChanged(r);
    })
    { Name = "浏览" };

    protected virtual void OnFilePathChanged(string filePath)
    {
        this.Value = filePath;
    }
}

public class OpenFolderDialogAppDomainRelativePropertyItem : OpenFolderDialogPropertyItem
{
    public OpenFolderDialogAppDomainRelativePropertyItem(PropertyInfo property, object obj) : base(property, obj)
    {

    }

    protected override void OnFilePathChanged(string filePath)
    {
        this.Value = filePath.GetAppDomainRelativePath();
    }
}


public class SaveFileDialogPropertyItem : CommandsTextPropertyItemBase
{
    public SaveFileDialogPropertyItem(PropertyInfo property, object obj) : base(property, obj)
    {

    }

    [Display(Name = "浏览", Order = 2)]
    public DisplayCommand OpenCommand => new DisplayCommand(l =>
    {
        var filter = this.PropertyInfo.GetCustomAttribute<OpenFileDialogFilterAttribute>();
        var r = IocMessage.IOFileDialog.ShowSaveFile(x =>
        {
            if (File.Exists(this.Value))
                x.InitialDirectory = Path.GetDirectoryName(this.Value).GetFullPath();
            if (filter != null)
                x.Filter = filter.Filter;
        });
        if (!File.Exists(r))
            return;
        this.OnFilePathChanged(r);
    })
    { Name = "浏览" };

    protected virtual void OnFilePathChanged(string filePath)
    {
        this.Value = filePath;
    }

    public class SaveFileDialogAppDomainRelativePropertyItem : SaveFileDialogPropertyItem
    {
        public SaveFileDialogAppDomainRelativePropertyItem(PropertyInfo property, object obj) : base(property, obj)
        {

        }

        protected override void OnFilePathChanged(string filePath)
        {
            this.Value = filePath.GetAppDomainRelativePath();
        }
    }
}
