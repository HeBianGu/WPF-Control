// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.Form.PropertyItems.Base;

public interface IRefreshFilterOnValueChanged
{
    bool CanRefreshFilterOnValueChanged { get; }
}

public interface IRefreshSourceOnValueChanged
{
    IEnumerable<string> GetPropertyNames();
}

public interface ISelectSourcePropertyItem : IPropertyItem
{
    void RefreshSource();
}


public abstract class BindingVisiblablePropertyItemBase : ObjectPropertyItemBase, IBindingVisibleable, IRefreshFilterOnValueChanged, IRefreshSourceOnValueChanged
{
    private readonly MethodInfo _methodInfo;
    private readonly PropertyInfo _propertyInfo;
    protected BindingVisiblablePropertyItemBase(PropertyInfo property, object obj) : base(property, obj)
    {
        this._methodInfo = this.CreateMethodInfo();
        this._propertyInfo = this.CreatePropertyInfo();
    }

    protected virtual MethodInfo CreateMethodInfo()
    {
        BindingVisiblableMethodNameAttribute attribute = this.PropertyInfo.GetCustomAttribute<BindingVisiblableMethodNameAttribute>();
        if (attribute?.MethodName == null)
            return null;
        MethodInfo method = this.Obj.GetType().GetMethod(attribute.MethodName);
        return method == null ? null : method;
    }

    protected virtual PropertyInfo CreatePropertyInfo()
    {
        BindingVisibleablePropertyNameAttribute attribute = this.PropertyInfo.GetCustomAttribute<BindingVisibleablePropertyNameAttribute>();
        if (attribute?.PropertyName == null)
            return null;
        var method = this.Obj.GetType().GetProperty(attribute.PropertyName);
        return method == null ? null : method;
    }

    public virtual bool GetVisible()
    {
        var mr = this._methodInfo?.Invoke(this.Obj, null);
        if (mr is bool mb && mb == false)
            return false;
        var pr = this._propertyInfo?.GetValue(this.Obj);
        if (pr is bool pb && pb == false)
            return false;
        return true;
    }

 

    #region - IRefreshFilterOnValueChanged -

    private bool? _CanRefreshFilterOnValueChanged;
    public virtual bool CanRefreshFilterOnValueChanged
    {
        get
        {
            if (this._CanRefreshFilterOnValueChanged == null)
                this._CanRefreshFilterOnValueChanged = this.PropertyInfo.GetCustomAttribute<RefreshFilterOnValueChangedAttribute>()?.CanRefresh == true;
            return this._CanRefreshFilterOnValueChanged.Value;
        }
    }
    #endregion

    #region - IRefreshSourceOnValueChanged -

    IEnumerable<string> IRefreshSourceOnValueChanged.GetPropertyNames()
    {
        var names = this.PropertyInfo.GetCustomAttribute<RefreshSourceOnValueChangedAttribute>()?.SourcePropertyNames;
        if (names == null)
            yield break;
        foreach (var item in names.Split(','))
        {
            yield return item;
        }
    }
    #endregion
}
