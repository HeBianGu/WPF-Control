using System.Threading;

namespace H.Controls.Diagram.Presenters.OpenCV.NodeDatas.ML;
[Display(Name = "支持向量机", GroupName = "机器学习", Description = "降噪成黑白色", Order = 0)]
public class SVM : OpenCVNodeDataBase
{
    private static double Function(double x)
    {
        return x + 50 * Math.Sin(x / 15.0);
    }

    protected override FlowableResult<Mat> Invoke(ISrcImageNodeData srcImageNodeData, IOpenCVNodeData from, IFlowableDiagramData diagram)
    {
        // Training data          
        Point2f[] points = new Point2f[500];
        int[] responses = new int[points.Length];
        Random rand = new Random();
        for (int i = 0; i < responses.Length; i++)
        {
            float x = rand.Next(0, 300);
            float y = rand.Next(0, 300);
            points[i] = new Point2f(x, y);
            responses[i] = y > Function(x) ? 1 : 2;
        }

        // Show training data and f(x)
        using (Mat pointsPlot = Mat.Zeros(300, 300, MatType.CV_8UC3))
        {
            for (int i = 0; i < points.Length; i++)
            {
                int x = (int)points[i].X;
                int y = (int)(300 - points[i].Y);
                int res = responses[i];
                Scalar color = res == 1 ? Scalar.Red : Scalar.GreenYellow;
                pointsPlot.Circle(x, y, 2, color, -1);
            }
            // f(x)
            for (int x = 1; x < 300; x++)
            {
                int y1 = (int)(300 - Function(x - 1));
                int y2 = (int)(300 - Function(x));
                pointsPlot.Line(x - 1, y1, x, y2, Scalar.LightBlue, 1);
            }

            this.Mat = pointsPlot;
            UpdateMatToView();
            Thread.Sleep(1000);
        }

        // Train
        Mat dataMat = new Mat(points.Length, 2, MatType.CV_32FC1, points);
        Mat resMat = new Mat(responses.Length, 1, MatType.CV_32SC1, responses);
        using OpenCvSharp.ML.SVM svm = OpenCvSharp.ML.SVM.Create();
        // normalize data
        dataMat /= 300.0;

        // SVM parameters
        svm.Type = OpenCvSharp.ML.SVM.Types.CSvc;
        svm.KernelType = OpenCvSharp.ML.SVM.KernelTypes.Rbf;
        svm.TermCriteria = TermCriteria.Both(1000, 0.000001);
        svm.Degree = 100.0;
        svm.Gamma = 100.0;
        svm.Coef0 = 1.0;
        svm.C = 1.0;
        svm.Nu = 0.5;
        svm.P = 0.1;

        svm.Train(dataMat, OpenCvSharp.ML.SampleTypes.RowSample, resMat);

        // Predict for each 300x300 pixel
        using Mat retPlot = Mat.Zeros(300, 300, MatType.CV_8UC3);
        for (int x = 0; x < 300; x++)
        {
            for (int y = 0; y < 300; y++)
            {
                float[] sample = { x / 300f, y / 300f };
                Mat sampleMat = new Mat(1, 2, MatType.CV_32FC1, sample);
                int ret = (int)svm.Predict(sampleMat);
                Rect plotRect = new Rect(x, 300 - y, 1, 1);
                if (ret == 1)
                    retPlot.Rectangle(plotRect, Scalar.Red);
                else if (ret == 2)
                    retPlot.Rectangle(plotRect, Scalar.GreenYellow);
            }
        }
        return this.OK(retPlot);
    }
}
