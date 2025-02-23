// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

namespace HeBianGu.Diagram.OpenCV
{

    [Display(Name = "FAST", GroupName = "特征提取", Order = 0)]
    public class FastFeatureDetector : OpenCVNodeData
    {
        private bool _nonmaxSupression = true;
        [Display(Name = "NonmaxSupression", GroupName = "数据")]
        public bool NonmaxSupression
        {
            get { return _nonmaxSupression; }
            set
            {
                _nonmaxSupression = value;
                DispatcherRaisePropertyChanged();
            }
        }


        private int _threshold;
        [Range(0, 500)]
        [Display(Name = "Threshold", GroupName = "数据")]
        public int Threshold
        {
            get { return _threshold; }
            set
            {
                _threshold = value;
                DispatcherRaisePropertyChanged();
            }
        }



        //public override IFlowableResult Invoke(Part previors, Node current)
        //{
        //    var imgSrc = GetFromMat(current);
        //    //using Mat imgSrc = new Mat(ImagePath.Lenna, ImreadModes.Color);
        //    Mat imgGray = new Mat();
        //    Mat imgDst = imgSrc.Clone();
        //    Cv2.CvtColor(imgSrc, imgGray, ColorConversionCodes.BGR2GRAY, 0);
        //    KeyPoint[] keypoints = Cv2.FAST(imgGray, 50, true);
        //    foreach (KeyPoint kp in keypoints)
        //    {
        //        imgDst.Circle((Point)kp.Pt, 3, Scalar.Red, -1, LineTypes.AntiAlias, 0);
        //    }
        //    RefreshMatToView(imgDst);
        //    return base.Invoke(previors, current);
        //}

        protected override IFlowableResult Refresh()
        {
            var imgSrc = this._preMat;
            //using Mat imgSrc = new Mat(ImagePath.Lenna, ImreadModes.Color);
            Mat imgGray = new Mat();
            Mat imgDst = imgSrc.Clone();
            Cv2.CvtColor(imgSrc, imgGray, ColorConversionCodes.BGR2GRAY, 0);
            KeyPoint[] keypoints = Cv2.FAST(imgGray, 50, true);
            foreach (KeyPoint kp in keypoints)
            {
                imgDst.Circle((Point)kp.Pt, 3, Scalar.Red, -1, LineTypes.AntiAlias, 0);
            }
            RefreshMatToView(imgDst);
            return base.Refresh();
        }
    }
}
