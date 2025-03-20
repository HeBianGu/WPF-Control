using Point = OpenCvSharp.Point;
using Rect = OpenCvSharp.Rect;
using Size = OpenCvSharp.Size;

namespace H.Controls.Diagram.Presenters.OpenCV.Base;

[Icon(FontIcons.EmojiTabPeople)]
public abstract class CascadeClassifierOpenCVNodeDataBase : OpenCVNodeDataBase, ICascadeClassifierOpenCVNodeData
{
    public override void LoadDefault()
    {
        base.LoadDefault();
        this.MinSize = new System.Windows.Size(30, 30);
        this.MaxSize = new System.Windows.Size(500, 500);
    }
    private double _scaleFactor = 1.1;
    [DefaultValue(1.1)]
    [Display(Name = "Scale Factor", GroupName = "数据")]
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
    [Display(Name = "Min Neighbors", GroupName = "数据")]
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

    private System.Windows.Size _minSize = new System.Windows.Size(30, 30);
    [Display(Name = "Min Size", GroupName = "数据")]
    public System.Windows.Size MinSize
    {
        get { return _minSize; }
        set
        {
            _minSize = value;
            RaisePropertyChanged();
        }
    }

    private System.Windows.Size _maxSize = new System.Windows.Size(500, 500);
    [DefaultValue(null)]
    [Display(Name = "Max Size", GroupName = "数据")]
    public System.Windows.Size MaxSize
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
                gray, this.ScaleFactor, this.MinNeighbors, this.Flags, this.MinSize.ToCVSize(), this.MaxSize.ToCVSize());

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
