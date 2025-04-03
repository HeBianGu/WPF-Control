namespace H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Video;
[Display(Name = "摄像头", GroupName = "数据源", Description = "降噪成黑白色", Order = 0)]
public class CameraCapture : VideoCaptureSrcNodeDataBase
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
    protected override async Task<IFlowableResult> BeforeInvokeAsync(IFlowableLinkData previors, IFlowableDiagramData diagram)
    {
        return await Task.FromResult(this.OK());
    }

    public override async Task<IFlowableResult> InvokeAsync(IFlowableLinkData previors, IFlowableDiagramData diagram)
    {
        //using BackgroundSubtractorMOG mog = BackgroundSubtractorMOG.Create();
        return await Task.Run(async () =>
        {
            // Opens MP4 file (ffmpeg is probably needed)
            using var capture = new OpenCvSharp.VideoCapture();
            capture.Open(this.VideoCaptureIndex, this.VideoCaptureAPIs);
            if (!capture.IsOpened())
                return this.Error("摄像头打开失败");
            int sleepTime = (int)Math.Round(this.SleepMilliseconds / capture.Fps);
            return await this.InvokeVideoFlowable(diagram, async () =>
            {
                int index = 0;
                while (true)
                {
                    if (this.State == FlowableState.Canceling)
                        return this.Error("用户取消");
                    using Mat frameMat = capture.RetrieveMat();
                    this.Message = $"{index++}/{capture.FrameCount}";
                    var r = await this.InvokeFrameMatAsync(previors, diagram, frameMat);
                    if (r == null)
                        return this.Error("用户取消");
                    else if (r == false)
                        return this.Error();
                    Cv2.WaitKey(sleepTime);
                }
            });

        });
    }
}
