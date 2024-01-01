



using H.Controls.TagBox;
using H.DataBases.Share;
using H.Extensions.Setting;
using H.Extensions.ViewModel;
using H.Modules.Project;
using H.Providers.Ioc;
using H.Providers.Ioc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace H.App.FileManager
{
    public class FileProjectItem : ProjectItemBase
    {
        private string _baseFolder;
        [Required]
        [Display(Name = "文件路径", Order = 5)]
        public string BaseFolder
        {
            get { return _baseFolder; }
            set
            {
                _baseFolder = value;
                RaisePropertyChanged();
            }
        }

        [Browsable(false)]
        public ObservableCollection<Tag> Tags { get; set; } = new ObservableCollection<Tag>();

        private IRepositoryViewModel<fm_dd_file> _file = new RepositoryViewModel<fm_dd_file>();
        [JsonIgnore]
        [XmlIgnore]
        [Browsable(false)]
        public IRepositoryViewModel<fm_dd_file> File
        {
            get { return _file; }
            set
            {
                _file = value;
                RaisePropertyChanged();
            }
        }

        public int Order { get; }

        public string GroupName { get; } = "工程设置";

        public override bool Load(out string message)
        {
            //  Do ：重新注册仓储和上下文接口
            DbIoc.ConfigureServices(dx =>
            {
                dx.AddDbContext<DataContext>(x =>
                {
                    IProjectService project = Ioc.GetService<IProjectService>();
                    string con = project.Current == null ? $"Data Source=default.db" : $"Data Source={System.IO.Path.Combine(project.Current.Path, project.Current.Title)}.db";
                    x.UseLazyLoadingProxies().UseSqlite(con);
                });
                dx.AddSingleton<IStringRepository<fm_dd_file>, DbContextRepository<DataContext, fm_dd_file>>();
                dx.AddSingleton<IRepositoryViewModel<fm_dd_file>, FileRepositoryViewModel>();
            });
            //  Do ：迁移数据
            DataContext find = DbIoc.Services.GetService<DataContext>();
            find.Database.Migrate();
            //  Do ：刷新数据
            File = DbIoc.GetService<IRepositoryViewModel<fm_dd_file>>();
            File.RefreshData();
            IocTagService.Instance.Load(out message);
            SettingDataManager.Instance.Add(this.Setting);
            return base.Load(out message);
        }

        public override bool Save(out string message)
        {
            File?.Save();
            DataContext context = DbIoc.Services.GetService<DataContext>();
            context?.SaveChanges();
            //context?.Dispose();
            return base.Save(out message);
        }

        public override bool Close(out string messge)
        {
            this.Save(out messge);
            DataContext context = DbIoc.Services.GetService<DataContext>();
            context?.Dispose();
            SettingDataManager.Instance.Remove(this.Setting);
            return true;
        } 

        private FileProjectItemSetting _setting = new FileProjectItemSetting();
        [Browsable(false)]
        public FileProjectItemSetting Setting
        {
            get { return _setting; }
            set
            {
                _setting = value;
                RaisePropertyChanged();
            }
        }
    }
}
