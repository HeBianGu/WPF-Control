using System.Threading.Tasks;
using System.Windows.Threading;

namespace H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Video;
[Display(Name = "摄像头", GroupName = "数据源", Description = "降噪成黑白色", Order = 0)]
public class CameraCapture : VideoCaptureImageImportNodeDataBase
{
    private VideoCaptureAPIs _videoCaptureAPIs = VideoCaptureAPIs.ANY;
    [Display(Name = "摄像头API", GroupName = "数据")]
    public VideoCaptureAPIs VideoCaptureAPIs
    {
        get { return _videoCaptureAPIs; }
        set
        {
            _videoCaptureAPIs = value;
            RaisePropertyChanged();
        }
    }

    private int _VideoCaptureIndex;
    [Display(Name = "摄像头序号", GroupName = "数据")]
    public int VideoCaptureIndex
    {
        get { return _VideoCaptureIndex; }
        set
        {
            _VideoCaptureIndex = value;
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

    //            this.UpdateMatToView(frameMat);
    //        }

    //        Thread.Sleep(this.SleepMilliseconds);
    //    }

    //    //return base.Invoke(previors, current);
    //}

    protected override async Task<IFlowableResult> BeforeInvokeAsync(Part previors, Node current)
    {
        return await Task.FromResult(this.OK());
    }

    public override async Task<IFlowableResult> InvokeAsync(Part previors, Node current)
    {
        //using BackgroundSubtractorMOG mog = BackgroundSubtractorMOG.Create();
        return await Task.Run(async () =>
        {
            // Opens MP4 file (ffmpeg is probably needed)
            using var capture = new OpenCvSharp.VideoCapture();
            //string haarXml = this.GetDataPath(TextPath.HaarCascade);
            //CascadeClassifier cascadeClassifier = new CascadeClassifier(haarXml);
            capture.Open(this.VideoCaptureIndex, this.VideoCaptureAPIs);
            if (!capture.IsOpened())
                return this.Error("摄像头打开失败");
            int sleepTime = (int)Math.Round(this.SleepMilliseconds / capture.Fps);
            return await this.InvokeVideoFlowable(current, async () =>
            {
                int index = 0;
                while (true)
                {
                    if (this.State == FlowableState.Canceling)
                        return this.Error("用户取消");
                    using Mat frameMat = capture.RetrieveMat();

                    //var rects = cascadeClassifier.DetectMultiScale(frameMat, 1.1, 5, HaarDetectionTypes.ScaleImage, new OpenCvSharp.Size(30, 30));

                    //foreach (var rect in rects)
                    //{
                    //    Cv2.Rectangle(frameMat, rect, Scalar.Red);
                    //}

                    //this.UpdateMatToView(frameMat);
                    this.Message = $"{index++}/{capture.FrameCount}";
                    var r = await this.InvokeFrameMatAsync(previors, current, frameMat);
                    if (r == null)
                        return this.Error("用户取消");
                    else if (r == false)
                        return this.Error();

                    //Thread.Sleep(this.SleepMilliseconds);

                    //this.Message = $"{index++}/{capture.FrameCount}";
                    //Mat = image;
                    ////this.Mat = image.Clone().CvtColor(ColorConversionCodes.BGR2GRAY, 0).Threshold(0, 255, ThresholdTypes.Otsu | ThresholdTypes.Binary);
                    //UpdateMatToView();
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
            });

        });
    }
}
