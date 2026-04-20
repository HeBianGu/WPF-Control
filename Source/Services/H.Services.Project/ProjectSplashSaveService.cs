// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Services.Project;

public class ProjectSplashSaveService : IProjectSplashSaveService
{
    public string Name => "保存项目";
    public bool Save(out string message)
    {
        message = null;
        return IocProject.Instance.Current?.Save(out message) != false;
    }
}