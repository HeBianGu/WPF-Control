// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using static System.Environment;

namespace H.Extensions.Computer.ComputerSpecialFolders;

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
public sealed class SpecialFolderInfoAttribute : Attribute
{
    readonly SpecialFolder _SpecialFolder;
    public SpecialFolderInfoAttribute(SpecialFolder specialFolder)
    {
        this._SpecialFolder = specialFolder;
    }

    public SpecialFolder SpecialFolder
    {
        get { return _SpecialFolder; }
    }
}
