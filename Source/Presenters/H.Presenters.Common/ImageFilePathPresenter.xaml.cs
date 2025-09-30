// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

global using H.Extensions.Mvvm.Commands;
global using H.Services.Message;
global using System.ComponentModel.DataAnnotations;
using H.Mvvm.Commands;

namespace H.Presenters.Common;

public interface IImageFilePathPresenter
{
    string FilePath { get; set; }
}

[Icon("\xEB9F")]
[Display(Name = "打开图片")]
public class ImageFilePathPresenter : DisplayBindableBase, IImageFilePathPresenter
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

    public RelayCommand OpenCommand => new RelayCommand(x =>
    {
        IocMessage.IOFileDialog.ShowOpenImageFile(l =>
        {
            this.FilePath = l;
        });
    });
}

public static partial class DialogServiceExtension
{
    public static async Task<bool?> ShowImageSource(this IDialogMessageService service, Action<IImageFilePathPresenter> option, Action<IImageFilePathPresenter> sumitAction, Action<IDialog> builder = null, Func<IImageFilePathPresenter, Task<bool>> canSumit = null)
    {
        return await service.ShowDialog<ImageFilePathPresenter>(option, sumitAction, x =>
        {
            x.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Stretch;
            x.MinWidth = 200;
            builder?.Invoke(x);
        }, canSumit);
    }
    public static async Task<bool?> ShowImageSource(this IDialogMessageService service, string filePath, Action<string> sumitAction, Action<IDialog> builder = null, Func<IImageFilePathPresenter, Task<bool>> canSumit = null)
    {
        return await service.ShowImageSource(x => x.FilePath = filePath, x => sumitAction?.Invoke(x.FilePath), builder, canSumit);
    }
}
