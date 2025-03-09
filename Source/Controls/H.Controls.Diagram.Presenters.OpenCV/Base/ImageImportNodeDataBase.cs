global using H.Controls.Diagram.Datas;
using H.Services.Common;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace H.Controls.Diagram.Presenters.OpenCV.Base;
public abstract class ImageImportNodeDataBase : OpenCVNodeData
{
    protected ImageImportNodeDataBase()
    {
        this.UseStart = true;
    }
    protected override IEnumerable<IPortData> CreatePortDatas()
    {
        {
            IPortData port = CreatePortData();
            port.Dock = Dock.Bottom;
            port.PortType = PortType.OutPut;
            yield return port;
        }
    }


    public override async Task<IFlowableResult> InvokeAsync(Part previors, Node current)
    {
        var r = await this.BeforeInvokeAsync(previors, current);
        if (r.State == FlowableResultState.Error)
            return r;
        return await base.InvokeAsync(previors, current);
    }

    public override IFlowableResult Invoke(Part previors, Node current)
    {
        this.Mat = new Mat(this.SrcFilePath, ImreadModes.Color);
        this.SrcMat = this.Mat;
        return base.Invoke(previors, current);
    }
}

