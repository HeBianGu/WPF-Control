// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Services.Message.IODialog;

public class IOFileDialogOption : IIOFileDialogOption
{
    public string Filter { get; set; } = IIOFileDialogOption.defaultFilter;
    public string Title { get; set; } = IIOFileDialogOption.defaultTitle;
    public string InitialDirectory { get; set; }
    public bool RestoreDirectory { get; set; } = true;
}

