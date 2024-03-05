// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Collections.Generic;
using System.Xml.Serialization;

namespace H.Controls.Diagram
{
    public class Unit
    {
        [XmlAttribute("Id")]
        public string Id { get; set; }

        [XmlAttribute("X")]
        public double X { get; set; }

        [XmlAttribute("Y")]
        public double Y { get; set; }

        [XmlAttribute("Text")]
        public string Text { get; set; }

        public List<Socket> Sockets { get; set; }
    }
}
