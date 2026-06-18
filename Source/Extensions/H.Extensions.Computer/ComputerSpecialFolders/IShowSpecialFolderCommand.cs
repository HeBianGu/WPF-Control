// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Extensions.Computer.Base;
using static System.Environment;

namespace H.Extensions.Computer.ComputerSpecialFolders;

public interface IShowSpecialFolderCommand : IStartComputerProcessCommand
{
    SpecialFolder SpecialFolder { get; set; }
}
