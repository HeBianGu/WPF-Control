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
