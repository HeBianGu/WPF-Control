// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


using H.Services.AppPath;
using System.IO;

namespace H.Modules.Project.Base;

public abstract class ProjectItemBase : CommandsBindableBase, IProjectItem
{
    private string _title;
    [Required]
    [Display(Name = "标题", Order = 4)]
    public string Title
    {
        get { return _title; }
        set
        {
            _title = value;
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
    [Browsable(false)]
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
    [Browsable(false)]
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

    public string Name => this.Title;

    public virtual bool Save(out string message)
    {
        message = null;
        object data = this.GetSaveFileData();
        if (data == null)
            return true;
        this.SaveToFile(data);
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

    public virtual void Dispose()
    {

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
