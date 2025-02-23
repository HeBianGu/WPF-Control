// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using OpenCvSharp;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;

namespace HeBianGu.Diagram.OpenCV
{
    public abstract class CascadeBase : OpenCVNodeData
    {
        public override void LoadDefault()
        {
            base.LoadDefault();
            this.MinSize = new Size(30, 30);
        }
        private double _scaleFactor = 1.1;
        [DefaultValue(1.1)]
        [Display(Name = "ScaleFactor", GroupName = "数据")]
        public double ScaleFactor
        {
            get { return _scaleFactor; }
            set
            {
                _scaleFactor = value;
                RaisePropertyChanged();
            }
        }


        private int _minNeighbors = 3;
        [DefaultValue(3)]
        [Display(Name = "MinNeighbors", GroupName = "数据")]
        public int MinNeighbors
        {
            get { return _minNeighbors; }
            set
            {
                _minNeighbors = value;
                RaisePropertyChanged();
            }
        }

        private HaarDetectionTypes _flags = HaarDetectionTypes.ScaleImage;
        [DefaultValue(HaarDetectionTypes.ScaleImage)]
        [Display(Name = "Flags", GroupName = "数据")]
        public HaarDetectionTypes Flags
        {
            get { return _flags; }
            set
            {
                _flags = value;
                RaisePropertyChanged();
            }
        }


        private Size? _minSize = new Size(30, 30);
        [Display(Name = "MinSize", GroupName = "数据")]
        public Size? MinSize
        {
            get { return _minSize; }
            set
            {
                _minSize = value;
                RaisePropertyChanged();
            }
        }


        private Size? _maxSize = null;
        [DefaultValue(null)]
        [Display(Name = "MaxSize", GroupName = "数据")]
        public Size? MaxSize
        {
            get { return _maxSize; }
            set
            {
                _maxSize = value;
                RaisePropertyChanged();
            }
        }


        protected Mat DetectFace(CascadeClassifier cascade, Mat src)
        {
            Mat result;
            using (var gray = new Mat())
            {
                result = src.Clone();
                Cv2.CvtColor(src, gray, ColorConversionCodes.BGR2GRAY);

                // Detect faces
                Rect[] faces = cascade.DetectMultiScale(
                    gray, this.ScaleFactor, this.MinNeighbors, this.Flags, this.MinSize, this.MaxSize);

                // Render all detected faces
                foreach (Rect face in faces)
                {
                    var center = new Point
                    {
                        X = (int)(face.X + face.Width * 0.5),
                        Y = (int)(face.Y + face.Height * 0.5)
                    };
                    var axes = new Size
                    {
                        Width = (int)(face.Width * 0.5),
                        Height = (int)(face.Height * 0.5)
                    };
                    Cv2.Ellipse(result, center, axes, 0, 0, 360, new Scalar(255, 0, 255), 4);
                }
            }
            return result;
        }
    }
}
