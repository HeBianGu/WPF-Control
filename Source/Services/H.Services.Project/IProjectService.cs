// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

global using H.Common.Interfaces;

namespace H.Services.Project;

public interface IProjectService : ISplashSave, ISplashLoadable
{
    IProjectItem Current { get; set; }
    IProjectItem Create();
    void Add(IProjectItem project);
    void Delete(Func<IProjectItem, bool> func);
    IEnumerable<IProjectItem> Where(Func<IProjectItem, bool> func = null);
    Action<IProjectItem, IProjectItem> CurrentChanged { get; set; }
}