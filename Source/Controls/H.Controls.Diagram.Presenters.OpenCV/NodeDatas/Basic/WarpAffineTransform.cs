using System.Windows.Media.Media3D;

namespace H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Basic;

[Display(Name = "仿射变换", GroupName = "基础函数", Description = "是一种几何变换，用于对图像进行线性变换，图像旋转，平移，缩放，剪切倾斜，对齐，校正", Order = 4)]
public class WarpAffineTransform : BasicOpenCVNodeDataBase
{
    public override void LoadDefault()
    {
        base.LoadDefault();
        this._srcPoints = new PointCollection { new System.Windows.Point(0, 0), new System.Windows.Point(1, 0), new System.Windows.Point(0, 1) };
        this._dstPoints = new PointCollection { new System.Windows.Point(0, 0), new System.Windows.Point(1, 0), new System.Windows.Point(0, 1) };
    }
    private PointCollection _srcPoints;
    [Display(Name = "源点", GroupName = "数据")]
    public PointCollection SrcPoints
    {
        get { return _srcPoints; }
        set
        {
            _srcPoints = value;
            RaisePropertyChanged();
        }
    }
    private PointCollection _dstPoints;
    [Display(Name = "目标点", GroupName = "数据")]
    public PointCollection DstPoints
    {
        get { return _dstPoints; }
        set
        {
            _dstPoints = value;
            RaisePropertyChanged();
        }
    }

    //private Matrix _matrix;
    //public Matrix Matrix
    //{
    //    get { return _matrix; }
    //    set
    //    {
    //        _matrix = value;
    //        RaisePropertyChanged();
    //    }
    //}


    protected override FlowableResult<Mat> Invoke(ISrcImageNodeData srcImageNodeData, IOpenCVNodeData from, IFlowableDiagramData diagram)
    {
        if (_srcPoints.Count != 3 || _dstPoints.Count != 3)
            return this.Error(from.Mat, "源点和目标点必须包含三个点。");

        //// 定义旋转中心和角度
        //Point2f center = new Point2f(src.Cols / 2, src.Rows / 2);
        //double angle = 45.0; // 旋转角度
        //double scale = 1.0;  // 缩放比例
        //// 计算旋转矩阵
        //Mat rotationMatrix = Cv2.GetRotationMatrix2D(center, angle, scale);

        IEnumerable<Point2f> src = this._srcPoints.Select(p => new Point2f((float)p.X, (float)p.Y));
        IEnumerable<Point2f> dst = this._dstPoints.Select(p => new Point2f((float)p.X, (float)p.Y));
        Mat transformMatrix = Cv2.GetAffineTransform(src, dst);
        Mat transformedImage = new Mat();
        Cv2.WarpAffine(from.Mat, transformedImage, transformMatrix, from.Mat.Size());
        return this.OK(transformedImage);
    }
}
