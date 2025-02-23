// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase




using OpenCvSharp;
using System.Linq;
using System.Windows.Media;

namespace HeBianGu.Diagram.OpenCV
{
    
    [Display(Name = "识别匹配图片", GroupName = "基础检测", Order = 0)]
    public class BestMatch : OpenCVNodeData
    {
        protected override ImageSource CreateImageSource()
        {
            this.FilePath = GetDataPath(ImagePath.Match1);
            return CreateImage(FilePath);
        }

        protected override IFlowableResult Refresh()
        {
            var img2 = this._preMat;
            var img1 = new Mat(GetDataPath(ImagePath.Match1), ImreadModes.Color);
            using var orb = ORB.Create(1000);
            using var descriptors1 = new Mat();
            using var descriptors2 = new Mat();
            orb.DetectAndCompute(img1, null, out var keyPoints1, descriptors1);
            orb.DetectAndCompute(img2, null, out var keyPoints2, descriptors2);

            using var bf = new BFMatcher(NormTypes.Hamming, crossCheck: true);
            var matches = bf.Match(descriptors1, descriptors2);

            var goodMatches = matches
                .OrderBy(x => x.Distance)
                .Take(10)
                .ToArray();

            var srcPts = goodMatches.Select(m => keyPoints1[m.QueryIdx].Pt).Select(p => new Point2d(p.X, p.Y));
            var dstPts = goodMatches.Select(m => keyPoints2[m.TrainIdx].Pt).Select(p => new Point2d(p.X, p.Y));

            using var homography = Cv2.FindHomography(srcPts, dstPts, HomographyMethods.Ransac, 5, null);

            int h = img1.Height, w = img1.Width;
            var img2Bounds = new[]
            {
                new Point2d(0, 0),
                new Point2d(0, h-1),
                new Point2d(w-1, h-1),
                new Point2d(w-1, 0),
            };
            var img2BoundsTransformed = Cv2.PerspectiveTransform(img2Bounds, homography);

            var view = img2.Clone();
            var drawingPoints = img2BoundsTransformed.Select(p => (Point)p).ToArray();
            Cv2.Polylines(view, new[] { drawingPoints }, true, Scalar.Red, 5);

            //using (new Window("view", view))
            //{
            //    Cv2.WaitKey();
            //}

            RefreshMatToView(view.Clone());
            return base.Refresh();
        }
    }
}
