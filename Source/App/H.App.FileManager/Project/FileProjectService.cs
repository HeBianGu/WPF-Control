using H.Common.Interfaces;
using H.Controls.FavoriteBox;
using H.Controls.TagBox;
using H.Extensions.Common;
using H.Modules.Login;
using H.Modules.Project;
using H.Services.Common;
using H.Services.Project;
using Microsoft.Extensions.Options;
using System;

namespace H.App.FileManager
{
    public class FileProjectService : ProjectServiceBase<FileProjectItem>, IProjectService, ILoginedSplashLoad
    {
        private readonly IOptions<TagOptions> _tagOptions;
        private readonly IOptions<FavoriteOptions> _favoriteOptions;
        public FileProjectService(IOptions<ProjectOptions> options, IOptions<TagOptions> tagOptions, IOptions<FavoriteOptions> favoriteOptions) : base(options)
        {
            _tagOptions = tagOptions;
            _favoriteOptions = favoriteOptions;
        }

        public override FileProjectItem Create()
        {
            return new FileProjectItem()
            {
                Title = DateTime.Now.ToString("yyyyMMddHHmmss"),
                Path = this.GetFolderPath(),
                Tags = _tagOptions.Value.Tags.ToObservable(),
                FavoriteItems = _favoriteOptions.Value.FavoriteItems.ToObservable()
            };
        }
    }
}
