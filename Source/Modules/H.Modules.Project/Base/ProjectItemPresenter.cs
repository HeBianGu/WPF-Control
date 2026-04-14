// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Modules.Project.Base;

public class ProjectItemPresenter<T> : BindableBase, IProjectItemPresenter
{
    public ProjectItemPresenter(T projectItem)
    {
        this.ProjectItem = projectItem;
    }
    private T _ProjectItem;
    public T ProjectItem
    {
        get { return _ProjectItem; }
        set
        {
            _ProjectItem = value;
            RaisePropertyChanged();
        }
    }

    IProjectItem IProjectItemPresenter.ProjectItem => throw new NotImplementedException();
}
