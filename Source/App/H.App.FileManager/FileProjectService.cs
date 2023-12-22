using H.DataBases.Share;
using H.Extensions.ViewModel;
using H.Modules.Login;
using H.Modules.Project;
using H.Providers.Ioc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Windows;

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
}
