using System.ComponentModel.DataAnnotations;
using System.IO;

namespace H.Services.Common
{
    [Display(Name = "系统路径", GroupName = SettingGroupNames.GroupSystem)]
    public class AppPaths : LazyInstance<AppPaths>, IAppPaths
    {
        public string Company { get; set; } = "HeBianGu";
        public string ConfigExtention { get; set; } = ".xml";
        public AppPaths()
        {
            this.CheckFolder(this.AppPath);
            this.CheckFolder(this.Default);
            this.CheckFolder(this.Config);
            this.CheckFolder(this.Data);
            this.CheckFolder(this.Setting);
            this.CheckFolder(this.License);
            this.CheckFolder(this.Log);
            this.CheckFolder(this.Project);
            this.CheckFolder(this.Template);
            this.CheckFolder(this.Setting);
            this.CheckFolder(this.Cache);
        }
        #region - 基础目录 -
        public string Document { get; set; } = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public string AppName => Assembly.GetEntryAssembly()?.GetName()?.Name;
        public string AppPath => Path.Combine(this.Document, this.Company, this.AppName);
        public string Default => Path.Combine(this.Document, this.Company, this.AppName, nameof(this.Default));
        public string Config => Path.Combine(this.Default, nameof(this.Config));
        public string Data => Path.Combine(this.Default, nameof(this.Data));
        public string RegistryPath => Path.Combine("SOFTWARE", this.AppName);
        public string Setting => Path.Combine(this.Default, nameof(this.Setting));
        public string License => Path.Combine(this.Default, nameof(this.License));
        public string Log => Path.Combine(this.Default, nameof(this.Log));
        public string Project => Path.Combine(this.Default, nameof(this.Project));
        public string Template => Path.Combine(this.Default, nameof(this.Template));
        public string Cache => Path.Combine(this.Default, nameof(this.Cache));
        #endregion

        #region - 登录用户目录 -

        public string UserPath => Path.Combine(this.AppPath, this.GetUserName() ?? nameof(this.Default));

        private string GetUserName()
        {
            return Ioc<ILoginService>.Instance?.User?.Account;
        }

        public string UserData => Path.Combine(this.UserPath, nameof(this.Data));
        public string UserSetting => Path.Combine(this.UserPath, nameof(this.Setting));
        public string UserProject => Path.Combine(this.UserPath, nameof(this.Project));
        public string UserTemplate => Path.Combine(this.UserPath, nameof(this.Template));
        public string UserCache => Path.Combine(this.UserPath, nameof(this.Cache));
        public string UserLicense => Path.Combine(this.Default, nameof(this.License));
        public string UserLog => Path.Combine(this.Default, nameof(this.Log));
        #endregion

        #region - 程序根目录 -

        public string Module => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Module");

        public string Component => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Component");

        public string Version => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Version");

        #endregion

        public void ClearCache()
        {
            try
            {
                Directory.Delete(this.Cache, true);
                IocMessage.Snack.ShowInfo("操作完成");
            }
            catch (Exception ex)
            {
                IocMessage.Dialog.Show("清空失败,详情请查看日志");
                IocLog.Instance?.Error(ex);
            }
        }

        public bool ClearSetting()
        {
            try
            {
                if (Directory.Exists(this.Setting))
                    Directory.Delete(this.Setting, true);
                if (Directory.Exists(this.UserSetting))
                    Directory.Delete(this.UserSetting, true);
                IocMessage.Snack?.ShowInfo("操作完成");
                return true;
            }
            catch (Exception ex)
            {
                IocMessage.Dialog.Show("清空失败,详情请查看日志");
                IocLog.Instance?.Error(ex);
                return false;
            }
        }

        public void CheckFolder(string folder)
        {
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);
        }
    }
}
