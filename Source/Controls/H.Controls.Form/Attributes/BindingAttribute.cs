// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.Form.Attributes;

[AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
public class BindingAttribute : Attribute
{
    public BindingAttribute(string path)
    {
        this.PropertyPath = new PropertyPath(path);
    }

    public PropertyPath PropertyPath { get; }

    public string[] GetPaths()
    {
        return this.PropertyPath?.Path?.Split('.');
    }

    public object GetValue(object obj)
    {
        string[] paths = this.GetPaths();
        if (obj == null) return null;
        if (paths == null) return null;
        if (paths.Length == 1) return obj;
        string propertyName = paths[1];
        //  ToDo：后面修改未递归兼容多级别
        return obj.GetType().GetProperty(propertyName).GetValue(obj);
    }
}
