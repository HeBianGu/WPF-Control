// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Collections.Generic;

namespace H.Controls.Diagram.GraphSource;

public interface IGraphSource
{
    List<Node> Nodes { get; set; }
}
