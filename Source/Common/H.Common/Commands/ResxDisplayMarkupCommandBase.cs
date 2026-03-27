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
        if (result == null)
        {
            System.Diagnostics.Debug.WriteLine(key);
        }
        return result;
    }

    protected virtual string GetResxDescription()
    {
        var type = this.GetType();
        string key = $"Type_{type.Name}_Description";
        var result = this.GetResourceManager(type)?.GetString(key);
        if (result == null)
        {
            System.Diagnostics.Debug.WriteLine(key);
        }
        return result;
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
