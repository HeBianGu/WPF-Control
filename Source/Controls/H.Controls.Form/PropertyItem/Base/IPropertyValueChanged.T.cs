// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


using System.Reflection;

namespace H.Controls.Form
{
    public interface IPropertyValueChanged<PropertyType>
    {
        void OnPropertyValueChanged(PropertyInfo propertyInfo, PropertyType o, PropertyType n);
    }
}
