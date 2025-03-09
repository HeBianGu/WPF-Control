using H.Services.Common;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace H.Controls.Diagram.Presenters.OpenCV.Base;
public abstract class OpenCVNodeDataBase : ActionNodeDataBase, IOpenCVNodeData, IFilePathable
{
    ~OpenCVNodeDataBase()
    {
        this.Mat?.Dispose();
        this.SrcMat?.Dispose();
    }
    [JsonIgnore]
    [Browsable(false)]
    [XmlIgnore]
    public Mat Mat { get; set; }

    [JsonIgnore]
    [Browsable(false)]
    [XmlIgnore]
    public Mat SrcMat { get; set; }

    private bool _useReview = true;
    [JsonIgnore]
    [Browsable(false)]
    [XmlIgnore]
    public bool UseReview
    {
        get { return _useReview; }
        set
        {
            _useReview = value;
            RaisePropertyChanged();
        }
    }

    private ImageSource _imageSource;
    [JsonIgnore]
    [Browsable(false)]
    [XmlIgnore]
    public ImageSource ImageSource
    {
        get { return _imageSource; }
        set
        {
            _imageSource = value;
            RaisePropertyChanged();
        }
    }


    private string _srcFilePath;
    [Display(Name = "源文件地址", GroupName = "数据")]
    [PropertyItem(typeof(OpenFileDialogPropertyItem))]
    public string SrcFilePath
    {
        get { return _srcFilePath; }
        set
        {
            _srcFilePath = value;
            RaisePropertyChanged();
        }
    }

    protected virtual async Task<IFlowableResult> BeforeInvokeAsync(Part previors, Node current)
    {
        if (string.IsNullOrEmpty(this.SrcFilePath))
        {
            var r = await IocMessage.Form?.ShowEdit(this, null, null, x =>
             {
                 x.UsePropertyNames = nameof(SrcFilePath);
             });
            if (r != true)
                return this.Error("未设置源文件地址");
        }
        return this.OK();
    }
}
