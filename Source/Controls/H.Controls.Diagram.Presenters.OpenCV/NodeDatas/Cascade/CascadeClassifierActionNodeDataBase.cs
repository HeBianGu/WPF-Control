using Point = OpenCvSharp.Point;
using Rect = OpenCvSharp.Rect;
using Size = OpenCvSharp.Size;

namespace H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Cascade;

[Icon("\xE11D")]
public abstract class CascadeClassifierActionNodeDataBase : OpenCVNodeData, ICascadeClassifierActionNodeData
{
    public override void LoadDefault()
    {
        base.LoadDefault();
        this.MinSize = new Size(30, 30);
    }
    private double _scaleFactor = 1.1;
    [DefaultValue(1.1)]
    [Display(Name = "ScaleFactor", GroupName = "数据")]
    public double ScaleFactor
    {
        get { return _scaleFactor; }
        set
        {
            _scaleFactor = value;
            RaisePropertyChanged();
        }
    }


    private int _minNeighbors = 3;
    [DefaultValue(3)]
    [Display(Name = "MinNeighbors", GroupName = "数据")]
    public int MinNeighbors
    {
        get { return _minNeighbors; }
        set
        {
            _minNeighbors = value;
            RaisePropertyChanged();
        }
    }

    private HaarDetectionTypes _flags = HaarDetectionTypes.ScaleImage;
    [DefaultValue(HaarDetectionTypes.ScaleImage)]
    [Display(Name = "Flags", GroupName = "数据")]
    public HaarDetectionTypes Flags
    {
        get { return _flags; }
        set
        {
            _flags = value;
            RaisePropertyChanged();
        }
    }


    private Size? _minSize = new Size(30, 30);
    [Display(Name = "MinSize", GroupName = "数据")]
    public Size? MinSize
    {
        get { return _minSize; }
        set
        {
            _minSize = value;
            RaisePropertyChanged();
        }
    }


    private Size? _maxSize = null;
    [DefaultValue(null)]
    [Display(Name = "MaxSize", GroupName = "数据")]
    public Size? MaxSize
    {
        get { return _maxSize; }
        set
        {
            _maxSize = value;
            RaisePropertyChanged();
        }
    }


    protected Mat DetectFace(CascadeClassifier cascade, Mat src)
    {
        Mat result;
        using (Mat gray = new Mat())
        {
            result = src.Clone();
            Cv2.CvtColor(src, gray, ColorConversionCodes.BGR2GRAY);

            // Detect faces
            Rect[] faces = cascade.DetectMultiScale(
                gray, this.ScaleFactor, this.MinNeighbors, this.Flags, this.MinSize, this.MaxSize);

            // Render all detected faces
            foreach (Rect face in faces)
            {
                Point center = new Point
                {
                    X = (int)(face.X + face.Width * 0.5),
                    Y = (int)(face.Y + face.Height * 0.5)
                };
                Size axes = new Size
                {
                    Width = (int)(face.Width * 0.5),
                    Height = (int)(face.Height * 0.5)
                };
                Cv2.Ellipse(result, center, axes, 0, 0, 360, new Scalar(255, 0, 255), 4);
            }
        }
        return result;
    }
}
