// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace H.Controls.Diagram.GraphSource;

/// <summary>
/// 子类用于重写 重写Node和Link跟数据源的转换方式
/// </summary>
/// <typeparam name="NodeDataType"></typeparam>
/// <typeparam name="LinkDataType"></typeparam>
public abstract class GraphSource<NodeDataType, LinkDataType> : IGraphSource, IDataSource<NodeDataType, LinkDataType>
{
    [JsonIgnore]
    public List<Node> Nodes { get; set; } = new List<Node>();

    public GraphSource(List<Node> nodeSource)
    {
        this.Nodes = nodeSource;
    }

    public GraphSource(IEnumerable<NodeDataType> nodes, IEnumerable<LinkDataType> links)
    {
        System.Windows.Application.Current.Dispatcher.Invoke(() =>
        {
            if (nodes != null)
                foreach (NodeDataType unit in nodes)
                {
                    Node n = this.ConvertToNode(unit);
                    this.Nodes.Add(n);
                }

            if (links != null)
                foreach (LinkDataType link in links)
                {
                    this.ConvertToLink(link);
                }
        });

    }

    public IEnumerable<Node> GetNodes()
    {
        return this.Nodes;
    }

    /// <summary>
    /// 加载数据，由数据源到节点数据
    /// </summary>
    /// <param name="node"></param>
    /// <returns></returns>
    protected abstract Node ConvertToNode(NodeDataType node);

    /// <summary>
    /// 加载数据，由数据源到连线数据
    /// </summary>
    /// <param name="node"></param>
    /// <returns></returns>
    protected abstract Link ConvertToLink(LinkDataType node);

    public abstract List<NodeDataType> GetNodeDatas();

    public abstract List<LinkDataType> GetLinkDatas();
}
