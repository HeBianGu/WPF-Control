namespace H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Detector;
[Display(Name = "识别匹配图片", GroupName = "基础检测", Order = 30)]
public class BestMatch : MatchDetectorOpenCVNodeDataBase
{
    public BestMatch()
    {
        this.TemplateFilePath = GetDataPath(ImagePath.Match1);
    }
    //protected override ImageSource CreateImageSource()
    //{
    //    this.SrcFilePath = GetDataPath(ImagePath.Match1);
    //    return CreateImage(this.SrcFilePath);
    //}

    protected override FlowableResult<Mat> Invoke(ISrcImageNodeData srcImageNodeData, IOpenCVNodeData from, IFlowableDiagramData diagram)
    {
        Mat img2 = from.Mat;
        using Mat img1 = new Mat(GetDataPath(ImagePath.Match1), ImreadModes.Color);
        using ORB orb = ORB.Create(1000);
        using Mat descriptors1 = new Mat();
        using Mat descriptors2 = new Mat();
        orb.DetectAndCompute(img1, null, out KeyPoint[] keyPoints1, descriptors1);
        orb.DetectAndCompute(img2, null, out KeyPoint[] keyPoints2, descriptors2);

        using BFMatcher bf = new BFMatcher(NormTypes.Hamming, crossCheck: true);
        DMatch[] matches = bf.Match(descriptors1, descriptors2);

        DMatch[] goodMatches = matches
            .OrderBy(x => x.Distance)
            .Take(10)
            .ToArray();

        IEnumerable<Point2d> srcPts = goodMatches.Select(m => keyPoints1[m.QueryIdx].Pt).Select(p => new Point2d(p.X, p.Y));
        IEnumerable<Point2d> dstPts = goodMatches.Select(m => keyPoints2[m.TrainIdx].Pt).Select(p => new Point2d(p.X, p.Y));

        using Mat homography = Cv2.FindHomography(srcPts, dstPts, HomographyMethods.Ransac, 5, null);

        int h = img1.Height, w = img1.Width;
        Point2d[] img2Bounds = new[]
        {
            new Point2d(0, 0),
            new Point2d(0, h-1),
            new Point2d(w-1, h-1),
            new Point2d(w-1, 0),
        };
        Point2d[] img2BoundsTransformed = Cv2.PerspectiveTransform(img2Bounds, homography);

        Mat view = img2.Clone();
        Point[] drawingPoints = img2BoundsTransformed.Select(p => (Point)p).ToArray();
        Cv2.Polylines(view, new[] { drawingPoints }, true, Scalar.Red, 5);

        //using (new Window("view", view))
        //{
        //    Cv2.WaitKey();
        //}

        //UpdateMatToView(view.Clone());
        return this.OK(view);
    }
}


