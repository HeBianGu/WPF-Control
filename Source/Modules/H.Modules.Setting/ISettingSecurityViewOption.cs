// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Modules.Setting;

public interface ISettingSecurityViewOption
{
    string Password { get; set; }
    int PasswordCount { get; set; }
    bool RememberPassword { get; set; }
    bool UsePassword { get; set; }
}
