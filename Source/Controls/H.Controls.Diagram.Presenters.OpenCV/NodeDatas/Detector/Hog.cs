namespace H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Detector;
[Display(Name = "行人检测", GroupName = "基础检测", Order = 21)]
public class Hog : DetectorOpenCVNodeDataBase
{
    protected override FlowableResult<Mat> Invoke(ISrcImageNodeData srcImageNodeData, IOpenCVNodeData from, IFlowableDiagramData diagram)
    {
        Mat result = from.Mat;
        //using Mat img = Cv2.ImRead(this.SrcFilePath, ImreadModes.Color);
        using HOGDescriptor hog = new HOGDescriptor();
        hog.SetSVMDetector(HOGDescriptor.GetDefaultPeopleDetector());
        bool b = hog.CheckDetectorSize();
        // run the detector with default parameters. to get a higher hit-rate
        // (and more false alarms, respectively), decrease the hitThreshold and
        // groupThreshold (set groupThreshold to 0 to turn off the grouping completely).
        Rect[] found = hog.DetectMultiScale(result, 0, new Size(8, 8), new Size(24, 16), 1.05, 2);
        var prev = this.GetPrviewMat(srcImageNodeData, from, result);
        foreach (Rect rect in found)
        {
            // the HOG detector returns slightly larger rectangles than the real objects.
            // so we slightly shrink the rectangles to get a nicer output.
            Rect r = new Rect
            {
                X = rect.X + (int)Math.Round(rect.Width * 0.1),
                Y = rect.Y + (int)Math.Round(rect.Height * 0.1),
                Width = (int)Math.Round(rect.Width * 0.8),
                Height = (int)Math.Round(rect.Height * 0.8)
            };
            prev.Rectangle(r.TopLeft, r.BottomRight, Scalar.Red, 3);
        }
        return this.OK(prev);
    }
}
