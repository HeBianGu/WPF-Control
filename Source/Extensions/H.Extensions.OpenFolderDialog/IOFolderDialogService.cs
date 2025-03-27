using H.Services.Message.IODialog;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Markup;

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
