// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

global using H.Mvvm.ViewModels.Base;
using H.Extensions.FontIcon;

namespace H.Modules.Project;

[Icon(FontIcons.List)]
public class ProjectListViewPresenter : DisplayBindableBase
{
    private IProjectItem _selectedItem;
    public IProjectItem SelectedItem
    {
        get { return _selectedItem; }
        set
        {
            _selectedItem = value;
            RaisePropertyChanged();
        }
    }
}
