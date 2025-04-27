namespace H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Basic;

[Display(Name = "图像对齐", GroupName = "基础函数", Description = "特征点匹配", Order = 4)]
public class HomographyTransform : BasicOpenCVNodeDataBase
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
        // 读取两张图像
        Mat img1 = Cv2.ImRead("image1.jpg");
        Mat img2 = Cv2.ImRead("image2.jpg");

        // 假设已经通过特征点匹配得到了对应点
        Point2d[] points1 = { new Point2d(100, 100), new Point2d(200, 100), new Point2d(100, 200), new Point2d(200, 200) };
        Point2d[] points2 = { new Point2d(120, 120), new Point2d(220, 120), new Point2d(120, 220), new Point2d(220, 220) };

        //Point2f[] srcPoints = new Point2f[] { new Point2f(0, 0), new Point2f(100, 0), new Point2f(0, 100) };
        //Point2f[] dstPoints = new Point2f[] { new Point2f(10, 10), new Point2f(90, 20), new Point2f(20, 90) };
        //Mat homography = Cv2.FindHomography(srcPoints, dstPoints, HomographyMethods.Ransac);
        // 计算透视变换矩阵
        Mat homography = Cv2.FindHomography(points1, points2, HomographyMethods.Ransac);
        // 应用透视变换
        Mat alignedImage = new Mat();
        Cv2.WarpPerspective(img1, alignedImage, homography, img2.Size());
        return this.OK(alignedImage);

    }
}