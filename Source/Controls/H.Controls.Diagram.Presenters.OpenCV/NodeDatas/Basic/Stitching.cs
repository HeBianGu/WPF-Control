namespace H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Basic;
[Display(Name = "图像拼接", GroupName = "基础函数", Description = "降噪成黑白色", Order = 71)]
public class Stitching : BasicActionNodeDataBase
{
    private Stitcher.Mode _mode = Stitcher.Mode.Scans;
    [DefaultValue(Stitcher.Mode.Scans)]
    [Display(Name = "Mode", GroupName = "数据")]
    public Stitcher.Mode Mode
    {
        get { return _mode; }
        set
        {
            _mode = value;
            DispatcherRaisePropertyChanged();
        }
    }

    //public override IFlowableResult Invoke(Part previors, Node current)
    //{
    //    string path = this.GetFromFilePath(current);
    //    Mat[] images = SelectStitchingImages(200, 200, 10, path);
    //    using var stitcher = Stitcher.Create(this.Mode);
    //    using var pano = new Mat();
    //    var status = stitcher.Stitch(images, pano);
    //    this.Mat = pano;
    //    this.RefreshMatToView();
    //    foreach (var image in images)
    //    {
    //        image.Dispose();
    //    }
    //    return base.Invoke(previors, current);
    //}

    protected override IFlowableResult Invoke()
    {
        string path = this.SrcFilePath;
        Mat[] images = SelectStitchingImages(200, 200, 10, path);
        using Stitcher stitcher = Stitcher.Create(this.Mode);
        using Mat pano = new Mat();
        Stitcher.Status status = stitcher.Stitch(images, pano);
        this.Mat = pano;
        this.RefreshMatToView();
        foreach (Mat image in images)
        {
            image.Dispose();
        }
        return base.Invoke();
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
