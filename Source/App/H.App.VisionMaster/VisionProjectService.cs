using H.Modules.Project;
using H.Services.Common;
using Microsoft.Extensions.Options;

namespace H.App.VisionMaster;

public class VisionProjectService : ProjectServiceBase<VisionProjectItem>, ILoginedSplashLoad
{
    public VisionProjectService(IOptions<ProjectOptions> options) : base(options)
    {

    }

    public override VisionProjectItem Create()
    {
        var n = new VisionProjectItem()
        {
            Path = AppPaths.Instance.Project
        };
        n.InitData();
        return n;
    }
}
