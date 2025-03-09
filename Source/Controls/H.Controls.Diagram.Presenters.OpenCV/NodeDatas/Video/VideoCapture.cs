using H.Controls.Diagram.Flowables;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Video;
public interface IVideoFlowable
{
    void Begin();
    void End();
}

[Display(Name = "视频读取", GroupName = "数据源", Description = "降噪成黑白色", Order = 0)]
public class VideoCapture : VideoCaptureImageImportNodeDataBase
{
    public VideoCapture()
    {
        this.UseAnimation = true;
        this.SrcFilePath = GetDataPath(MoviePath.Bach);
    }
    //protected override ImageSource CreateImageSource()
    //{
    //    this.SrcFilePath = GetDataPath(this.GetImagePath());
    //    return null;
    //}

    //protected virtual ImageSource CreateImage(string path)
    //{
    //    using OpenCvSharp.VideoCapture capture = new OpenCvSharp.VideoCapture(this.SrcFilePath);
    //    if (!capture.IsOpened())
    //        return null;
    //    Mat image = new Mat();
    //    capture.Read(image); // same as cvQueryFrame
    //    return image.Empty() ? null : (ImageSource)image.ToBitmapSource();
    //}

    private int _sleepMilliseconds = 1000;
    [Display(Name = "等待延迟", GroupName = "数据")]
    public int SleepMilliseconds
    {
        get { return _sleepMilliseconds; }
        set
        {
            _sleepMilliseconds = value;
            RaisePropertyChanged();
        }
    }


    private int _startFrame = 0;
    [DefaultValue(0)]
    [Display(Name = "采样帧间隔", GroupName = "数据")]
    public int StartFrame
    {
        get { return _startFrame; }
        set
        {
            _startFrame = value;
            RaisePropertyChanged();
        }
    }


    private int _endFrame = int.MaxValue;
    [DefaultValue(int.MaxValue)]
    [Display(Name = "采样帧间隔", GroupName = "数据")]
    public int EndFrame
    {
        get { return _endFrame; }
        set
        {
            _endFrame = value;
            RaisePropertyChanged();
        }
    }

    private int _spanFrame = 60;
    [DefaultValue(60)]
    [Display(Name = "采样帧间隔", GroupName = "数据")]
    public int SpanFrame
    {
        get { return _spanFrame; }
        set
        {
            _spanFrame = value;
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
        var diagram = current.GetDiagram();
        var invokeable = diagram.InvokeDispatcher(x => x.DataContext) as IPartInvokeable;

        Action<Part> invoking = x =>
        {
            //OpenCVNodeDataBase data = x.GetContent<OpenCVNodeDataBase>();
            //data.UseInfoLogger = false;
            //data.UseReview = false;
            //data.UseAnimation = false;
            //current.Dispatcher.Invoke(() =>
            //{
            //    invokeable?.OnInvokingPart(x);
            //});
        };

        Action<Part> invoked = x =>
        {
            current.Dispatcher.Invoke(() =>
            {
                invokeable?.OnInvokedPart(x);
            });
            //Thread.Sleep(1000);
        };
        return await Task.Run(async () =>
        {
            if (this.State == FlowableState.Canceling)
                return this.Error("用户取消");
            // Opens MP4 file (ffmpeg is probably needed)
            using OpenCvSharp.VideoCapture capture = new OpenCvSharp.VideoCapture(this.SrcFilePath);

            if (File.Exists(this.SrcFilePath) == false)
                return this.Error("视频文件不存在");
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
                if (this.State == FlowableState.Canceling)
                    return this.Error("用户取消");
                invoking.Invoke(current);

                capture.Read(image); // same as cvQueryFrame
                if (image.Empty())
                    break;
                index++;
                if (index < this.StartFrame)
                    continue;
                if (index > this.EndFrame)
                    continue;
                if (index % this.SpanFrame != 0)
                    continue;
                this.Message = $"{index}/{capture.FrameCount}";
                this.Mat = image;
                this.SrcMat = this.Mat;
                //this.Mat = image.Clone().CvtColor(ColorConversionCodes.BGR2GRAY, 0).Threshold(0, 255, ThresholdTypes.Otsu | ThresholdTypes.Binary);
                RefreshMatToView();
                invoked.Invoke(current);
                var tos = current.GetToNodes();
                Node to = tos.FirstOrDefault();
                if (to != null)
                {
                    await to.Dispatcher.InvokeAsync(() =>
                      {
                          tos.Wait();
                      });
                    //await to.Dispatcher.InvokeAsync(async () =>
                    //    {
                    await to.InvokeNode(invoking, invoked);
                    await Task.Delay(1000);
                    //});
                    //x =>
                    //{
                    //    OpenCVNodeDataBase data = x.GetContent<OpenCVNodeDataBase>();
                    //    data.UseInfoLogger = false;
                    //    data.UseReview = false;
                    //    data.UseAnimation = false;
                    //}, 
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
