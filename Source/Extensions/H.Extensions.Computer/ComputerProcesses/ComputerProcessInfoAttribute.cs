// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Extensions.Computer.ComputerProcesses;

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
public sealed class ComputerProcessInfoAttribute : Attribute
{
    readonly string _fileName;
    public ComputerProcessInfoAttribute(string fileName)
    {
        this._fileName = fileName;
    }

    public string FileName
    {
        get { return _fileName; }
    }
}
