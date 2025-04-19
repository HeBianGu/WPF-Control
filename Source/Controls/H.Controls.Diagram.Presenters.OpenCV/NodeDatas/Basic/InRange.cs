namespace H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Basic;

[Display(Name = "二值掩膜", GroupName = "基础函数", Description = "根据设定的阈值范围，从图像中提取符合要求的像素区域，生成一个二值掩膜（Mask）", Order = 10)]
public class InRange : BasicOpenCVNodeDataBase
{
    public override void LoadDefault()
    {
        base.LoadDefault();

        //// 定义红色的BGR范围（注意OpenCV默认是BGR格式）
        //Scalar lowerRed = new Scalar(0, 50, 50);    // B最小值, G最小值, R最小值
        //Scalar upperRed = new Scalar(10, 255, 200);  // B最大值, G最大值, R最大值

        //// 红色需要两个范围（色相在 0° 和 180° 附近）
        //Scalar lowerRed1 = new Scalar(0, 70, 30);    // 色相 0° 附近，饱和度和明度下限
        //Scalar upperRed1 = new Scalar(8, 255, 150);  // 色相 8° 范围，饱和度和明度上限

        //Scalar lowerRed2 = new Scalar(172, 70, 30);  // 色相 172° 附近（紫红色调）
        //Scalar upperRed2 = new Scalar(180, 255, 150); // 色相 180° 范围

        // 定义绿色的HSV范围
        Scalar lowerGreen = new Scalar(35, 50, 50);   // H最小值, S最小值, V最小值
        Scalar upperGreen = new Scalar(85, 255, 255); // H最大值, S最大值, V最R最大值
        this._lowerScalar = lowerGreen;
        this._upperScalar = upperGreen;
    }
    private Scalar _lowerScalar = new Scalar(35, 50, 50);
    [Display(Name = "HSV下限", GroupName = "数据")]
    public Scalar LowerScalar
    {
        get { return _lowerScalar; }
        set
        {
            _lowerScalar = value;
            RaisePropertyChanged();
        }
    }
    private Scalar _upperScalar = new Scalar(85, 255, 255);
    [Display(Name = "HSV上限", GroupName = "数据")]
    public Scalar UpperScalar
    {
        get { return _upperScalar; }
        set
        {
            _upperScalar = value;
            RaisePropertyChanged();
        }
    }

    protected override FlowableResult<Mat> Invoke(ISrcImageNodeData srcImageNodeData, IOpenCVNodeData from, IFlowableDiagramData diagram)
    {
        Mat hsv = from.Mat;
        Mat mask = new Mat();
        // 需要前序流程颜色处理 ColorConversionCodes.BGR2HSV)
        Cv2.InRange(hsv, this.LowerScalar, this.UpperScalar, mask);
        return this.OK(mask);
    }
}
