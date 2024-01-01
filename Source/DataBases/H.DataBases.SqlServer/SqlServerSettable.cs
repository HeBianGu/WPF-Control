#if NETFRAMEWORK
using System.Data.Entity;
#endif

#if NETCOREAPP
#endif
using H.DataBases.Share;
using H.Providers.Ioc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace H.DataBases.SqlServer
{
    public abstract class SqlServerSettable<TSetting> : DbSettableBase<TSetting>, ISqlServerOption where TSetting : new()
    {
        protected override string GetDefaultPath()
        {
            return this.ConfigPath;
        }

        public override void LoadDefault()
        {
            base.LoadDefault();
            this.ConfigPath = Path.Combine(AppPaths.Instance.Config, this.GetType().Name + AppPaths.Instance.ConfigExtention);
        }

        private string _configPath;
        [ReadOnly(true)]
        [Browsable(false)]
        [Display(Name = "文件路径")]
        public string ConfigPath
        {
            get { return _configPath; }
            set
            {
                _configPath = value;
                RaisePropertyChanged();
            }
        }

        private string _server;
        [Display(Name = "服务器名称")]
        public string Server
        {
            get { return _server; }
            set
            {
                _server = value;
                RaisePropertyChanged();
            }
        }

        private string _initialCatalog;
        [Display(Name = "数据库")]
        public string InitialCatalog
        {
            get { return _initialCatalog; }
            set
            {
                _initialCatalog = value;
                RaisePropertyChanged();
            }
        }


        private string _userName;
        [DefaultValue("sa")]
        [Display(Name = "用户名")]
        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                RaisePropertyChanged();
            }
        }


        private string _password;
        [DefaultValue(".")]
        [Display(Name = "密码")]
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                RaisePropertyChanged();
            }
        }

        public override string GetConnect()
        {
            string connect = $"Server={this.Server}; Trusted_Connection=False; uid={this.UserName}; pwd={this.Password};Initial Catalog={this.InitialCatalog}; MultipleActiveResultSets=true;";
            return connect;
        }
    }


    [Display(Name = "数据库配置", GroupName = SettingGroupNames.GroupApp)]
    public class SqlServerSettable : SqlServerSettable<SqlServerSettable>, ISqlServerOption
    {

    }
}
