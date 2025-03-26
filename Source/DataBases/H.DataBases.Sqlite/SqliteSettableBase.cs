


#if NETFRAMEWORK
using System.Data.Entity;
#endif

#if NETCOREAPP
#endif
using H.DataBases.Share;
using H.Services.AppPath;
using H.Services.Common;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace H.DataBases.Sqlite
{
    public abstract class SqliteSettableBase<TSetting> : DbSettableBase<TSetting>, ISqliteOption where TSetting : new()
    {
        protected override string GetDefaultPath()
        {
            return this.ConfigPath;
        }

        public override void LoadDefault()
        {
            base.LoadDefault();
            this.ConfigPath = Path.Combine(IocAppPaths.Instance.Config, this.GetType().Name + IocAppPaths.Instance.ConfigExtention);
            this.FilePath = IocAppPaths.Instance.Data;
        }

        private string _configPath;
        [ReadOnly(true)]
        [Browsable(false)]
        [Display(Name = "数据库配置文件路径", Description = "表示数据库连接字符串的配置文件存放的位置")]
        public string ConfigPath
        {
            get { return _configPath; }
            set
            {
                _configPath = value;
                RaisePropertyChanged();
            }
        }

        private string _filePath;
        [Display(Name = "数据库文件夹路径", Description = "数据库保存的文件夹路径")]
        public string FilePath
        {
            get { return _filePath; }
            set
            {
                _filePath = value;
                RaisePropertyChanged();
            }
        }

        private string _initialCatalog;
        [DefaultValue("data.db")]
        [Display(Name = "数据库名称", Description = "数据库文件的名称")]
        public string InitialCatalog
        {
            get { return _initialCatalog; }
            set
            {
                _initialCatalog = value;
                RaisePropertyChanged();
            }
        }

        public override string GetConnect()
        {
            if (string.IsNullOrEmpty(this.FilePath))
                return $"Data Source={this.InitialCatalog}";
            return $"Data Source={Path.Combine(this.FilePath, this.InitialCatalog)}";
        }
    }
}
