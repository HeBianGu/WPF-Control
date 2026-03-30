// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using System.Resources;

namespace H.Common.Commands;

public abstract class ResxDisplayMarkupCommandBase : DisplayMarkupCommandBase
{
    const string resxFormat = "  <data name=\"{0}\" xml:space=\"preserve\">\r\n    <value>{1}</value>\r\n  </data>";
    public ResxDisplayMarkupCommandBase()
    {
        this.UpdateResx();
    }
    protected void UpdateResx()
    {
        string rname = this.GetResxName();
        string rdesc = this.GetResxDescription();
        this.Name = rname ?? this.Name;
        this.Description = rdesc ?? this.Description;
    }

    protected virtual string GetResxName()
    {
        var type = this.GetType();
        string key = $"Type_{type.Name}";
        var result = this.GetResourceManager(type)?.GetString(key);
#if DEBUG
        if (result == null && this.Name != null)
            this.WhiteLine(key, this.Name);
#endif
        return result;
    }

    protected virtual string GetResxDescription()
    {
        var type = this.GetType();
        string key = $"Type_{type.Name}_Description";
        var result = this.GetResourceManager(type)?.GetString(key);
#if DEBUG
        if (result == null && this.Description != null)
            this.WhiteLine(key, this.Description);
#endif
        return result;
    }

    protected void WhiteLine(string key, string value)
    {
        string v = string.Format(resxFormat, key, value);
        System.Diagnostics.Debug.WriteLine(v);
    }

    private ResourceManager _resourceManager;
    protected ResourceManager GetResourceManager(Type type)
    {
        if (this._resourceManager == null)
            this._resourceManager = this.CreateResourceManager(type);
        return this._resourceManager;
    }

    private ResourceManager CreateResourceManager(Type type)
    {
        return new ResourceManager($"{type.Assembly.GetName().Name}.Properties.Resources", type.Assembly);
    }
}
