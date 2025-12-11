// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Extensions.FontIcon;
using H.Extensions.Mvvm.Commands;
using System.IO;
using System.Text.Json.Serialization;

namespace H.Modules.Project.Base;

[Icon(FontIcons.DateTime)]
[Display(Name = "新建项目")]
public abstract class ProjectItemBase : DisplayBindableBase, IProjectItem
{
    [Required]
    [Display(Name = "标题", Order = 4)]
    public string Title
    {
        get { return this.Name; }
        set
        {
            this.Name = value;
            RaisePropertyChanged();
        }
    }

    private string _path;
    [ReadOnly(true)]
    [Browsable(false)]
    [Display(Name = "工程文件路径", Order = 4)]
    public string Path
    {
        get { return _path; }
        set
        {
            _path = value;
            RaisePropertyChanged();
        }
    }

    private bool _isFixed;
    [Display(Name = "是否固定", Order = 4)]
    public bool IsFixed
    {
        get { return _isFixed; }
        set
        {
            _isFixed = value;
            RaisePropertyChanged();
        }
    }

    private DateTime _createTime = DateTime.Now;
    [ReadOnly(true)]
    [Display(Name = "创建时间", Order = 4)]
    public DateTime CreateTime
    {
        get { return _createTime; }
        set
        {
            _createTime = value;
            RaisePropertyChanged();
        }
    }

    private DateTime _updateTime = DateTime.Now;
    [ReadOnly(true)]
    [Display(Name = "修改时间", Order = 4)]
    public DateTime UpdateTime
    {
        get { return _updateTime; }
        set
        {
            _updateTime = value;
            RaisePropertyChanged();
        }
    }

    public virtual bool Save(out string message)
    {
        message = null;
        object data = this.GetSaveFileData();
        if (data == null)
            return true;
        this.SaveToFile(data);
        this.UpdateTime = DateTime.Now;
        return true;
    }

    protected void SaveToFile(object data)
    {
        string path = this.GetFilePath();
        this.GetSerializer()?.Save(path, data);
    }
    public virtual bool Load(out string message)
    {
        message = null;
        return true;
    }

    protected virtual ISerializerService GetSerializer() => ProjectOptions.Instance.JsonSerializerService;

    protected virtual bool LoadFile<T>(out T value)
    {
        string path = this.GetFilePath();
        if (!File.Exists(path))
        {
            value = default;
            return false;
        }
        value = this.GetSerializer().Load<T>(path);
        return true;
    }

    protected virtual object GetSaveFileData()
    {
        return null;
    }

    public virtual bool Close(out string message)
    {
        message = null;
        return true;
    }

    public virtual string GetFilePath()
    {
        string folder = this.GetPath();
        return System.IO.Path.Combine(folder, this.Title + ProjectOptions.Instance.Extenstion);
    }

    private string GetPath()
    {
        if (string.IsNullOrEmpty(this.Path))
            return ProjectOptions.Instance.DefaultProjectFolder;
        return this.Path;
    }

    public virtual bool Delete(out string message)
    {
        bool r = this.Close(out message);
        if (r == false)
            return false;
        if (File.Exists(this.Path))
            File.Delete(this.Path);
        if (!string.IsNullOrEmpty(this.Path))
        {
            string find = this.GetFilePath();
            if (File.Exists(find))
                File.Delete(find);
        }
        return true;
    }
}
