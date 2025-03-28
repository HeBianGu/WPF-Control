using H.Controls.FavoriteBox;
using H.Controls.TagBox;
using H.DataBases.Share;
using H.Modules.Project;
using H.Services.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text.Json.Serialization;
using System.Xml.Serialization;
using H.Extensions.DataBase;
using H.Modules.Project.Base;
using H.Extensions.DataBase.Repository;
using H.Services.Setting;

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

        public override string GetFilePath()
        {
            return System.IO.Path.Combine(this.Path, this.Title + ".db");
        }

        [Browsable(false)]
        public ObservableCollection<Tag> Tags { get; set; } = new ObservableCollection<Tag>();

        [Browsable(false)]
        public ObservableCollection<FavoriteItem> FavoriteItems { get; set; } = new ObservableCollection<FavoriteItem>();


        private IRepositoryBindable<fm_dd_file> _file = new RepositoryBindable<fm_dd_file>();
        [JsonIgnore]
        [XmlIgnore]
        [Browsable(false)]
        public IRepositoryBindable<fm_dd_file> File
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
                    string con = project.Current == null ? $"Data Source=default.db" : $"Data Source={this.GetFilePath()}";
                    x.UseLazyLoadingProxies().UseSqlite(con);
                });
                dx.AddSingleton<IStringRepository<fm_dd_file>, DbContextRepository<DataContext, fm_dd_file>>();
                dx.AddSingleton<IRepositoryBindable<fm_dd_file>, FileRepositoryBindable>();
            });

            if (!Directory.Exists(System.IO.Path.GetDirectoryName(this.GetFilePath())))
                Directory.CreateDirectory(System.IO.Path.GetDirectoryName(this.GetFilePath()));
            //  Do ：迁移数据
            DataContext find = DbIoc.Services.GetService<DataContext>();
            find.Database.Migrate();
            //  Do ：刷新数据
            this.File = DbIoc.GetService<IRepositoryBindable<fm_dd_file>>();
            this.File.RefreshData();
            IocTagService.Instance.Load(out message);
            IocFavoriteService.Instance.Load(out message);
            IocSetting.Instance.Add(this.Setting);
            return base.Load(out message);
        }

        public override bool Save(out string message)
        {
            this.File?.Save();
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
            IocSetting.Instance.Remove(this.Setting);
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

        public override bool Delete(out string message)
        {
            //  Do ：这块需要释放数据库
            //var r = this.Close(out message);
            //if (r == false)
            //    return false;
            //if (System.IO.File.Exists(this.Path))
            //    File.Delete(this.Path);
            //if (!string.IsNullOrEmpty(this.Path))
            //{
            //    var find = this.GetFilePath();
            //    if (System.IO.File.Exists(find))
            //        File.Delete(find);
            //}
            message = null;
            return true;
        }
    }
}
