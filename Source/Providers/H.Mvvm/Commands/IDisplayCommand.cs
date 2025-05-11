﻿// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Mvvm.Commands;

public interface IDisplayCommand
{
    string Name { get; set; }
    string Icon { get; set; }
    string Description { get; set; }
    string GroupName { get; set; }
    int Order { get; set; }
}
