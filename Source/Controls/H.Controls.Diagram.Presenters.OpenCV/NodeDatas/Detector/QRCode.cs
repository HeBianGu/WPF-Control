namespace H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Detector;

[Display(Name = "二维码检测", GroupName = "基础检测", Order = 3)]
public class QRCode : DetectorOpenCVNodeDataBase
{
    protected override FlowableResult<Mat> Invoke(ISrcImageNodeData srcImageNodeData, IOpenCVNodeData from, IFlowableDiagramData diagram)
    {
        Mat image = from.Mat;
        // 2. 创建二维码检测器
        var qrDecoder = new QRCodeDetector();
        // 3. 检测二维码
        Point2f[] points;
        using (Mat straightQrCode = new Mat())
        {
            bool detected = qrDecoder.Detect(image, out points);
            if (detected && points != null)
            {
                //    bool detected = qrDecoder.DetectAndDecode(image, out points, out data);
                // 4. 解码二维码
                string data = qrDecoder.Decode(image, points, straightQrCode);
                if (!string.IsNullOrEmpty(data))
                {
                    // 5. 在图像上绘制二维码边界
                    for (int i = 0; i < points.Length; i++)
                    {
                        Cv2.Line(image, (Point)points[i], (Point)points[(i + 1) % points.Length],
                                this.OutputColor.ToScalar(), this.OutPutThickness);
                    }
                    return this.OK(image, "解码结果: " + data);
                }
                else
                {
                    return this.Error(image, "检测到二维码但解码失败");
                }
            }
            else
            {
                return this.Error(image, "未检测到二维码");
            }
        }
    }
}

