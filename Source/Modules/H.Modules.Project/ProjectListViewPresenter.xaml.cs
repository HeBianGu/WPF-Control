// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

global using H.Extensions.Mvvm.ViewModels.Base;
using H.Extensions.FontIcon;

namespace H.Modules.Project;

public interface IProjectListViewPresenter
{
    bool UseAddProject { get; set; }
}

[Icon(FontIcons.List)]
public class ProjectListViewPresenter : DisplayBindableBase, IProjectListViewPresenter
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

    private bool _UseAddProject;
    public bool UseAddProject
    {
        get { return _UseAddProject; }
        set
        {
            _UseAddProject = value;
            RaisePropertyChanged();
        }
    }


    private string _searchText;

    public string SearchText
    {
        get { return _searchText; }
        set
        {
            _searchText = value;
            RaisePropertyChanged();
        }
    }

}
