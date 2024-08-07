﻿using H.Modules.Login;
using H.Modules.Project;
using H.Services.Common;
using Microsoft.Extensions.Options;

namespace H.Test.Project
{
    public class UserProjectService : ProjectService, ILoginedSplashLoad
    {
        public UserProjectService(IOptions<ProjectOptions> options) : base(options)
        {

        }
    }
}
