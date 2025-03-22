

namespace H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Detector;

[Display(Name = "模板匹配", GroupName = "基础检测", Order = 0)]
public class MatchTemplate : MatchDetectorOpenCVNodeDataBase
{
    public MatchTemplate()
    {
        this.TemplateFilePath = this.GetDataPath(ImagePath.Circle);
    }

    private TemplateMatchModes _templateMatchModes = TemplateMatchModes.CCoeffNormed;
    [Display(Name = "匹配类型", GroupName = "数据")]
    public TemplateMatchModes TemplateMatchModes
    {
        get { return _templateMatchModes; }
        set
        {
            _templateMatchModes = value;
            RaisePropertyChanged();
        }
    }


    protected override FlowableResult<Mat> Invoke(ISrcImageNodeData srcImageNodeData, IOpenCVNodeData from, IFlowableDiagramData diagram)
    {
        // 加载源图像和模板图像
        var src = from.Mat;
        using (Mat template = Cv2.ImRead(this.TemplateFilePath, ImreadModes.Color))
        {
            using Mat result = new Mat();
            // 获取模板图像的尺寸
            int resultCols = src.Cols - template.Cols + 1;
            int resultRows = src.Rows - template.Rows + 1;
            result.Create(resultRows, resultCols, MatType.CV_32FC1);
            // 进行模板匹配
            Cv2.MatchTemplate(src, template, result, TemplateMatchModes.CCoeffNormed);
            // 归一化结果
            Cv2.Normalize(result, result, 0, 1, NormTypes.MinMax, -1);
            // 找到最佳匹配位置
            Cv2.MinMaxLoc(result, out _, out double maxVal, out _, out Point maxLoc);
            // 设置匹配阈值
            double threshold = 0.8;
            if (maxVal >= threshold)
            {
                Mat view = this.GetPrviewMat(srcImageNodeData, from, result);
                // 在源图像上绘制矩形框
                Cv2.Rectangle(view, maxLoc, new Point(maxLoc.X + template.Cols, maxLoc.Y + template.Rows), Scalar.Red, 2);
                return this.OK(view);
            }
            else
            {
                return Error(null, "没有匹配到模板");
            }
        }
  
    }
}

