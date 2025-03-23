
using H.Controls.Diagram;
using H.Controls.Diagram.Datas;
using H.Controls.Diagram.Parts;
using H.Controls.Diagram.Parts.Base;
using H.Controls.Diagram.Presenter.DiagramDatas.Base;
using H.Controls.Diagram.Presenter.Flowables;
using H.Controls.Diagram.Presenter.NodeDatas.Card;
using H.Controls.Diagram.Presenter.PortDatas;
using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace H.Test.Diagram
{

    public class TestNodeData : LineCardNodeData
    {
        public override IFlowableResult Invoke(IFlowableLinkData previors, IFlowableDiagramData diagram)
        {
            return base.Invoke(previors, diagram);
        }

        /// <summary>
        /// 自定义创建节点的类型
        /// </summary>
        /// <returns></returns>
        public override IFlowablePortData CreatePortData()
        {
            return new TestPortData(this.ID, PortType.Both);

        }

        protected override void InitPortDatas()
        {
            base.InitPortDatas();
        }

        protected override IEnumerable<IPortData> CreatePortDatas()
        {
            for (int i = 0; i < 3; i++)
            {
                {
                    IPortData port = CreatePortData();
                    port.Dock = Dock.Left;
                    port.PortType = (PortType)i;
                    yield return port;

                }

                {
                    IPortData port = CreatePortData();
                    port.Dock = Dock.Right;
                    port.PortType = (PortType)i;
                    yield return port;
                }

                {
                    IPortData port = CreatePortData();
                    port.Dock = Dock.Top;
                    port.PortType = (PortType)i;
                    yield return port;
                }

                {
                    IPortData port = CreatePortData();
                    port.Dock = Dock.Bottom;
                    port.PortType = (PortType)i;
                    yield return port;
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
