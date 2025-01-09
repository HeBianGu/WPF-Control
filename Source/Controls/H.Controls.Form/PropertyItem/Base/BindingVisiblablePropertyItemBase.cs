// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


using H.Controls.Form.Base;
using System.Reflection;

namespace H.Controls.Form
{
    public abstract class BindingVisiblablePropertyItemBase : ObjectPropertyItemBase, IBindingVisibleable
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
    }
}
