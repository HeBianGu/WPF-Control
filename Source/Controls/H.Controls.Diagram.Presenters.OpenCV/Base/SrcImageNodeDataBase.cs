global using H.Controls.Diagram.Datas;
using H.Services.Common;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml.Serialization;

namespace H.Controls.Diagram.Presenters.OpenCV.Base;

public interface ISrcImageNodeData : IOpenCVNodeData
{
    string SrcFilePath { get; set; }
}

public abstract class SrcImageNodeDataBase : OpenCVNodeDataBase, ISrcImageNodeData, IFilePathable
{
    protected SrcImageNodeDataBase()
    {
        this.UseStart = true;
    }

    private string _srcFilePath;
    [Browsable(false)]
    [Display(Name = "源文件地址", GroupName = "数据")]
    [PropertyItem(typeof(OpenFileDialogPropertyItem))]
    public virtual string SrcFilePath
    {
        get { return _srcFilePath; }
        set
        {
            _srcFilePath = value;
            RaisePropertyChanged();
        }
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

    protected override async Task<IFlowableResult> BeforeInvokeAsync(IFlowableLinkData previors, IFlowableDiagramData diagram)
    {
        if (File.Exists(this.SrcFilePath) == false)
        {
            var r = await IocMessage.Form?.ShowEdit(this, null, null, x =>
            {
                x.UsePropertyNames = nameof(SrcFilePath);
            });
            if (r != true)
                return this.Error("未设置源文件地址");
        }
        return await base.BeforeInvokeAsync(previors, diagram);
    }

    protected override FlowableResult<Mat> Invoke(ISrcImageNodeData srcImageNodeData, IOpenCVNodeData from, IFlowableDiagramData diagram)
    {
        var mat = new Mat(this.SrcFilePath, ImreadModes.Color);
        return this.OK(mat);
    }
}

