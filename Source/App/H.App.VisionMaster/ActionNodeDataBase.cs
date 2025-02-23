
using H.Controls.Diagram;
using H.Controls.Diagram.Extension;
using System.Collections.Generic;
using System.Windows.Controls;

namespace H.App.VisionMaster;

public abstract class ActionNodeDataBase : LineCardNodeData
{
    protected override IEnumerable<IPortData> CreatePortDatas()
    {
        {
            IPortData port = CreatePortData();
            port.Dock = Dock.Top;
            port.PortType = PortType.Input;
            yield return port;
        }
        {
            IPortData port = CreatePortData();
            port.Dock = Dock.Bottom;
            port.PortType = PortType.OutPut;
            yield return port;
        }
    }
}