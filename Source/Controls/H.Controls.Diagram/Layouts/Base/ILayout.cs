// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

namespace H.Controls.Diagram.Layouts.Base;

public interface ILayout
{
    Diagram Diagram { get; set; }
    void DoLayout(params Node[] nodes);
    void UpdateNode(params Node[] nodes);
    void RemoveNode(params Node[] nodes);
    void AddNode(params Node[] nodes);
    void DoLayoutLink(Link link);
}
