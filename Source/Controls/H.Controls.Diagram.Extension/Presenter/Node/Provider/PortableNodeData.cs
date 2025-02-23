
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;
using static System.Net.Mime.MediaTypeNames;

namespace H.Controls.Diagram.Extension
{
    public interface IPortableNodeData : ILinkDataCreator
    {
        public List<IPortData> PortDatas { get; set; }
    }

    public abstract class PortableNodeData : NodeData, IPortableNodeData
    {
        //[XmlIgnore]
        [Browsable(false)]
        public List<IPortData> PortDatas { get; set; } = new List<IPortData>();

        protected override void InitPort()
        {
            var ds = this.CreatePortDatas().ToList();
            foreach (var item in ds)
            {
                item.BuildTextData();
            }
            this.PortDatas = ds.ToList();
        }

        protected virtual IEnumerable<IPortData> CreatePortDatas()
        {
            {
                IPortData port = CreatePortData();
                port.Dock = Dock.Left;
                port.PortType = PortType.Both;
                yield return port;
            }

            {
                IPortData port = CreatePortData();
                port.Dock = Dock.Right;
                port.PortType = PortType.Both;
                yield return port;
            }

            {
                IPortData port = CreatePortData();
                port.Dock = Dock.Top;
                port.PortType = PortType.Both;
                yield return port;
            }

            {
                IPortData port = CreatePortData();
                port.Dock = Dock.Bottom;
                port.PortType = PortType.Both;
                yield return port;
            }
        }


        public override object Clone()
        {
            PortableNodeData data = base.Clone() as PortableNodeData;
            data.PortDatas.Clear();
            data.InitPort();
            return data;
        }
    }
}
