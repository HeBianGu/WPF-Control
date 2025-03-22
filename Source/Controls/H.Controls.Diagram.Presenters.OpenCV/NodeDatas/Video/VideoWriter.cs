namespace H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Video;
[Display(Name = "视频写入", GroupName = "视频操作", Description = "降噪成黑白色", Order = 0)]
public class VideoWriter : SrcImageNodeDataBase
{
    public VideoWriter()
    {
        this.SrcFilePath = GetDataPath(MoviePath.Bach);
    }

    private string _outVideoFile = "out.avi";
    [Display(Name = "OutVideoFile", GroupName = "数据")]
    public string OutVideoFile
    {
        get { return _outVideoFile; }
        set
        {
            _outVideoFile = value;
            RaisePropertyChanged();
        }
    }

    private Size _frameSize = new Size(640, 480);
    [Display(Name = "FrameSize", GroupName = "数据")]
    public Size FrameSize
    {
        get { return _frameSize; }
        set
        {
            _frameSize = value;
            RaisePropertyChanged();
        }
    }

    private bool _isColor = true;
    [Display(Name = "IsColor", GroupName = "数据")]
    public bool IsColor
    {
        get { return _isColor; }
        set
        {
            _isColor = value;
            RaisePropertyChanged();
        }
    }

    private int _sleepMilliseconds = 1000;
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
    protected override FlowableResult<Mat> Invoke(ISrcImageNodeData srcImageNodeData, IOpenCVNodeData from, IFlowableDiagramData diagram)
    {
        //const string OutVideoFile = "out.avi";

        // Opens MP4 file (ffmpeg is probably needed)
        using OpenCvSharp.VideoCapture capture = new OpenCvSharp.VideoCapture(this.SrcFilePath);

        // Read movie frames and write them to VideoWriter 
        //var dsize = new Size(640, 480);
        using (OpenCvSharp.VideoWriter writer = new OpenCvSharp.VideoWriter(OutVideoFile, -1, capture.Fps, this.FrameSize, this.IsColor))
        {
            //Console.WriteLine("Converting each movie frames...");
            using Mat frame = new Mat();
            while (true)
            {
                // Read image
                capture.Read(frame);
                if (frame.Empty())
                    break;

                //Console.CursorLeft = 0;
                //Console.Write("{0} / {1}", capture.PosFrames, capture.FrameCount);

                this.Message = $"导出进度：{capture.PosFrames} / {capture.FrameCount}";
                // grayscale -> canny -> resize
                using Mat gray = new Mat();
                using Mat canny = new Mat();
                using Mat dst = new Mat();
                Cv2.CvtColor(frame, gray, ColorConversionCodes.BGR2GRAY);
                Cv2.Canny(gray, canny, 100, 180);
                Cv2.Resize(canny, dst, this.FrameSize, 0, 0, InterpolationFlags.Linear);
                this.Mat = dst;
                UpdateMatToView();
                // Write mat to VideoWriter
                writer.Write(dst);
            }
            //Console.WriteLine();
        }
        return this.OK(this.Mat);
    }
}
