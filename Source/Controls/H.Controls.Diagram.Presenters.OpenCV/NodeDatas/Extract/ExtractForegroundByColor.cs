namespace H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Extract;

[Display(Name = "提取前景", GroupName = "提取", Description = "基于颜色范围（适合纯色背景，如绿幕）", Order = 0)]
public class ExtractForegroundByColor : ExtractOpenCVNodeDataBase
{
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
        //using Mat src = Cv2.ImRead(srcImageNodeData.SrcFilePath, ImreadModes.Grayscale);

        // 读取图像
        //Mat img = Cv2.ImRead(inputPath);
        Mat img = from.Mat;
        if (img.Empty())
            throw new Exception("无法加载图像！");

        // 转换为HSV颜色空间
        Mat hsv = new Mat();
        Cv2.CvtColor(img, hsv, ColorConversionCodes.BGR2HSV);

        // 定义绿色背景范围（示例）
        Scalar lowerGreen = new Scalar(35, 50, 50);   // HSV下限
        Scalar upperGreen = new Scalar(85, 255, 255); // HSV上限

        // 创建掩膜并提取前景
        Mat mask = new Mat();
        Cv2.InRange(hsv, lowerGreen, upperGreen, mask);
        Cv2.BitwiseNot(mask, mask); // 反转掩膜（前景=255）

        Mat foreground = new Mat();
        Cv2.BitwiseAnd(img, img, foreground, mask);

        //// 保存结果
        //Cv2.ImWrite(outputPath, foreground);
        //return foreground;
        return this.OK(foreground);
    }

}
