// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Common.Interfaces.Where;

namespace H.Services.Project;

public interface IProjectItem : ISaveable, ILoadable
{
    DateTime UpdateTime { get; set; }
    bool IsFixed { get; set; }
    string Title { get; set; }
    bool Close(out string message);
    Task<(bool success, string message)> DeleteAsync();
    IProjectItemPresenter Presenter { get; }
    IProjectItemThumbnailPresenter ThumbnailPresenter { get; }
}

/// <summary>
/// 主要视图显示
/// </summary>
public interface IProjectItemPresenter
{
    IProjectItem ProjectItem { get; }
}

/// <summary>
/// 缩率图显示
/// </summary>
public interface IProjectItemThumbnailPresenter
{
    IProjectItem ProjectItem { get; }
}