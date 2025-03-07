using System.Text.Json.Serialization;
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
    [JsonIgnore]
    public string SrcFilePath { get; set; }
}
