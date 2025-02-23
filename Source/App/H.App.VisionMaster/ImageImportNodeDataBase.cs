
using H.Controls.Diagram;
using H.Mvvm;
using H.Services.Common;
using System.Collections.Generic;
using System.Windows.Controls;

namespace H.App.VisionMaster;

public interface IImageImportNodeData : INodeData, IDisplayBindable
{

}

public abstract class ImageImportNodeDataBase : ActionNodeDataBase, IImageImportNodeData
{
    public string ImagePath { get; set; }
    public string ImageName { get; set; }
    public string ImageDescription { get; set; }

    protected override IEnumerable<IPortData> CreatePortDatas()
    {
        {
            IPortData port = CreatePortData();
            port.Dock = Dock.Bottom;
            port.PortType = PortType.OutPut;
            yield return port;
        }
    }
}
