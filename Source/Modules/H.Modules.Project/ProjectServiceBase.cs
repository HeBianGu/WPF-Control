// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase
using H.Extensions.XmlSerialize;
using H.Modules.Login;
using H.Providers.Ioc;
using H.Providers.Mvvm;
using Microsoft.Extensions.Options;
using System;
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
    [Display(Name = "工程数据", GroupName = SystemSetting.GroupData)]
    public abstract class ProjectServiceBase : NotifyPropertyChangedBase, ISave
    {
        IOptions<ProjectOptions> _options;
        public ProjectServiceBase(IOptions<ProjectOptions> options)
        {
            _options = options;
        }

        private ObservableCollection<IProjectItem> _projects = new ObservableCollection<IProjectItem>();
        [Browsable(false)]
        public ObservableCollection<IProjectItem> Projects
        {
            get { return _projects; }
            internal set
            {
                _projects = value;
                RaisePropertyChanged();
            }
        }

        private IProjectItem _current;
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

        public virtual void Add(IProjectItem project)
        {
            if (this.Projects.Contains(project)) return;
            this.Projects.Add(project);
            this.OnItemChanged();
        }

        public virtual void Delete(Func<IProjectItem, bool> func)
        {
            var finds = this.Projects.Where(func).ToList();
            foreach (var item in finds)
            {
                if (File.Exists(item.Path))
                    File.Delete(item.Path);
                if (!string.IsNullOrEmpty(item.Path))
                {
                    var find = Path.Combine(item.Path, item.Title + "" + this._options.Value.Extenstion);
                    if (File.Exists(find))
                        File.Delete(find);
                }

                this.Projects.Remove(item);
            }
            this.OnItemChanged();
        }

        public virtual IEnumerable<IProjectItem> Where(Func<IProjectItem, bool> func = null)
        {
            return func == null ? this.Projects : this.Projects.Where(func);
        }

        protected ISerializerService GetSerializer() => new XmlSerializerService();
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
                this.GetSerializer().Save(ProjectOptions.Instance.HistoryPath, new ProjectHistroyData() { ProjectItems = this.Projects.ToList() });
                return true;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return false;
            }
        }
    }

    public class ProjectHistroyData
    {
        public List<IProjectItem> ProjectItems { get; set; }
    }
}
