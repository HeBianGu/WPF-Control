using H.Mvvm;
using System.Windows.Controls;

namespace HeBianGu.Diagram.OpenCV;

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
