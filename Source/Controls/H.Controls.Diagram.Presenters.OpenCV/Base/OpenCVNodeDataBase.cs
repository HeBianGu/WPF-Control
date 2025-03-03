using System.Xml.Serialization;

namespace H.Controls.Diagram.Presenters.OpenCV.Base;
public abstract class OpenCVNodeDataBase : ActionNodeDataBase, IOpenCVNodeData, IFilePathable
{
    [Browsable(false)]
    [XmlIgnore]
    public Mat Mat { get; set; }

    [Browsable(false)]
    [XmlIgnore]
    public Mat SrcMat { get; set; }

    private bool _useReview = true;
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
}
