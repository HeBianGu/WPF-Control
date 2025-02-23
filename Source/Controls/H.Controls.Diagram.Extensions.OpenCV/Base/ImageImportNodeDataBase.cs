using System.Windows.Controls;

namespace H.Controls.Diagram.Extensions.OpenCV.Base;
public interface IImageImportNodeData : INodeData, IDisplayBindable
{

}

public abstract class ImageImportNodeDataBase : OpenCVNodeData, IImageImportNodeData
{
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
