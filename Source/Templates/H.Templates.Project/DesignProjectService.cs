using H.Modules.Project;
using Microsoft.Extensions.Options;

namespace H.Templates.Project;
public class DesignProjectService : ProjectServiceBase<DesignProjectItem>
{
    private readonly IOptions<ProjectOptions> _options;
    public DesignProjectService(IOptions<ProjectOptions> options) : base(options)
    {
        _options = options;
    }
    public override DesignProjectItem Create()
    {
        return new DesignProjectItem();
    }
}
