
namespace H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Basic;
[Display(Name = "图像拼接", GroupName = "基础函数", Description = "将多张重叠的图像拼接成一张更大、更完整的图像", Order = 71)]
public class Stitching : BasicOpenCVNodeDataBase
{
    private Stitcher.Mode _mode = Stitcher.Mode.Scans;
    [DefaultValue(Stitcher.Mode.Scans)]
    [Display(Name = "拼接模式", GroupName = "数据", Description = "参数用于指定图像拼接的模式或策略")]
    public Stitcher.Mode Mode
    {
        get { return _mode; }
        set
        {
            _mode = value;
            RaisePropertyChanged();
        }
    }

    protected override FlowableResult<Mat> Invoke(ISrcImageNodeData srcImageNodeData, IOpenCVNodeData from, IFlowableDiagramData diagram)
    {
        string path = srcImageNodeData.SrcFilePath;
        Mat[] images = SelectStitchingImages(200, 200, 10, path);
        using Stitcher stitcher = Stitcher.Create(this.Mode);
        using Mat pano = new Mat();
        Stitcher.Status status = stitcher.Stitch(images, pano);
        foreach (Mat image in images)
        {
            image.Dispose();
        }
        return this.OK(pano);
    }

    private Mat[] SelectStitchingImages(int width, int height, int count, string path)
    {
        using Mat source = new Mat(path, ImreadModes.Color);
        using Mat result = source.Clone();

        Random rand = new Random();
        List<Mat> mats = new List<Mat>();
        for (int i = 0; i < count; i++)
        {
            int x1 = rand.Next(source.Cols - width);
            int y1 = rand.Next(source.Rows - height);
            int x2 = x1 + width;
            int y2 = y1 + height;

            result.Line(new Point(x1, y1), new Point(x1, y2), new Scalar(0, 0, 255));
            result.Line(new Point(x1, y2), new Point(x2, y2), new Scalar(0, 0, 255));
            result.Line(new Point(x2, y2), new Point(x2, y1), new Scalar(0, 0, 255));
            result.Line(new Point(x2, y1), new Point(x1, y1), new Scalar(0, 0, 255));

            using Mat m = source[new Rect(x1, y1, width, height)];
            mats.Add(m.Clone());
        }

        return mats.ToArray();
    }
}
