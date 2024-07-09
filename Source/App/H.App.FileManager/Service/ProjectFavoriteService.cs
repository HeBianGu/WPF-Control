using H.Controls.FavoriteBox;
using H.Services.Common;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

namespace H.App.FileManager
{
    public class ProjectFavoriteService : FavoriteService
    {
        private readonly IProjectService _project;
        public ProjectFavoriteService(IOptions<FavoriteOptions> options, IProjectService project) : base(options)
        {
            _project = project;
        }

        public override IEnumerable<IFavoriteItem> Collection
        {
            get
            {
                if (_project.Current is FileProjectItem item)
                    return item.FavoriteItems;
                return base.Collection;
            }
        }
    }
}
