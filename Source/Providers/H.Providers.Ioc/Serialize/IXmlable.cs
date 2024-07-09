// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Xml;

namespace H.Providers.Ioc
{
    public interface IXmlable
    {
        void FromXml(XmlElement xmlEle, XmlDocument cnt, Func<PropertyInfo, object, bool> match = null);

        void ToXml(XmlElement xmlEle, XmlDocument cnt, Func<PropertyInfo, object, bool> match = null);
    }
}