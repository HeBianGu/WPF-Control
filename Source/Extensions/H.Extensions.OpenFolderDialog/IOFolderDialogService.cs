// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Services.Message.IODialog;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace H.Extensions.OpenFolderDialog;

public class IOFolderDialogService : IIOFolderDialogService
{
    public string ShowOpenFolder(string title = "打开文件夹")
    {
        CommonOpenFileDialog folderDialog = new CommonOpenFileDialog();
        folderDialog.Title = title;
        folderDialog.IsFolderPicker = true;
        if (folderDialog.ShowDialog() != CommonFileDialogResult.Ok)
            return null;
        return folderDialog.FileName;
    }
}
