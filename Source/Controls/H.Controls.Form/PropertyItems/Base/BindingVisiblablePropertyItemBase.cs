// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

namespace H.Controls.Form.PropertyItems.Base;

public interface IRefreshOnValueChanged
{
    bool CanRefresh { get; }
}

public abstract class BindingVisiblablePropertyItemBase : ObjectPropertyItemBase, IBindingVisibleable, IRefreshOnValueChanged
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

    #region - IRefreshOnValueChanged -

    private bool? _canRefresh;
    public bool CanRefresh
    {
        get
        {
            if (this._canRefresh == null)
                this._canRefresh = this.PropertyInfo.GetCustomAttribute<RefreshOnValueChangedAttribute>()?.CanRefresh == true;
            return this._canRefresh.Value;
        }
    }
    #endregion
}
