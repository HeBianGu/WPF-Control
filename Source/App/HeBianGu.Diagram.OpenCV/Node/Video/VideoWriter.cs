// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase




using OpenCvSharp;
using System;
using System.ComponentModel.DataAnnotations;
using System.Windows.Media;
using System.Xml.Linq;

namespace HeBianGu.Diagram.OpenCV
{
    
    [Display(Name = "视频写入", GroupName = "视频操作", Description = "降噪成黑白色", Order = 0)]
    public class VideoWriter : StartNodeDataBase
    {
        protected override ImageSource CreateImageSource()
        {
            this.FilePath = GetDataPath(this.GetImagePath());
            return null;
        }
        protected override string GetImagePath()
        {
            return MoviePath.Bach;
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

        public override IFlowableResult Invoke(Part previors, Node current)
        {
            //const string OutVideoFile = "out.avi";

            // Opens MP4 file (ffmpeg is probably needed)
            using var capture = new OpenCvSharp.VideoCapture(this.FilePath);

            // Read movie frames and write them to VideoWriter 
            //var dsize = new Size(640, 480);
            using (var writer = new OpenCvSharp.VideoWriter(OutVideoFile, -1, capture.Fps, this.FrameSize, this.IsColor))
            {
                //Console.WriteLine("Converting each movie frames...");
                using var frame = new Mat();
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
                    using var gray = new Mat();
                    using var canny = new Mat();
                    using var dst = new Mat();
                    Cv2.CvtColor(frame, gray, ColorConversionCodes.BGR2GRAY);
                    Cv2.Canny(gray, canny, 100, 180);
                    Cv2.Resize(canny, dst, this.FrameSize, 0, 0, InterpolationFlags.Linear);
                    Mat = dst;
                    RefreshMatToView();
                    // Write mat to VideoWriter
                    writer.Write(dst);
                }
                //Console.WriteLine();
            }
            //{
            //    // Watch result movie
            //    using var capture2 = new OpenCvSharp.VideoCapture(OutVideoFile);
            //    //using (var window = new Window("result"))
            //    //{
            //    //int sleepTime = (int)(1000 / capture.Fps);

            //    using var frame = new Mat();
            //    while (true)
            //    {
            //        capture2.Read(frame);
            //        if (frame.Empty())
            //            break;

            //        Mat = frame;
            //        RefreshMatToView();
            //        Cv2.WaitKey(this.SleepMilliseconds);

            //        //window.ShowImage(frame);
            //        //Cv2.WaitKey(sleepTime);
            //    }
            //    //}
            //}

            return base.Invoke(previors, current);
        }
    }
}
