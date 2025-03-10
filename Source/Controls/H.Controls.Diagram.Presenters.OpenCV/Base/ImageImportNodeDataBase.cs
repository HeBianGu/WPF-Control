global using H.Controls.Diagram.Datas;
using H.Services.Common;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace H.Controls.Diagram.Presenters.OpenCV.Base;
public abstract class ImageImportNodeDataBase : OpenCVNodeData
{
    protected ImageImportNodeDataBase()
    {
        this.UseStart = true;
    }

    //private string _srcFilePath;
    //[JsonInclude]
    [Browsable(true)]
    [Display(Name = "源文件地址", GroupName = "数据")]
    [PropertyItem(typeof(OpenFileDialogPropertyItem))]
    public override string SrcFilePath
    {
        get { return base.SrcFilePath; }
        set
        {
            base.SrcFilePath = value;
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

    protected override async Task<IFlowableResult> BeforeInvokeAsync(Part previors, Node current)
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
        return await base.BeforeInvokeAsync(previors, current);
    }

    public override IFlowableResult Invoke(Part previors, Node current)
    {
        this.Mat = new Mat(this.SrcFilePath, ImreadModes.Color);
        this.SrcMat = this.Mat;
        return base.Invoke(previors, current);
    }
}

