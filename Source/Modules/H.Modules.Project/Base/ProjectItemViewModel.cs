// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using H.Mvvm.ViewModels;

namespace H.Modules.Project.Base;

public class ProjectItemViewModel : SelectBindable<IProjectItem>
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
