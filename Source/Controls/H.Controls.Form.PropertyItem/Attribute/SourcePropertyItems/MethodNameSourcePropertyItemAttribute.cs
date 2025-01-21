// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Collections;
using System.Reflection;

namespace H.Controls.Form.PropertyItem.Attribute.SourcePropertyItem
{
    public class MethodNameSourcePropertyItemAttribute : SourcePropertyItemBaseAttribute
    {
        private MethodInfo _sourceMethodInfo;
        public MethodNameSourcePropertyItemAttribute(Type type, string methodName) : base(type)
        {
            this.MethodName = methodName;
        }

        public string MethodName { get; set; }

        public override IEnumerable GetSource(PropertyInfo propertyInfo, object obj)
        {
            if (_sourceMethodInfo == null)
                _sourceMethodInfo = obj.GetType().GetMethod(this.MethodName);
            return _sourceMethodInfo.Invoke(obj, null) as IEnumerable;
        }
    }
}
