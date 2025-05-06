// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

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
        PropertyInfo propertyInfo = this.Obj.GetType().GetProperty(attribute.PropertyName);
        return propertyInfo == null ? null : propertyInfo;
    }

    public virtual bool GetVisible()
    {
        if (this._methodInfo != null)
        {
            var v = this._methodInfo?.Invoke(this.Obj, null);
            if (v is bool b && b == false)
                return false;
        }
        if (this._propertyInfo != null)
        {
            var v = this._propertyInfo?.GetValue(this.Obj);
            if (v is bool b && b == false)
                return false;
        }
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
