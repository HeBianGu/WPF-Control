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

public interface IManagementObjectPresenter
{
    ObservableCollection<IPropertyDataInfo> PropertyDataInfos { get; set; }
}

public abstract class ManagementObjectPresenterBase : DisplayBindableBase, IManagementObjectPresenter
{
    protected ManagementObjectPresenterBase()
    {
        this.PropertyDataInfos = new ObservableCollection<IPropertyDataInfo>(this.GetPropertyDataInfos());
    }
    private ObservableCollection<IPropertyDataInfo> _PropertyDataInfos = new ObservableCollection<IPropertyDataInfo>();
    public ObservableCollection<IPropertyDataInfo> PropertyDataInfos
    {
        get { return _PropertyDataInfos; }
        set
        {
            _PropertyDataInfos = value;
            RaisePropertyChanged();
        }
    }

    protected abstract IEnumerable<IPropertyDataInfo> GetPropertyDataInfos();
}

public abstract class ManagementPathManagementObjectPresenterBase : ManagementObjectPresenterBase
{
    protected ManagementPathManagementObjectPresenterBase()
    {
        var attribute = this.GetType().GetCustomAttribute<ManagementPathKeyAttribute>();
        if (attribute != null)
        {
            this.ManagementPathKey = attribute.Key;
        }
    }

    public string ManagementPathKey { get; set; }

    protected override IEnumerable<IPropertyDataInfo> GetPropertyDataInfos()
    {
        return ComputerManagementObjectManager.GetManagementClassInfos(managementClass: this.ManagementPathKey);
    }
}
