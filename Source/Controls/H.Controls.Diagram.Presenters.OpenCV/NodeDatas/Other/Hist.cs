﻿namespace H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Other;
[Display(Name = "直方图均计算", GroupName = "其他", Order = 0)]
public class Hist : OtherOpenCVNodeDataBase
{
    protected override FlowableResult<Mat> Invoke(ISrcImageNodeData srcImageNodeData, IOpenCVNodeData from, IFlowableDiagramData diagram)
    {
        using Mat src = Cv2.ImRead(srcImageNodeData.SrcFilePath, ImreadModes.Grayscale);

        // Histogram view
        const int Width = 260, Height = 200;
        using Mat render = new Mat(new Size(Width, Height), MatType.CV_8UC3, Scalar.All(255));

        // Calculate histogram
        Mat hist = new Mat();
        int[] hdims = { 256 }; // Histogram size for each dimension
        Rangef[] ranges = { new Rangef(0, 256), }; // min/max 
        Cv2.CalcHist(
            new Mat[] { src },
            new int[] { 0 },
            null,
            hist,
            1,
            hdims,
            ranges);

        // Get the max value of histogram
        Cv2.MinMaxLoc(hist, out _, out double maxVal);

        Scalar color = Scalar.All(100);
        // Scales and draws histogram
        hist = hist * (maxVal != 0 ? Height / maxVal : 0.0);
        for (int j = 0; j < hdims[0]; ++j)
        {
            int binW = (int)((double)Width / hdims[0]);
            render.Rectangle(
                new Point(j * binW, render.Rows - (int)hist.Get<float>(j)),
                new Point((j + 1) * binW, render.Rows),
                color,
                -1);
        }
        return this.OK(render);
    }
}
