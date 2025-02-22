
using H.Controls.Diagram;
using H.Controls.Diagram.Extension;
using H.Controls.Diagram.Extensions.Workflow;
using System;
using System.Windows.Controls;

namespace H.Test.Diagram
{

    public class TestNodeData : LineCardNodeData
    {
        public override IFlowableResult Invoke(Part previors, Node current)
        {
            return base.Invoke(previors, current);
        }

        /// <summary>
        /// 自定义创建节点的类型
        /// </summary>
        /// <returns></returns>
        public override IPortData CreatePortData()
        {
            return new TestPortData(this.ID, PortType.Both);

        }

        /// <summary>
        /// 自定义节点个数
        /// </summary>
        protected override void InitPort()
        {
            this.PortDatas.Clear();

            for (int i = 0; i < 3; i++)
            {
                {
                    IPortData port = CreatePortData();
                    port.Dock = Dock.Left;
                    port.PortType = (PortType)i;
                    this.PortDatas.Add(port);

                }

                {
                    IPortData port = CreatePortData();
                    port.Dock = Dock.Right;
                    port.PortType = (PortType)i;
                    this.PortDatas.Add(port);
                }

                {
                    IPortData port = CreatePortData();
                    port.Dock = Dock.Top;
                    port.PortType = (PortType)i;
                    this.PortDatas.Add(port);
                }

                {
                    IPortData port = CreatePortData();
                    port.Dock = Dock.Bottom;
                    port.PortType = (PortType)i;
                    this.PortDatas.Add(port);
                }
            }

            foreach (var item in this.PortDatas)
            {
                if (item is TextPortData text)
                {
                    if (item.PortType == PortType.Input)
                    {
                        text.Text = "Input";
                    }
                    if (item.PortType == PortType.OutPut)
                    {
                        text.Text = "OutPut";
                    }
                }
            }

        }
    }
}
