// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Extensions.Mvvm.ViewModels.Base;
using H.Mvvm.Commands;
using H.Services.Message.IODialog;
using System.Collections.ObjectModel;

namespace H.Presenters.Common;

public class AddImageListPresenter : DisplayBindableBase
{
    private ObservableCollection<string> _paths = new ObservableCollection<string>();
    public ObservableCollection<string> Paths
    {
        get { return _paths; }
        set
        {
            _paths = value;
            RaisePropertyChanged();
        }
    }

    public RelayCommand AddFileCommand => new RelayCommand(x =>
    {
        var path = IocMessage.IOFileDialog.ShowOpenImageFile();
        if (path == null)
            return;
        this.Paths.Add(path);
    });

    public RelayCommand RemoveFileCommand => new RelayCommand(x =>
    {
        if (x is string t)
            this.Paths.Remove(t);
    });


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

}
