global using H.Controls.Diagram.Datas;
using System.Windows.Controls;

namespace H.Controls.Diagram.Presenters.OpenCV.Base;
public abstract class ImageImportNodeDataBase : OpenCVNodeData
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

    public override IFlowableResult Invoke(Part previors, Node current)
    {
        this.Mat = new Mat(this.SrcFilePath, ImreadModes.Color);
        this.SrcMat = this.Mat;
        return base.Invoke(previors, current);
    }
}

