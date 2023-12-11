// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase



using H.Providers.Ioc;
using H.Providers.Mvvm;

namespace H.Modules.Project
{
    public class ProjectItemViewModel : SelectViewModel<IProjectItem>
    {
        public ProjectItemViewModel(IProjectItem project) : base(project)
        {
        }

        private string _groupName;
        public string GroupName
        {
            get { return _groupName; }
            set
            {
                _groupName = value;
                RaisePropertyChanged();
            }
        }
    }
}
