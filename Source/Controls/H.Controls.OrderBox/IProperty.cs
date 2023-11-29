// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase


using System.Reflection;
using System.Text.Json.Serialization;
using System.Xml.Serialization;
using H.Providers.Ioc;

namespace H.Controls.OrderBox
{
    public interface IProperty
    {
        bool UseDesc { get; }
        bool IsSelected { get; }
        string PropertyName { get; }
        [JsonIgnore]
        [XmlIgnore]
        PropertyInfo PropertyInfo { get; set; }
    }
}
