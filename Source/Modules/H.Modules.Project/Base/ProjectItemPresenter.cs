// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Modules.Project.Base;

public abstract class ProjectItemBindableBase<T> : BindableBase where T : IProjectItem
{
    public ProjectItemBindableBase(T projectItem)
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
}

public abstract class ProjectItemPresenter<T> : ProjectItemBindableBase<T>, IProjectItemPresenter where T : IProjectItem
{
    public ProjectItemPresenter(T projectItem) : base(projectItem)
    {
    }
    IProjectItem IProjectItemPresenter.ProjectItem => this.ProjectItem;
}

public abstract class ProjectItemThumbnailPresenter<T> : ProjectItemBindableBase<T>, IProjectItemThumbnailPresenter where T : IProjectItem
{
    public ProjectItemThumbnailPresenter(T projectItem) : base(projectItem)
    {
    }
    IProjectItem IProjectItemThumbnailPresenter.ProjectItem => this.ProjectItem;
}