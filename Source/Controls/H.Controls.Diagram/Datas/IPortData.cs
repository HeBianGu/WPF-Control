// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Windows;
using System.Windows.Controls;

namespace H.Controls.Diagram.Datas;

public interface IPortData : ILinkInitializer, IData
{
    string ID { get; set; }
    string NodeID { get; set; }
    string Name { get; set; }
    Dock Dock { get; set; }
    PortType PortType { get; set; }
    Thickness PortMargin { get; set; }
}
