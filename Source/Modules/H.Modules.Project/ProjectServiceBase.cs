// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control
global using H.Services.Serializable;
global using System.Collections;
global using System.ComponentModel.DataAnnotations;
using H.Common.Interfaces;
using H.Extensions.Common;
using H.Services.AppPath;
using H.Services.Setting;
using System.IO;

namespace H.Modules.Project;

[Display(Name = "工程数据", GroupName = SettingGroupNames.GroupData)]
public abstract class ProjectServiceBase<T> : BindableBase, IProjectService, IDataSource<T> where T : IProjectItem
{
    private readonly IOptions<ProjectOptions> _options;
    public ProjectServiceBase(IOptions<ProjectOptions> options)
    {
        _options = options;
    }

    private IEnumerable<T> _collection = new ObservableCollection<T>();
    [Browsable(false)]
    public IEnumerable<T> Collection
    {
        get { return _collection; }
        internal set
        {
            _collection = value;
            RaisePropertyChanged();
        }
    }

    private IProjectItem _current;

    public event EventHandler CollectionChanged;
    [System.Text.Json.Serialization.JsonIgnore]

    [System.Xml.Serialization.XmlIgnore]
    [Browsable(false)]
    public IProjectItem Current
    {
        get { return _current; }
        set
        {
            if (_current == value)
                return;
            IProjectItem old = _current;
            _current = value;
            old?.Save(out string message);
            old?.Close(out string messge);
            _current?.Load(out message);
            RaisePropertyChanged();
            this.OnCurrentChanged(old, _current);
        }
    }

    [System.Text.Json.Serialization.JsonIgnore]

    [System.Xml.Serialization.XmlIgnore]
    [Browsable(false)]
    public Action<IProjectItem, IProjectItem> CurrentChanged { get; set; }

    public string Name => "项目数据";

    protected virtual void OnCurrentChanged(IProjectItem o, IProjectItem n)
    {
        this.CurrentChanged?.Invoke(o, n);
    }

    public virtual IEnumerable<IProjectItem> Where(Func<IProjectItem, bool> func = null)
    {
        return func == null ? this.Collection.OfType<IProjectItem>() : this.Collection.Where(x => func(x)).OfType<IProjectItem>();
    }

    protected virtual ISerializerService GetSerializer() => ProjectOptions.Instance.JsonSerializerService;
    protected virtual void OnItemChanged()
    {
        if (this._options.Value.SaveMode == ProjectSaveMode.OnProjectChanged)
            this.Save(out string message);
    }

    public virtual bool Save(out string message)
    {
        message = null;
        try
        {
            this.GetSerializer().Save(this._projectsPath, new Projects<T>() { Items = this.Collection.ToList() });
            this.Current?.Save(out message);
            return true;
        }
        catch (Exception ex)
        {
            message = ex.Message;
            IocLog.Instance?.Error(ex);
            return false;
        }
    }

    public virtual void Add(params T[] ts)
    {
        foreach (T item in ts)
        {
            if (this.Collection.Contains(item))
                return;
            if (this.Collection is IList list)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    list.Insert(0, item);
                });
            }
            this.OnItemChanged();
        }
    }

    public virtual void Delete(params T[] ts)
    {
        foreach (T item in ts)
        {
            item.Delete(out string message);
            if (this.Collection is IList list)
                list.Remove(item);
        }
        this.OnItemChanged();
    }

    IProjectItem IProjectService.Create() => this.Create();

    public abstract T Create();

    void IProjectService.Add(IProjectItem project)
    {
        if (project is T t)
            this.Add(t);
    }
    void IProjectService.Delete(Func<IProjectItem, bool> func)
    {
        IEnumerable<T> ps = this.Collection.Where(x => func(x));
        this.Delete(ps.ToArray());
    }

    private string _projectsPath => System.IO.Path.Combine(this.GetFolderPath(), "projects.json");

    protected virtual string GetFolderPath() => AppPaths.Instance.UserProject;

    public virtual bool Load(out string message)
    {
        message = string.Empty;
        Projects<T> data = !File.Exists(this._projectsPath) ? this.LoadDefaultProjects() : this.GetSerializer().Load<Projects<T>>(this._projectsPath);
        this.Clear();
        if (data != null)
        {
            var orders = data.Items.OrderByDescending(x => x.UpdateTime);
            foreach (T item in orders)
            {
                this.Add(data.Items.ToArray());
            }
            this.Current = orders.FirstOrDefault();
        }
        if (this.Current == null)
        {
            this.Current = this.Create();
            if (string.IsNullOrEmpty(this.Current.Title))
                this.Current.Title = ProjectOptions.Instance.DefaultProjectName + (this.Collection.Count() + 1).ToString();
        }
        return true;
    }

    protected virtual Projects<T> LoadDefaultProjects()
    {
        string path = AppPaths.Instance.DefaultProjects;
        string toPath = this.GetFolderPath();
        path.ToDirectoryEx().BackupToDirectory(toPath);
        Projects<T> data = this.GetSerializer().Load<Projects<T>>(this._projectsPath);
        if (data == null)
            return null;
        return data;
    }

    public void Clear()
    {
        if (this.Collection is IList list)
            list.Clear();
        this.Current = null;
    }

}

public class Projects<T>
{
    public List<T> Items { get; set; }
}
