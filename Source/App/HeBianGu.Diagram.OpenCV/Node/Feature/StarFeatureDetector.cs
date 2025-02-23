// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase




using OpenCvSharp;
using OpenCvSharp.XFeatures2D;
using System.ComponentModel.DataAnnotations;

namespace HeBianGu.Diagram.OpenCV
{
    
    [Display(Name = "STAR", GroupName = "特征提取", Order = 0)]
    public class StarFeatureDetector : OpenCVNodeData
    {
        private int _maxSize = 45;
        [Display(Name = "MaxSize", GroupName = "数据")]
        public int MaxSize
        {
            get { return _maxSize; }
            set
            {
                _maxSize = value;
                DispatcherRaisePropertyChanged();
            }
        }


        private int _responseThreshold = 30;
        [Display(Name = "ResponseThreshold", GroupName = "数据")]
        public int ResponseThreshold
        {
            get { return _responseThreshold; }
            set
            {
                _responseThreshold = value;
                DispatcherRaisePropertyChanged();
            }
        }


        private int _lineThresholdProjected = 10;
        [Display(Name = "LineThresholdProjected", GroupName = "数据")]
        public int LineThresholdProjected
        {
            get { return _lineThresholdProjected; }
            set
            {
                _lineThresholdProjected = value;
                DispatcherRaisePropertyChanged();
            }
        }


        private int _lineThresholdBinarized = 8;
        [Display(Name = "LineThresholdBinarized", GroupName = "数据")]
        public int LineThresholdBinarized
        {
            get { return _lineThresholdBinarized; }
            set
            {
                _lineThresholdBinarized = value;
                DispatcherRaisePropertyChanged();
            }
        }


        private int _suppressNonmaxSize = 5;
        [Display(Name = "SuppressNonmaxSize", GroupName = "数据")]
        public int SuppressNonmaxSize
        {
            get { return _suppressNonmaxSize; }
            set
            {
                _suppressNonmaxSize = value;
                DispatcherRaisePropertyChanged();
            }
        }




        protected override IFlowableResult Refresh()
        {
            string path = this._srcFilePath;
            this.FilePath = path;
            var dst = new Mat(path, ImreadModes.Color);
            var gray = this._preMat;

            StarDetector detector = StarDetector.Create(this.MaxSize, this.ResponseThreshold, this.LineThresholdProjected, this.LineThresholdBinarized, this.SuppressNonmaxSize);
            KeyPoint[] keypoints = detector.Detect(gray);

            if (keypoints != null)
            {
                var color = new Scalar(0, 255, 0);
                foreach (KeyPoint kpt in keypoints)
                {
                    float r = kpt.Size / 2;
                    Cv2.Circle(dst, (Point)kpt.Pt, (int)r, color);
                    Cv2.Line(dst,
                        (Point)new Point2f(kpt.Pt.X + r, kpt.Pt.Y + r),
                        (Point)new Point2f(kpt.Pt.X - r, kpt.Pt.Y - r),
                        color);
                    Cv2.Line(dst,
                        (Point)new Point2f(kpt.Pt.X - r, kpt.Pt.Y + r),
                        (Point)new Point2f(kpt.Pt.X + r, kpt.Pt.Y - r),
                        color);
                }
            }
            this.Mat = dst;
            RefreshMatToView();
            return base.Refresh();
        }
    }
}
