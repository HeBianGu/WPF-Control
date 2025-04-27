namespace H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Basic;

[Display(Name = "图像校正", GroupName = "基础函数", Description = "透视失真校正", Order = 4)]
public class WarpPerspectiveTransform : BasicOpenCVNodeDataBase
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


    protected override FlowableResult<Mat> Invoke(ISrcImageNodeData srcImageNodeData, IOpenCVNodeData from, IFlowableDiagramData diagram)
    {
        // 定义原始点和目标点
        Point2f[] srcPoints = {
            new Point2f(100, 100), // 左上角
            new Point2f(500, 100), // 右上角
            new Point2f(100, 400), // 左下角
            new Point2f(500, 400)  // 右下角
        };

        Point2f[] dstPoints = {
            new Point2f(0, 0),     // 左上角
            new Point2f(400, 0),   // 右上角
            new Point2f(0, 300),   // 左下角
            new Point2f(400, 300)  // 右下角
            };
        // 计算透视变换矩阵
        Mat perspectiveMatrix = Cv2.GetPerspectiveTransform(srcPoints, dstPoints);

        // 应用透视变换
        Mat dst = new Mat();
        Cv2.WarpPerspective(from.Mat, dst, perspectiveMatrix, new Size(400, 300));
        return this.OK(dst);

    }
}
