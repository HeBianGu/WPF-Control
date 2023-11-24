﻿// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Windows.Controls;
using System.Xml.Serialization;

namespace H.Controls.Diagram
{
    public class Socket
    {
        [XmlAttribute("Name")]
        public string Name { get; set; }

        [XmlAttribute("Dock")]
        public Dock Dock { get; set; }

        [XmlAttribute("Color")]
        public string Color { get; set; }

        [XmlAttribute("Index")]
        public int Index { get; set; }
    }
}