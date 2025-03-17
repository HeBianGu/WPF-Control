global using H.Controls.Diagram.GraphSource;
using System;
using System.Collections.Generic;

namespace H.Controls.Diagram;

public interface IDiagramDataSource : IGraphSource
{
    List<ILinkData> GetLinkDatas();
    List<INodeData> GetNodeDatas();
}
