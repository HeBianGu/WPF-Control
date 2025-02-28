// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control
using System.Windows;
namespace H.Controls.Diagram.Datas;

public interface INodeData : IData
{
    string ID { get; set; }
    Point Location { get; set; }
    void ApplayStyleTo(INodeData node);
}
