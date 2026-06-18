// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Extensions.Computer.ComputerManagementObjects;

public class ManagementPathInfo
{
    public ManagementPathInfo(string key, string name, string groupName, string description, int order)
    {
        this.Key = key;
        this.Name = name;
        this.GroupName = groupName;
        this.Description = description;
        this.Order = order;
    }

    public string Key { get; }

    public string Name { get; }

    public string GroupName { get; }

    public string Description { get; }

    public int Order { get; }
}
