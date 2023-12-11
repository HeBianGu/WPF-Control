// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase



using H.Modules.Login;
using H.Providers.Ioc;
using Microsoft.Extensions.Options;

namespace H.Modules.Project
{
    public class ProjectService : ProjectServiceBase, IProjectService
    {
        IOptions<ProjectOptions> _options;
        public ProjectService(IOptions<ProjectOptions> options) : base(options)
        {
            _options = options;
        }

        public IProjectItem Create()
        {
            return new ProjectItem()
            {
                Path = SystemPathSetting.Instance.Project
            };
        }
    }
}
