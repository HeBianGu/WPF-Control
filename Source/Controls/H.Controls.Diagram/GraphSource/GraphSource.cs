// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System.Collections.Generic;

namespace H.Controls.Diagram
{
    /// <summary>
    /// 子类用于重写 重写Node和Link跟数据源的转换方式
    /// </summary>
    /// <typeparam name="NodeDataType"></typeparam>
    /// <typeparam name="LinkDataType"></typeparam>
    public abstract class GraphSource<NodeDataType, LinkDataType> : IGraphSource, IDataSource<NodeDataType, LinkDataType>
    {
        public List<Node> NodeSource { get; private set; } = new List<Node>();

        public GraphSource(List<Node> nodeSource)
        {
            this.NodeSource = nodeSource;
        }

        public GraphSource(IEnumerable<NodeDataType> nodes, IEnumerable<LinkDataType> links)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(() =>
            {
                foreach (NodeDataType unit in nodes)
                {
                    Node n = this.ConvertToNode(unit);
                    this.NodeSource.Add(n);
                }

                foreach (LinkDataType link in links)
                {
                    this.ConvertToLink(link);
                }
            });

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

        public abstract List<NodeDataType> GetNodeType();

        public abstract List<LinkDataType> GetLinkType();
    }


}
