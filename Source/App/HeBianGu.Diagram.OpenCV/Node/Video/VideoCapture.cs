// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase




using OpenCvSharp;
using OpenCvSharp.WpfExtensions;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using System.Xml.Linq;

namespace HeBianGu.Diagram.OpenCV
{

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
        }
        protected override ImageSource CreateImageSource()
        {
            this.FilePath = GetDataPath(this.GetImagePath());
            return null;
        }
        protected override string GetImagePath()
        {
            return MoviePath.Bach;
        }

        protected override ImageSource CreateImage(string path)
        {
            using var capture = new OpenCvSharp.VideoCapture(this.FilePath);
            if (!capture.IsOpened())
                return null;
            var image = new Mat();
            capture.Read(image); // same as cvQueryFrame
            if (image.Empty())
                return null;
            return image.ToBitmapSource();
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
        protected override string GetFilter()
        {
            return "视频文件|*.asf;*.wav;*.mp4;*.mpg;*wmv;mtv";
        }

        public override async Task<IFlowableResult> InvokeAsync(Part previors, Node current)
        {
            using var mog = BackgroundSubtractorMOG.Create();

            return await Task.Run(async () =>
            {
                // Opens MP4 file (ffmpeg is probably needed)
                using var capture = new OpenCvSharp.VideoCapture(this.FilePath);
                if (!capture.IsOpened())
                    return this.Error("视频打开失败");

                var videos = current.GetAllParts().Select(x => x.GetContent<IFlowable>()).OfType<IVideoFlowable>();
                foreach (var video in videos)
                {
                    video.Begin();
                }

                int sleepTime = (int)Math.Round(this.SleepMilliseconds / capture.Fps);
                // Frame image buffer
                var image = new Mat();
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

                    var to = current.GetToNodes().FirstOrDefault();
                    if (to != null)
                    {
                        await to.StartNode(x =>
                        {
                            var data = x.GetContent<OpenCVNodeDataBase>();
                            data.UseInfoLogger = false;
                            data.UseReview = false;
                            data.UseAnimation = false;
                        });
                    }
                    Cv2.WaitKey(sleepTime);
                }

                foreach (var video in videos)
                {
                    video.End();
                }
                return this.OK("运行成功");
            });
        }
    }

}
