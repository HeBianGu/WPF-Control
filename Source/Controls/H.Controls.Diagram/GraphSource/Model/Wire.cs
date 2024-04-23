// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Xml.Serialization;

namespace H.Controls.Diagram
{
    public class Wire
    {
        [XmlAttribute("From")]
        public string From { get; set; }

        [XmlAttribute("To")]
        public string To { get; set; }

        [XmlAttribute("FromPort")]
        public string FromPort { get; set; }

        [XmlAttribute("ToPort")]
        public string ToPort { get; set; }
    }
}
