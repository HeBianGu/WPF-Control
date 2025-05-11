// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.DataBases.Sqlite;
using H.Modules.Identity;
using H.Modules.Login;

namespace H.ApplicationBases.Identify;
public interface IDefaultIndentifyOptions
{
    void UseIdentifyOptions(Action<IIdentifyOptions> action);
    void UseLoginOptions(Action<ILoginOptions> action);
    void UseRegistorOptions(Action<IRegistorOptions> action);
    void UseSqliteSettable(Action<ISqliteSettable> action);
}