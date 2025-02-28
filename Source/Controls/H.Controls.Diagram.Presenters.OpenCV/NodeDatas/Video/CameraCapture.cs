using System.Threading.Tasks;

namespace H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Video;
[Display(Name = "摄像头", GroupName = "数据源", Description = "降噪成黑白色", Order = 0)]
public class CameraCapture : StartNodeDataBase
{
    protected override ImageSource CreateImageSource()
    {
        return null;
    }
    protected override string GetImagePath()
    {
        return null;
    }

    private int _sleepMilliseconds = 30;
    [Display(Name = "SleepMilliseconds", GroupName = "数据")]
    public int SleepMilliseconds
    {
        get { return _sleepMilliseconds; }
        set
        {
            _sleepMilliseconds = value;
            RaisePropertyChanged();
        }
    }

    //public override IFlowableResult Invoke(Part previors, Node current)
    //{
    //    // Opens MP4 file (ffmpeg is probably needed)
    //    using var capture = new OpenCvSharp.VideoCapture();
    //    string haarXml = this.GetDataPath(TextPath.HaarCascade);
    //    CascadeClassifier cascadeClassifier = new CascadeClassifier(haarXml);
    //    capture.Open(0, VideoCaptureAPIs.ANY);
    //    if (!capture.IsOpened())
    //        return this.Error("摄像头打开失败");

    //    int sleepTime = (int)Math.Round(this.SleepMilliseconds / capture.Fps);
    //    // Frame image buffer
    //    //var image = new Mat();
    //    // When the movie playback reaches end, Mat.data becomes NULL.
    //    while (true)
    //    {
    //        if (this.State == FlowableState.Canceling)
    //            return this.Error("用户取消");
    //        using (var frameMat = capture.RetrieveMat())
    //        {
    //            var rects = cascadeClassifier.DetectMultiScale(frameMat, 1.1, 5, HaarDetectionTypes.ScaleImage, new OpenCvSharp.Size(30, 30));

    //            foreach (var rect in rects)
    //            {
    //                Cv2.Rectangle(frameMat, rect, Scalar.Red);
    //            }

    //            this.RefreshMatToView(frameMat);
    //        }

    //        Thread.Sleep(this.SleepMilliseconds);
    //    }

    //    //return base.Invoke(previors, current);
    //}


    public override async Task<IFlowableResult> InvokeAsync(Part previors, Node current)
    {
        using BackgroundSubtractorMOG mog = BackgroundSubtractorMOG.Create();

        return await Task.Run(async () =>
        {
            // Opens MP4 file (ffmpeg is probably needed)
            using OpenCvSharp.VideoCapture capture = new OpenCvSharp.VideoCapture();
            string haarXml = this.GetDataPath(TextPath.HaarCascade);
            //CascadeClassifier cascadeClassifier = new CascadeClassifier(haarXml);
            capture.Open(0, VideoCaptureAPIs.ANY);
            if (!capture.IsOpened())
                return this.Error("摄像头打开失败");

            int sleepTime = (int)Math.Round(this.SleepMilliseconds / capture.Fps);
            int index = 0;

            IEnumerable<IVideoFlowable> videos = current.GetAllParts().Select(x => x.GetContent<IFlowable>()).OfType<IVideoFlowable>();
            foreach (IVideoFlowable video in videos)
            {
                video.Begin();
            }

            while (true)
            {
                if (this.State == FlowableState.Canceling)
                    return this.Error("用户取消");
                using (Mat frameMat = capture.RetrieveMat())
                {
                    //var rects = cascadeClassifier.DetectMultiScale(frameMat, 1.1, 5, HaarDetectionTypes.ScaleImage, new OpenCvSharp.Size(30, 30));

                    //foreach (var rect in rects)
                    //{
                    //    Cv2.Rectangle(frameMat, rect, Scalar.Red);
                    //}

                    //this.RefreshMatToView(frameMat);
                    this.Message = $"{index++}/{capture.FrameCount}";
                    this.Mat = frameMat;
                    this.SrcMat = frameMat;
                    //this.Mat = image.Clone().CvtColor(ColorConversionCodes.BGR2GRAY, 0).Threshold(0, 255, ThresholdTypes.Otsu | ThresholdTypes.Binary);
                    RefreshMatToView();
                    if (this.State == FlowableState.Canceling)
                        break;

                    Node to = current.GetToNodes().FirstOrDefault();
                    if (to != null)
                    {
                        await to.InvokeNode(x =>
                        {
                            OpenCVNodeDataBase data = x.GetContent<OpenCVNodeDataBase>();
                            data.UseInfoLogger = false;
                            data.UseReview = false;
                            data.UseAnimation = false;
                        });
                    }

                }

                //Thread.Sleep(this.SleepMilliseconds);

                //this.Message = $"{index++}/{capture.FrameCount}";
                //Mat = image;
                ////this.Mat = image.Clone().CvtColor(ColorConversionCodes.BGR2GRAY, 0).Threshold(0, 255, ThresholdTypes.Otsu | ThresholdTypes.Binary);
                //RefreshMatToView();
                //if (this.State == FlowableState.Canceling)
                //    break;

                //var to = current.GetToNodes().FirstOrDefault();
                //if (to != null)
                //{
                //    await to.StartNode(x =>
                //    {
                //        var data = x.GetContent<OpenCVNodeDataBase>();
                //        data.UseInfoLogger = false;
                //        data.UseReview = false;
                //        data.UseAnimation = false;
                //    });
                //}

                Cv2.WaitKey(sleepTime);

            }

            foreach (IVideoFlowable video in videos)
            {
                video.End();
            }
            return this.OK("运行成功");
        });
    }
}
