// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Extensions.Mvvm.ViewModels;

public class DisplayBindable<T> : DisplayBindableBase
{
    public DisplayBindable(T t)
    {
        this.Model = t;

    }

    private T _model;
    [Browsable(false)]
    public T Model
    {
        get { return _model; }
        set
        {
            _model = value;
            RaisePropertyChanged("Model");
        }
    }
}
