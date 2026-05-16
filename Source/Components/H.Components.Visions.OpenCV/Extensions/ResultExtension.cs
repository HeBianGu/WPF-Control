// Copyright (c) HeBianGu Authors. All Rights Reserved. 
// Author: HeBianGu 
// Github: https://github.com/HeBianGu/WPF-Control 
// Document: https://hebiangu.github.io/WPF-Control-Docs  
// QQ:908293466 Group:971261058 
// bilibili: https://space.bilibili.com/370266611 
// Licensed under the MIT License (the "License")

using H.Components.Vision.Presenters;

namespace H.Components.Visions.OpenCV.Extensions;

public static class ResultExtension
{

    public static IResultPresenter ToResultPresenter(this IEnumerable<KeyPoint> keyPoints)
    {
        return keyPoints.ToRectangleDataGridResultPresenter(x => x.ToWindowRect(), x => $"C[{x.ClassId.ToString()}],O[{x.Octave.ToString()}],R[{x.Response.ToString()}]");
    }
    public static IResultPresenter ToResultPresenter(this IEnumerable<OpenCvSharp.Rect> rects, string name = "位置信息")
    {
        return rects.ToRectangleDataGridResultPresenter(x => x.ToWindowRect(), x => name);
    }

    public static IResultPresenter ToResultPresenter(this IEnumerable<Point2f> points, string name = "位置信息")
    {
        return points.ToWindowRect().ToEnumerable().ToResultPresenter(x => name);
    }
    public static IResultPresenter ToResultPresenter(this IEnumerable<OpenCvSharp.Point> points, string name = "位置信息")
    {
        return points.ToWindowRect().ToEnumerable().ToResultPresenter(x => name);
    }
    public static IResultPresenter ToResultPresenter(this OpenCvSharp.Rect rect, string name = "位置信息")
    {
        return rect.ToWindowRect().ToEnumerable().ToResultPresenter(x => name);
    }

    public static IResultPresenter ToResultPresenter(this IEnumerable<ConnectedComponents.Blob> blobs)
    {
        return blobs.ToRectangleDataGridResultPresenter(x => x.Rect.ToWindowRect(), x => "标记" + x.Label.ToString());
    }

    public static IResultPresenter ToResultPresenter(this IEnumerable<LineSegmentPoint> lineSegmentPoints, string name = "直线数据")
    {
        return lineSegmentPoints.ToLineDataGridResultPresenter(x => new VisionLine() { Start = x.P1.ToPoint(), End = x.P2.ToPoint() }, x => name);
    }

    public static IEnumerable<IVisionResultImage<IMatImage>> ToResultImages(this IEnumerable<OpenCvSharp.Rect> rects, Mat fromImage)
    {
        foreach (var item in rects)
        {
            var result = item.ToResultImage(fromImage);
            if (result != null)
                yield return result;
        }
    }

    public static IVisionResultImage<IMatImage> ToResultImage(this OpenCvSharp.Rect rect, Mat fromImage)
    {
        if (fromImage.IsValid() == false)
            return null;
        if (!fromImage.IsRoiInRange(rect))
            return null;
        Mat mat = new Mat(fromImage, rect);
        return new VisionResultImage<IMatImage>() { Image = new MatImage(mat), Name = "Hello World" };
    }

    public static IEnumerable<IVisionResultImage<IMatImage>> ToResultImages(this IEnumerable<RotatedRect> rects, Mat fromImage)
    {
        foreach (var item in rects)
        {
          var result = item.ToResultImage(fromImage);
          if (result != null)
              yield return result;
        }
    }

    public static IVisionResultImage<IMatImage> ToResultImage(this OpenCvSharp.RotatedRect rect, Mat fromImage)
    {
        if (fromImage.IsValid() == false)
            return null;
        if (!fromImage.IsRotatedRectInRange(rect))
            return null;
        Mat mat = fromImage.GetRotatedRectImage(rect);
        return new VisionResultImage<IMatImage>() { Image = new MatImage(mat), Name = "Hello World" };
    }

    private static bool IsRotatedRectInRange(this Mat src, RotatedRect rect)
    {
        if (!src.IsValid())
            return false;
        if (rect.Size.Width <= 0 || rect.Size.Height <= 0)
            return false;

        return rect.Points().All(x => x.X >= 0 && x.Y >= 0 && x.X <= src.Width && x.Y <= src.Height);
    }

    private static Mat GetRotatedRectImage(this Mat src, RotatedRect rect)
    {
        Point2f[] srcPoints = rect.GetOrderedPoints();
        double width1 = PointDistance(srcPoints[0], srcPoints[1]);
        double width2 = PointDistance(srcPoints[3], srcPoints[2]);
        double height1 = PointDistance(srcPoints[0], srcPoints[3]);
        double height2 = PointDistance(srcPoints[1], srcPoints[2]);
        int width = Math.Max(1, (int)Math.Ceiling(Math.Max(width1, width2)));
        int height = Math.Max(1, (int)Math.Ceiling(Math.Max(height1, height2)));
        Point2f[] dstPoints =
        {
            new(0, 0),
            new(width - 1, 0),
            new(width - 1, height - 1),
            new(0, height - 1)
        };

        using Mat matrix = Cv2.GetPerspectiveTransform(srcPoints, dstPoints);
        Mat result = new Mat();
        Cv2.WarpPerspective(src, result, matrix, new OpenCvSharp.Size(width, height));
        result = result.RotateLongSideToHorizontal();
        result.FlipByForegroundCentroid();
        return result;
    }

    private static Mat RotateLongSideToHorizontal(this Mat src)
    {
        if (src.Width >= src.Height)
            return src;

        Mat result = new Mat();
        Cv2.Rotate(src, result, RotateFlags.Rotate90Clockwise);
        src.Dispose();
        return result;
    }

    private static void FlipByForegroundCentroid(this Mat src)
    {
        using Mat mask = src.ToForegroundMask();
        Moments moments = Cv2.Moments(mask, true);
        if (Math.Abs(moments.M00) < double.Epsilon)
            return;

        double centroidX = moments.M10 / moments.M00;
        double centroidY = moments.M01 / moments.M00;
        double centerX = (src.Width - 1) / 2.0;
        double centerY = (src.Height - 1) / 2.0;

        if (centroidX > centerX)
            Cv2.Flip(src, src, FlipMode.Y);
        if (centroidY > centerY)
            Cv2.Flip(src, src, FlipMode.X);
    }

    private static Mat ToForegroundMask(this Mat src)
    {
        Mat gray = new Mat();
        if (src.Channels() > 1)
            Cv2.CvtColor(src, gray, ColorConversionCodes.BGR2GRAY);
        else
            src.CopyTo(gray);

        Mat mask = new Mat();
        Cv2.Threshold(gray, mask, 0, 255, ThresholdTypes.Binary);
        gray.Dispose();
        return mask;
    }

    private static Point2f[] GetOrderedPoints(this RotatedRect rect)
    {
        Point2f[] points = rect.Points()
            .OrderBy(x => Math.Atan2(x.Y - rect.Center.Y, x.X - rect.Center.X))
            .ToArray();
        int topLeftIndex = 0;
        float minValue = points[0].X + points[0].Y;
        for (int i = 1; i < points.Length; i++)
        {
            float value = points[i].X + points[i].Y;
            if (value >= minValue)
                continue;

            minValue = value;
            topLeftIndex = i;
        }

        return Enumerable.Range(0, points.Length)
            .Select(x => points[(topLeftIndex + x) % points.Length])
            .ToArray();
    }

    private static double PointDistance(Point2f p1, Point2f p2)
    {
        double x = p1.X - p2.X;
        double y = p1.Y - p2.Y;
        return Math.Sqrt(x * x + y * y);
    }
}

