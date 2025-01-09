// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


using System.Reflection;

namespace H.Controls.Form
{
    public interface IPropertyValueChanged
    {
        void OnPropertyValueChanged(PropertyInfo propertyInfo, object o, object n);
    }
}
