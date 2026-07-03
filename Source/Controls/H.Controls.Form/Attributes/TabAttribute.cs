// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.Form.Attributes;

[System.AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
public sealed class TabAttribute : Attribute
{
    readonly string _tab;

    public TabAttribute(string positionalString)
    {
        this._tab = positionalString;
    }

    public string Tab
    {
        get { return _tab; }
    }
}