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

public interface IAddImageFilePathPresenter
{
    string FilePath { get; set; }
}

[Icon("\xEB9F")]
[Display(Name = "打开图片")]
public class AddImageFilePathPresenter : DesignPresenterBase, IAddImageFilePathPresenter
{
    private bool _UseAdd = true;
    public bool UseAdd
    {
        get { return _UseAdd; }
        set
        {
            _UseAdd = value;
            RaisePropertyChanged();
        }
    }


    private bool _UseRemove = true;
    public bool UseRemove
    {
        get { return _UseRemove; }
        set
        {
            _UseRemove = value;
            RaisePropertyChanged();
        }
    }

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
    public RelayCommand RemoveFileCommand => new RelayCommand(x =>
    {
        this.FilePath = null;
    });


}

