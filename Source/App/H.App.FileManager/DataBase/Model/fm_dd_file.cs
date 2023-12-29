using H.Extensions.Behvaiors;
using H.Providers.Ioc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

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
        public int Score { get; set; } = 1;

        [DataGridColumn("Auto")]
        [Display(Name = "收藏")]
        public bool Favorite { get; set; }
    
        private string _favoritePath;
        [DataGridColumn("Auto")]
        [Display(Name = "收藏夹")]
        public string FavoritePath
        {
            get { return _favoritePath; }
            set
            {
                _favoritePath = value;
                RaisePropertyChanged();
            }
        }


        [Display(Name = "最近打开")]
        public DateTime LastPlayTime { get; set; }

        [Display(Name = "已观看")]
        public bool Watched { get; set; }

        //[Browsable(false)]
        //[Display(Name = "收藏")]
        //public string Project { get; set; }
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
}
