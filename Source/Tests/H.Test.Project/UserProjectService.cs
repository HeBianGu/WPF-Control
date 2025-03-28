using H.Common.Interfaces;
using H.Modules.Login;
using H.Modules.Project;
using H.Modules.Project.Base;
using H.Services.Common;
using H.Services.Identity;
using Microsoft.Extensions.Options;
using System;

namespace H.Test.Project
{
    public class UserProjectService : ProjectServiceBase<UserProjectItem>, ILoginedSplashLoad
    {
        public UserProjectService(IOptions<ProjectOptions> options) : base(options)
        {

        }

        public override UserProjectItem Create()
        {
            return new UserProjectItem();
        }
    }

    public class UserProjectItem : ProjectItemBase
    {
        private string _value = DateTime.Now.ToString();
        public string Value
        {
            get { return _value; }
            set
            {
                _value = value;
                RaisePropertyChanged();
            }
        }

        protected override object GetSaveFileData()
        {
            return this.Value;
        }

        public override bool Load(out string message)
        {
            message = null;
            if (this.LoadFile<string>(out string value))
                this.Value = value;
            return true;
        }
    }
}
