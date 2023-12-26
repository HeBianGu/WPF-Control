using H.Modules.Login;
using H.Modules.Project;
using H.Providers.Ioc;
using Microsoft.Extensions.Options;
using System;

namespace H.App.FileManager
{
    public class FileProjectService : ProjectServiceBase<FileProjectItem>, IProjectService
    {
        public FileProjectService(IOptions<ProjectOptions> options) : base(options)
        {

        }

        public override IProjectItem Create()
        {
            return new FileProjectItem()
            {
                Title = DateTime.Now.ToString("yyyyMMddHHmmss"),
                Path = SystemPathSetting.Instance.Project
            };
        }
    }

    public class AppSaveService : IAppSaveService
    {
        public string Name => "应用程序保存";

        public bool Save(out string message)
        {
            return IocProject.Instance.Current.Save(out message);
        }
    }
}
