// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Extensions.Computer.ComputerManagementObjects;

[System.AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
sealed class ManagementPathKeyAttribute : Attribute
{
    readonly string _key;

    // This is a positional argument
    public ManagementPathKeyAttribute(string key)
    {
        this._key = key;
    }

    public string Key
    {
        get { return _key; }
    }
}
