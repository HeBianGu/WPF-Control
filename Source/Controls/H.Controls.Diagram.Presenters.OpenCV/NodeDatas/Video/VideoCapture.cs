using System.Threading.Tasks;

namespace H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Video;
public interface IVideoFlowable
{
    void Begin();
    void End();
}


[Display(Name = "视频读取", GroupName = "数据源", Description = "降噪成黑白色", Order = 0)]
public class VideoCapture : StartNodeDataBase
{
    public VideoCapture()
    {
        this.UseAnimation = true;
        this.SrcFilePath = GetDataPath(this.GetImagePath());
    }
    //protected override ImageSource CreateImageSource()
    //{
    //    this.SrcFilePath = GetDataPath(this.GetImagePath());
    //    return null;
    //}
    protected override string GetImagePath()
    {
        return MoviePath.Bach;
    }

    protected virtual ImageSource CreateImage(string path)
    {
        using OpenCvSharp.VideoCapture capture = new OpenCvSharp.VideoCapture(this.SrcFilePath);
        if (!capture.IsOpened())
            return null;
        Mat image = new Mat();
        capture.Read(image); // same as cvQueryFrame
        return image.Empty() ? null : (ImageSource)image.ToBitmapSource();
    }

    private int _sleepMilliseconds = 1000;
    [Display(Name = "每帧延迟", GroupName = "数据")]
    public int SleepMilliseconds
    {
        get { return _sleepMilliseconds; }
        set
        {
            _sleepMilliseconds = value;
            RaisePropertyChanged();
        }
    }
    //protected override string GetFilter()
    //{
    //    return "视频文件|*.asf;*.wav;*.mp4;*.mpg;*wmv;mtv";
    //}

    public override async Task<IFlowableResult> InvokeAsync(Part previors, Node current)
    {
        using BackgroundSubtractorMOG mog = BackgroundSubtractorMOG.Create();

        return await Task.Run(async () =>
        {
            // Opens MP4 file (ffmpeg is probably needed)
            using OpenCvSharp.VideoCapture capture = new OpenCvSharp.VideoCapture(this.SrcFilePath);
            if (!capture.IsOpened())
                return this.Error("视频打开失败");

            IEnumerable<IVideoFlowable> videos = current.GetAllParts().Select(x => x.GetContent<IFlowable>()).OfType<IVideoFlowable>();
            foreach (IVideoFlowable video in videos)
            {
                video.Begin();
            }

            int sleepTime = (int)Math.Round(this.SleepMilliseconds / capture.Fps);
            // Frame image buffer
            Mat image = new Mat();
            // When the movie playback reaches end, Mat.data becomes NULL.
            int index = 0;
            while (true)
            {
                capture.Read(image); // same as cvQueryFrame
                if (image.Empty())
                    break;

                this.Message = $"{index++}/{capture.FrameCount}";
                this.Mat = image;
                this.SrcMat = this.Mat;
                //this.Mat = image.Clone().CvtColor(ColorConversionCodes.BGR2GRAY, 0).Threshold(0, 255, ThresholdTypes.Otsu | ThresholdTypes.Binary);
                RefreshMatToView();
                if (this.State == FlowableState.Canceling)
                    return this.Error("用户取消");

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
