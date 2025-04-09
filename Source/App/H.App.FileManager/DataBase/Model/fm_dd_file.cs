global using H.Extensions.Behvaiors;
global using H.Extensions.TypeConverter;
global using H.Extensions.ValueConverter;
global using H.Services.Common;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Design;
global using System;
global using System.ComponentModel;
global using System.ComponentModel.DataAnnotations;
global using H.Extensions.DataBase;
global using H.Extensions.Behvaiors.DataGrids;
global using H.Extensions.ValueConverter.Files;

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

        private long _size;
        [DataGridColumn("Auto", ConvertyType = typeof(GetByteToSizeDisplayConverter))]
        [ReadOnly(true)]
        [Display(Name = "大小")]
        [TypeConverter(typeof(SizeToDisplayTypeConverter))]
        public long Size
        {
            get { return _size; }
            set
            {
                _size = value;
                RaisePropertyChanged();
            }
        }


        private int _playCount;
        [DataGridColumn("Auto")]
        [ReadOnly(true)]
        [Display(Name = "次数")]
        public int PlayCount
        {
            get { return _playCount; }
            set
            {
                _playCount = value;
                RaisePropertyChanged();
            }
        }


        private int _score = 1;
        [DataGridColumn("Auto")]
        [Display(Name = "评分")]
        public int Score
        {
            get { return _score; }
            set
            {
                _score = value;
                RaisePropertyChanged();
            }
        }

        private bool _favorite;
        [DataGridColumn("Auto")]
        [Display(Name = "喜欢")]
        public bool Favorite
        {
            get { return _favorite; }
            set
            {
                _favorite = value;
                RaisePropertyChanged();
            }
        }


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

        private DateTime _lastPlayTime;
        [Display(Name = "最近打开")]
        public DateTime LastPlayTime
        {
            get { return _lastPlayTime; }
            set
            {
                _lastPlayTime = value;
                RaisePropertyChanged();
            }
        }

        private bool _watched;
        [Display(Name = "已观看")]
        public bool Watched
        {
            get { return _watched; }
            set
            {
                _watched = value;
                RaisePropertyChanged();
            }
        }

        private bool _seeLater;
        [Display(Name = "稍后观看")]
        public bool SeeLater
        {
            get { return _seeLater; }
            set
            {
                _seeLater = value;
                RaisePropertyChanged();
            }
        }

    }

    public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<DataContext> optionsBuilder = new DbContextOptionsBuilder<DataContext>();
            optionsBuilder.UseLazyLoadingProxies().UseSqlite("Data Source=Migration.db");
            return new DataContext(optionsBuilder.Options);
        }
    }
}
