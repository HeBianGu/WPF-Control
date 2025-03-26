// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

namespace H.Controls.Form.PropertyItems.Base;

public interface IRefreshOnValueChanged
{
    bool CanRefresh { get; }
}

public abstract class BindingVisiblablePropertyItemBase : ObjectPropertyItemBase, IBindingVisibleable, IRefreshOnValueChanged
{
    private readonly MethodInfo _methodInfo;
    protected BindingVisiblablePropertyItemBase(PropertyInfo property, object obj) : base(property, obj)
    {
        this._methodInfo = this.CreateMethodInfo();
    }

    protected virtual MethodInfo CreateMethodInfo()
    {
        BindingVisiblableMethodNameAttribute attribute = this.PropertyInfo.GetCustomAttribute<BindingVisiblableMethodNameAttribute>();
        if (attribute?.MethodName == null)
            return null;
        MethodInfo method = this.Obj.GetType().GetMethod(attribute.MethodName);
        return method == null ? null : method;
    }

    public virtual bool GetVisible()
    {
        return this._methodInfo?.Invoke(this.Obj, null) is not bool l || l != false;
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
