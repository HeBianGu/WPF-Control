// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Modules.Login;

public interface ILoginOptions
{
    string AdminName { get; set; }
    string AdminPassword { get; set; }
    string LastPassword { get; set; }
    string LastUserName { get; set; }
    string Product { get; set; }
    double ProductFontSize { get; set; }
    bool Remember { get; set; }
    bool UseVisitor { get; set; }
    bool Load(out string message);
    void LoadDefault();
    bool Save(out string message);
}
