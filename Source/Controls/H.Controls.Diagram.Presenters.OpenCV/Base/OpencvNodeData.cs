global using H.Services.Logger;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace H.Controls.Diagram.Presenters.OpenCV.Base;
public abstract class OpenCVNodeData : OpenCVNodeDataBase
{
    protected virtual string GetDataPath(string dataPath)
    {
        if (string.IsNullOrEmpty(dataPath))
            return null;
        return System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, dataPath);
    }
    public override async Task<IFlowableResult> InvokeAsync(IFlowableLinkData previors, IFlowableDiagramData current)
    {
        var r = await this.BeforeInvokeAsync(previors, current);
        if (r.State == FlowableResultState.Error)
            return r;
        return await base.InvokeAsync(previors, current);
    }

}
