using H.Controls.TagBox;
using H.Modules.Login;
using H.Modules.Project;
using H.Providers.Ioc;
using Microsoft.Extensions.Options;
using System;

namespace H.App.FileManager
{
    public class FileProjectService : ProjectServiceBase<FileProjectItem>, IProjectService
    {
        private readonly IOptions<TagOptions> _tagOptions;
        public FileProjectService(IOptions<ProjectOptions> options, IOptions<TagOptions> tagOptions) : base(options)
        {
            _tagOptions = tagOptions;
        }

        public override IProjectItem Create()
        {
            return new FileProjectItem()
            {
                Title = DateTime.Now.ToString("yyyyMMddHHmmss"),
                Path = AppPaths.Instance.Project,
                Tags = _tagOptions.Value.Tags.ToObservable()
            };
        }
    }
}
