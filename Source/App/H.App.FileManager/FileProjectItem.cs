using H.DataBases.Share;
using H.Extensions.ViewModel;
using H.Modules.Project;
using H.Providers.Ioc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace H.App.FileManager
{
    public class FileProjectItem : ProjectItemBase
    {
        [Required]
        [Display(Name = "文件路径", Order = 2)]
        public string BaseFolder { get; set; }

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

        public override bool Load(out string message)
        {
            //  Do ：重新注册仓储和上下文接口
            DbIoc.ConfigureServices(dx =>
            {
                dx.AddDbContext<DataContext>(x =>
                {
                    var project = Ioc.GetService<IProjectService>();
                    string con = project.Current == null ? $"Data Source=default.db" : $"Data Source={System.IO.Path.Combine(project.Current.Path, project.Current.Title)}.db";
                    x.UseLazyLoadingProxies().UseSqlite(con);
                });
                dx.AddSingleton<IStringRepository<fm_dd_file>, DbContextRepository<DataContext, fm_dd_file>>();
                dx.AddSingleton<IRepositoryViewModel<fm_dd_file>, FileRepositoryViewModel>();
            });
            //  Do ：迁移数据
            var find = DbIoc.Services.GetService<DataContext>();
            find.Database.Migrate();
            //  Do ：刷新数据
            this.File = DbIoc.GetService<IRepositoryViewModel<fm_dd_file>>();
            this.File.RefreshData();
            return base.Load(out message);
        }

        public override bool Save(out string message)
        {
            this.File?.Save();
            var context = DbIoc.Services.GetService<DataContext>();
            context?.SaveChanges();
            context?.Dispose();
            return base.Save(out message);
        }
    }
}
