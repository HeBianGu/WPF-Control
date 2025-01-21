// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


using System.Reflection;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace H.Controls.OrderBox
{
    public interface IProperty
    {
        bool UseDesc { get; }
        bool IsSelected { get; }
        string PropertyName { get; }
        [System.Text.Json.Serialization.JsonIgnore]
        
        [System.Xml.Serialization.XmlIgnore]
        PropertyInfo PropertyInfo { get; set; }
    }
}
