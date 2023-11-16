// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

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
