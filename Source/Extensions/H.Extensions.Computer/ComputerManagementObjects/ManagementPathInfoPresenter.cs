// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Collections.ObjectModel;

namespace H.Extensions.Computer.ComputerManagementObjects;

public class ManagementPathInfoPresenter : ManagementObjectPresenterBase
{
    public ManagementPathInfoPresenter(ManagementPathInfo info, bool loadData = false)
    {
        this.Name = info.Name;
        this.ShortName = info.Name;
        this.GroupName = info.GroupName;
        this.Description = info.Description;
        this.Order = info.Order;
        this.ManagementPathKey = info.Key;

        this.PropertyDataInfos = new ObservableCollection<IPropertyDataInfo>(this.GetPropertyDataInfos());
    }

    public string ManagementPathKey { get; set; }

    protected override IEnumerable<IPropertyDataInfo> GetPropertyDataInfos()
    {
        return ComputerManagementObjectManager.GetManagementClassInfos(managementClass: this.ManagementPathKey);
    }
}
