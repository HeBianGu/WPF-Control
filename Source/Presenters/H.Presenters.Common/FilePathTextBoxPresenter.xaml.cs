// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

global using H.Services.Message.IODialog;

namespace H.Presenters.Common;

public interface IFilePathTextBoxPresenter : IOpenFilePathable
{

}

[Icon("\xE70F")]
[Display(Name = "打开文件")]
public class FilePathTextBoxPresenter : DisplayBindableBase, IFilePathTextBoxPresenter
{
    private string _filePath;
    public string FilePath
    {
        get { return _filePath; }
        set
        {
            _filePath = value;
            RaisePropertyChanged();
        }
    }
    public string Filter { get; set; } = IIOFileDialogOption.defaultFilter;
}

public static partial class DialogServiceExtension
{
    public static async Task<bool?> ShowFilePathTextBox(this IDialogMessageService service, Action<IFilePathTextBoxPresenter> option, Action<IFilePathTextBoxPresenter> sumitAction, Action<IDialog> builder = null, Func<IFilePathTextBoxPresenter, Task<bool>> canSumit = null)
    {
        return await service.ShowDialog<FilePathTextBoxPresenter>(option, sumitAction, x =>
        {
            x.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Stretch;
            x.MinWidth = 200;
            builder?.Invoke(x);
        }, canSumit);
    }
}
