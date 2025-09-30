// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

global using H.Common.Interfaces;

namespace H.Extensions.Mvvm.ViewModels;

public partial class SelectBindable<T> : ModelBindable<T>, ISelectable
{
    public SelectBindable(T t) : base(t)
    {

    }

    private bool _isSelected;
    [Browsable(false)]
    public bool IsSelected
    {
        get { return _isSelected; }
        set
        {
            _isSelected = value;
            RaisePropertyChanged();
        }
    }

}
