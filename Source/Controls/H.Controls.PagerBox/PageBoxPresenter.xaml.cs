// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Extensions.Mvvm.ViewModels.Base;
using H.Mvvm.Commands;
using System.ComponentModel;

namespace H.Controls.PagerBox;

public class PageBoxPresenter : DisplayBindableBase
{
    private readonly Action _pageUpdated;
    public PageBoxPresenter(Action pageUpdated)
    {
        this._pageUpdated = pageUpdated;
    }
    private int _page = 1;
    [DefaultValue(1)]
    public int Page
    {
        get { return _page; }
        set
        {
            _page = value;
            RaisePropertyChanged();
        }
    }

    private int _limit = 10;
    [DefaultValue(10)]
    public int Limit
    {
        get { return _limit; }
        set
        {
            _limit = value;
            RaisePropertyChanged();
        }
    }

    private int _totalPage;
    public int TotalPage
    {
        get { return _totalPage; }
        set
        {
            _totalPage = value;
            RaisePropertyChanged();
        }
    }

    private int _total;
    public int Total
    {
        get { return _total; }
        set
        {
            _total = value;
            RaisePropertyChanged();
        }
    }

    public RelayCommand SearchCommand => new RelayCommand(x =>
    {
        this._pageUpdated?.Invoke();
    });
}
