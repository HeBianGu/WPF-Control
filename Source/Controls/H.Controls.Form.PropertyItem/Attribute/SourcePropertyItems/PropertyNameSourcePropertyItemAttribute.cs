// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Collections;
using System.Reflection;

namespace H.Controls.Form.PropertyItem.Attribute.SourcePropertyItem
{
    public class PropertyNameSourcePropertyItemAttribute : SourcePropertyItemBaseAttribute
    {
        private PropertyInfo _sourcePropertyInfo;
        public PropertyNameSourcePropertyItemAttribute(Type type, string propertyName) : base(type)
        {
            this.PropertyName = propertyName;
        }

        public string PropertyName { get; set; }

        public override IEnumerable GetSource(PropertyInfo propertyInfo, object obj)
        {
            if(_sourcePropertyInfo==null)
                _sourcePropertyInfo = obj.GetType().GetProperty(this.PropertyName);
            return _sourcePropertyInfo.GetValue(obj) as IEnumerable;
        }
    }
}
