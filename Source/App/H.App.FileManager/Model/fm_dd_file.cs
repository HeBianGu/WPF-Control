using H.Extensions.Behvaiors;
using H.Providers.Ioc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
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
        [Required]
        [Display(Name = "名称")]
        public string Name { get; set; }

        [Display(Name = "类型")]
        public string Type { get; set; }

        [DataGridColumn("2*")]
        [Required]
        [Display(Name = "资源路径")]
        public string Url { get; set; }

        [Display(Name = "资源标签")]
        public string Tags { get; set; }

        [Display(Name = "扩展名")]
        public string Extend{ get; set; }

        [Display(Name = "大小")]
        public long Size { get; set; }

        [Display(Name = "总播放次数")]
        public string PlayCount { get; set; }

        [Display(Name = "评分")]
        public string Score { get; set; }

        [Display(Name = "收藏")]
        public bool Favorite { get; set; }
    }


    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            //Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<fm_dd_file> fm_dd_files { get; set; }

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
