// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase



using H.Modules.Login;
using H.Providers.Ioc;
using H.Providers.Mvvm;
using Microsoft.Extensions.Options;

namespace H.Modules.Project
{
    public class ProjectService : ProjectServiceBase<ProjectItem>, IProjectService
    {
        IOptions<ProjectOptions> _options;
        public ProjectService(IOptions<ProjectOptions> options) : base(options)
        {
            _options = options;
        }

        public override ProjectItem Create()
        {
            return new ProjectItem()
            {
                Path = AppPaths.Instance.Project
            };
        }
    }
}
