// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Services.AppPath;

public interface IAppPathServce
{
    string AppName { get; }
    string AppPath { get; }
    string Cache { get; }
    string Company { get; set; }
    string Config { get; }
    string Data { get; }
    string Default { get; }
    string Document { get; set; }
    string License { get; }
    string Log { get; }
    string Project { get; }
    string RegistryPath { get; }
    string Setting { get; }
    string Template { get; }
    string UserCache { get; }
    string UserData { get; }
    string UserLicense { get; }
    string UserLog { get; }
    string UserPath { get; }
    string UserProject { get; }
    string UserSetting { get; }
    string UserTemplate { get; }
    string Version { get; }
}
