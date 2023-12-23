using H.Extensions.Behvaiors;
using H.Modules.Project;
using H.Providers.Ioc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace H.App.FileManager
{
    public class fm_dd_file : DbModelBase
    {
        [DataGridColumn("Auto")]
        [ReadOnly(true)]
        [Required]
        [Display(Name = "名称")]
        public string Name { get; set; }

        [DataGridColumn("Auto")]
        [ReadOnly(true)]
        [Display(Name = "类型")]
        public string Type { get; set; }

        [ReadOnly(true)]
        [DataGridColumn("2*")]
        [Required]
        [Display(Name = "路径")]
        public string Url { get; set; }

        private string _tags;
        [Display(Name = "标签")]
        public string Tags
        {
            get { return _tags; }
            set
            {
                _tags = value;
                RaisePropertyChanged();
            }
        }

        [DataGridColumn("Auto")]
        [ReadOnly(true)]
        [Display(Name = "扩展名")]
        public string Extend { get; set; }

        [DataGridColumn("Auto")]
        [ReadOnly(true)]
        [Display(Name = "大小")]
        public long Size { get; set; }

        [DataGridColumn("Auto")]
        [ReadOnly(true)]
        [Display(Name = "次数")]
        public string PlayCount { get; set; }

        [DataGridColumn("Auto")]
        [Display(Name = "评分")]
        public string Score { get; set; }

        [DataGridColumn("Auto")]
        [Display(Name = "收藏")]
        public bool Favorite { get; set; }

        [Browsable(false)]
        [Display(Name = "收藏")]
        public string Project { get; set; }
    }

    public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
            optionsBuilder.UseLazyLoadingProxies().UseSqlite("Data Source=Migration.db");
            return new DataContext(optionsBuilder.Options);
        }
    }

    public class fm_dd_image : fm_dd_file
    {
        [DataGridColumn("Auto")]
        [ReadOnly(true)]
        [Display(Name = "宽度")]
        public int PixelWidth { get; set; }

        [DataGridColumn("Auto")]
        [ReadOnly(true)]
        [Display(Name = "高度")]
        public int PixelHeight { get; set; }
    }

    public class fm_dd_video : fm_dd_image
    {

    }

    public class fm_dd_audio : fm_dd_file
    {

    }
}
