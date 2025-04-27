
using System.Security.Cryptography;

namespace H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Basic;


public abstract class SeamlessCloneBase : BasicOpenCVNodeDataBase
{
    private SeamlessCloneMethods _method = SeamlessCloneMethods.NormalClone;
    [DefaultValue(SeamlessCloneMethods.NormalClone)]
    [Display(Name = "Method", GroupName = "数据")]
    public SeamlessCloneMethods Method
    {
        get { return _method; }
        set
        {
            _method = value;
            RaisePropertyChanged();
        }
    }

    //public override IFlowableResult Invoke(Part previors, Node diagram)
    //{
    //    var path = this.DetectFilePath;
    //    Mat src = new Mat(path, ImreadModes.Color);
    //    Mat dst = this.GetFromMat(diagram);
    //    Mat src0 = src.Resize(dst.Size(), 0, 0, InterpolationFlags.Lanczos4);
    //    Mat mask = Mat.Zeros(src0.Size(), MatType.CV_8UC3);
    //    mask.Circle(200, 200, 100, Scalar.White, -1);
    //    Mat blend = new Mat();
    //    Cv2.SeamlessClone(src0, dst, mask, this.Point, blend, this.Method);
    //    this.Mat = blend;
    //    this.UpdateMatToView();
    //    return base.Invoke(previors, diagram);
    //}
}

[Display(Name = "无缝融合", GroupName = "基础函数", Description = "将一幅图像中的指定目标复制后粘贴到另一幅图像中，并自然的融合", Order = 80)]
public class SeamlessClone : SeamlessCloneBase
{
    public SeamlessClone()
    {
        this.DetectFilePath = ImagePath.Girl.ToDataPath();
    }

    private string _detectFilePath;
    [PropertyItem(typeof(OpenFileDialogPropertyItem))]
    [Display(Name = "检测图片路径", GroupName = "数据")]
    public string DetectFilePath
    {
        get { return _detectFilePath; }
        set
        {
            _detectFilePath = value;
            RaisePropertyChanged();
        }
    }

    private System.Windows.Point _point = new System.Windows.Point(260, 270);
    [Display(Name = "放置位置", GroupName = "数据")]
    public System.Windows.Point Point
    {
        get { return _point; }
        set
        {
            _point = value;
            RaisePropertyChanged();
        }
    }

    public override void LoadDefault()
    {
        base.LoadDefault();
        this.Point = new System.Windows.Point(260, 270);
    }

    protected override FlowableResult<Mat> Invoke(ISrcImageNodeData srcImageNodeData, IOpenCVNodeData from, IFlowableDiagramData diagram)
    {
        //string path = this.DetectFilePath;
        //using Mat src = new Mat(path, ImreadModes.Color);
        //using Mat dst = from.Mat;
        //using Mat src0 = src.Resize(dst.Size(), 0, 0, InterpolationFlags.Lanczos4);
        //using Mat mask = Mat.Zeros(src0.Size(), MatType.CV_8UC3);
        //mask.Circle(200, 200, 100, Scalar.White, -1);
        //Mat blend = new Mat();
        //Cv2.SeamlessClone(src0, dst, mask, this.Point.ToCVPoint(), blend, this.Method);
        //return this.OK(blend);

        string path = this.DetectFilePath;
        using Mat src = new Mat(path, ImreadModes.Color);
        using Mat dst = from.Mat;
        //using Mat src0 = src.Resize(dst.Size(), 0, 0, InterpolationFlags.Lanczos4);
        using Mat mask = new Mat(src.Size(), MatType.CV_8UC1, Scalar.White);
        //mask.Circle(200, 200, 100, Scalar.White, -1);
        Point center = new Point(dst.Width / 2, dst.Height / 2);
        Mat blend = new Mat();
        Cv2.SeamlessClone(src, dst, mask, center, blend, this.Method);
        return this.OK(blend);

        //// 创建掩膜（假设要插入img2到img1中）
        //Mat mask = new Mat(from.Mat.Size(), MatType.CV_8UC1, Scalar.White);
        //Point center = new Point(src.Width / 2, src.Height / 2);

        //Mat result = new Mat();
        //Cv2.SeamlessClone(from.Mat, src, mask, center, result, SeamlessCloneMethods.NormalClone);
        //return this.OK(blend);
    }
}
