// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase
using H.Extensions.XmlSerialize;
using H.Modules.Login;
using H.Providers.Ioc;
using H.Providers.Mvvm;
using Microsoft.Extensions.Options;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace H.Modules.Project
{
    [Display(Name = "工程数据", GroupName = SettingGroupNames.GroupData)]
    public abstract class ProjectServiceBase<T> : ViewModelBase, IProjectService, IDataSource<T> where T : IProjectItem
    {
        IOptions<ProjectOptions> _options;
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

        [JsonIgnore]
        [XmlIgnore]
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

        [JsonIgnore]
        [XmlIgnore]
        [Browsable(false)]
        public Action<IProjectItem, IProjectItem> CurrentChanged { get; set; }

        public string Name => "工程数据";

        protected virtual void OnCurrentChanged(IProjectItem o, IProjectItem n)
        {
            this.CurrentChanged?.Invoke(o, n);
        }

        public virtual IEnumerable<IProjectItem> Where(Func<IProjectItem, bool> func = null)
        {
            return func == null ? this.Collection.OfType<IProjectItem>() : this.Collection.Where(x => func(x)).OfType<IProjectItem>();
        }

        protected ISerializerService GetSerializer() => new JsonSerializerService();
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
                this.GetSerializer().Save(ProjectOptions.Instance.HistoryPath, new ProjectHistroyData<T>() { ProjectItems = this.Collection.ToList() });
                return true;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return false;
            }
        }

        public void Add(params T[] ts)
        {
            foreach (var item in ts)
            {
                if (this.Collection.Contains(item))
                    return;
                if (this.Collection is IList list)
                    list.Add(item);
                this.OnItemChanged();
            }
        }

        public void Delete(params T[] ts)
        {
            foreach (var item in ts)
            {
                if (File.Exists(item.Path))
                    File.Delete(item.Path);
                if (!string.IsNullOrEmpty(item.Path))
                {
                    var find = Path.Combine(item.Path, item.Title + "" + this._options.Value.Extenstion);
                    if (File.Exists(find))
                        File.Delete(find);
                }
                if (this.Collection is IList list)
                    list.Remove(item);
            }
            this.OnItemChanged();
        }
        public abstract IProjectItem Create();

        public void Add(IProjectItem project)
        {
            if (project is T t)
                this.Add(t);
        }

        public void Delete(Func<IProjectItem, bool> func)
        {
            var ps = this.Collection.Where(x => func(x));
            this.Delete(ps.ToArray());
        }

        public virtual bool Load(out string message)
        {
            message = string.Empty;
            var data = this.GetSerializer().Load<ProjectHistroyData<T>>(ProjectOptions.Instance.HistoryPath);
            this.Clear();
            if (data != null)
            {
                foreach (var item in data.ProjectItems)
                {
                    this.Add(data.ProjectItems.ToArray());
                }
                this.Current = data.ProjectItems.FirstOrDefault();
            }
            if (this.Current == null)
            {
                this.Current = this.Create();
                this.Current.Title = "新建项目";
            }
            return true;
        }


        public void Clear()
        {
            if (this.Collection is IList list)
                list.Clear();
        }

    }

    public class ProjectHistroyData<T>
    {
        public List<T> ProjectItems { get; set; }
    }
}
