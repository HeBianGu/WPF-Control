using H.Components.VisionDiagrams.OpenCV.Extensions;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace H.Components.VisionDiagrams.OpenCV.Base.Calibration;

public class PixelWorldPoints : ObservableCollection<PixelWorldPointKeyValuePair>
{
    public PixelWorldPoints()
    {

    }

    public PixelWorldPoints(IEnumerable<PixelWorldPointKeyValuePair> collection) : base(collection)
    {
    }


    public WpfPoint GetRotCenterImagePoint()
    {
        double sumX = 0;
        double sumY = 0;
        int count = this.Count;
        foreach (var point in this)
        {
            sumX += point.PixelPointX;
            sumY += point.PixelPointY;
        }
        return new WpfPoint(sumX / count, sumY / count);
    }

    public WpfPoint GetRotCenterWorldPoint()
    {
        double sumX = 0;
        double sumY = 0;
        int count = this.Count;
        foreach (var point in this)
        {
            sumX += point.WorldPointX;
            sumY += point.WorldPointY;
        }
        return new WpfPoint(sumX / count, sumY / count);
    }

    public (Homography homography, string message) FindHomography(double ransacThreshold = 3)
    {
        Point2d[] source = this.Select(x => new Point2d(x.PixelPointX, x.PixelPointY)).ToArray();
        Point2d[] target = this.Select(x => new Point2d(x.WorldPointX, x.WorldPointY)).ToArray();
        Mat homography = Cv2.FindHomography(source, target, HomographyMethods.Ransac, ransacThreshold);
        if (homography.Empty())
            return (null, "±Í∂®æÿ’Ûº∆À„ ß∞‹");
        return (new Homography(homography), null);
    }
}
