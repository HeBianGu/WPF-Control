// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Extensions.Mvvm.ViewModels.Base;

public abstract class GroupDisplayBindableBase<T> : DisplayBindableBase
{
    public GroupDisplayBindableBase()
    {
        this.NodeDatas = new ObservableCollection<T>(this.CreateNodeDatas());
    }
    private ObservableCollection<T> _nodeDatas = new ObservableCollection<T>();
    public ObservableCollection<T> NodeDatas
    {
        get { return _nodeDatas; }
        set
        {
            _nodeDatas = value;
            RaisePropertyChanged();
        }
    }

    protected abstract IEnumerable<T> CreateNodeDatas();
}
