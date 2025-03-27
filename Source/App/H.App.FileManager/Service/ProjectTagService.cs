using H.Controls.TagBox;
using H.Services.Common.Schedule;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

namespace H.App.FileManager
{
    public class ProjectTagService : TagService
    {
        private readonly IProjectService _project;
        public ProjectTagService(IOptions<TagOptions> options, IProjectService project) : base(options)
        {
            _project = project;
        }

        public override IList<Tag> Collection
        {
            get
            {
                if (_project.Current is FileProjectItem item)
                    return item.Tags;
                return base.Collection;
            }
        }
    }

    public class ProjectSaveScheduledTaskService : ScheduledTaskServiceBase
    {
        public override bool Invoke(out string message)
        {
            return IocProject.Instance.Save(out message);
        }
    }
}
