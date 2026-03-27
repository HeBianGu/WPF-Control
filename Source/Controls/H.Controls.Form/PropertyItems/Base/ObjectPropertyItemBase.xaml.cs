// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Mvvm.Commands;
using System.Reflection;
using System.Resources;
using System.Xml.Linq;

namespace H.Controls.Form.PropertyItems.Base;

public abstract class ObjectPropertyItemBase : ResxDisplayBindableBase, IPropertyItem, IValueChangeable
{
    public ObjectPropertyItemBase(PropertyInfo property, object obj)
    {
        this.PropertyInfo = property;
        this.Obj = obj;
        DisplayAttribute display = property.GetCustomAttribute<DisplayAttribute>();
        this.Name = property.Name;
        if (display != null)
        {
            this.Name = display == null ? property.Name : display.Name;
            this.TabGroup = display?.Prompt;
            this.GroupName = display?.GroupName;
            this.Description = display?.Description;
            this.Order = display == null ? 999 : display.GetOrder().HasValue ? display.GetOrder().Value : 999;
        }

        ReadOnlyAttribute readyOnly = property.GetCustomAttribute<ReadOnlyAttribute>();
        this.ReadOnly = readyOnly?.IsReadOnly == true;
        if (!this.PropertyInfo.CanWrite)
            this.ReadOnly = true;

        BrowsableAttribute browsable = property.GetCustomAttribute<BrowsableAttribute>();
        this.Visibility = browsable == null || browsable.Browsable ? Visibility.Visible : Visibility.Collapsed;
        this.Icon = property.GetCustomAttribute<IconAttribute>()?.Icon;

        this.UpdateResx();
    }
    public string TabGroup { get; set; }
    public PropertyInfo PropertyInfo { get; set; }
    public object Obj { get; set; }
    public bool ReadOnly { get; set; }
    public Visibility Visibility { get; set; }
    public Action<object> ValueChanged { get; set; }

    public bool UseSetDefault { get; set; } = false;
    private bool _isHitTestVisible = true;
    public bool IsHitTestVisible
    {
        get { return _isHitTestVisible; }
        set
        {
            _isHitTestVisible = value;
            RaisePropertyChanged();
        }
    }

    public abstract void LoadValue();

    protected override string GetResxName()
    {
        if (this.Obj == null || this.PropertyInfo == null)
            return null;
        var type = this.Obj.GetType();
        string key = $"Property_{type.Name}_{this.PropertyInfo.Name}";
        var result = this.GetResourceManager(type)?.GetString(key);
        if (result == null)
        {
            System.Diagnostics.Debug.WriteLine(key);
        }
        return result;
    }

    protected override string GetResxGroupName()
    {
        if (this.Obj == null || this.PropertyInfo == null)
            return null;
        var type = this.Obj.GetType();
        string key = $"Property_{type.Name}_{this.PropertyInfo.Name}_GroupName";
        var result = this.GetResourceManager(type)?.GetString(key);
        if (result == null)
            System.Diagnostics.Debug.WriteLine(key);
        return result;

    }

    protected override string GetResxDescription()
    {
        if (this.Obj == null || this.PropertyInfo == null)
            return null;
        var type = this.Obj.GetType();
        string key = $"Property_{type.Name}_{this.PropertyInfo.Name}_Description";
        var result = this.GetResourceManager(type)?.GetString(key);
        if (result == null)
            System.Diagnostics.Debug.WriteLine(key);
        return result;
    }
}
