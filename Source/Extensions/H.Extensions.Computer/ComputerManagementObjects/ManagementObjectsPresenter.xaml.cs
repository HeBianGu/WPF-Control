// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Extensions.Mvvm.ViewModels.Base;
using System.Collections.ObjectModel;
using System.Reflection;

namespace H.Extensions.Computer.ComputerManagementObjects;

public class ManagementObjectsPresenter : DisplayBindableBase
{
    protected ManagementObjectsPresenter()
    {
        this.ManagementObjectPresenters = new ObservableCollection<IManagementObjectPresenter>(this.GetManagementObjectPresenters());
    }
    private ObservableCollection<IManagementObjectPresenter> _ManagementObjectPresenters = new ObservableCollection<IManagementObjectPresenter>();
    public ObservableCollection<IManagementObjectPresenter> ManagementObjectPresenters
    {
        get { return _ManagementObjectPresenters; }
        set
        {
            _ManagementObjectPresenters = value;
            RaisePropertyChanged();
        }
    }

    protected virtual IEnumerable<IManagementObjectPresenter> GetManagementObjectPresenters()
    {
        return this.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(p => typeof(IManagementObjectPresenter).IsAssignableFrom(p.PropertyType))
            .Select(p => p.GetValue(this) as IManagementObjectPresenter)
            .Where(p => p != null);
    }
}
