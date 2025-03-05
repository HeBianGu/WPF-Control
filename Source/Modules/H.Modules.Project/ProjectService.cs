// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

global using Microsoft.Extensions.Options;

namespace H.Modules.Project;

public class ProjectService : ProjectServiceBase<ProjectItem>, IProjectService
{
    public ProjectService(IOptions<ProjectOptions> options) : base(options)
    {

    }

    public override ProjectItem Create()
    {
        return new ProjectItem();
    }
}
