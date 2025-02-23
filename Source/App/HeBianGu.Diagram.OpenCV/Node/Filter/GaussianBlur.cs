// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase




using OpenCvSharp;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HeBianGu.Diagram.OpenCV
{
    
    
    [Display(Name = "高斯滤波", GroupName = "滤波/降噪/模糊", Order = 0)]
    public class GaussianBlur : OpenCVNodeData
    {
        public override void LoadDefault()
        {
            base.LoadDefault();
            this.KSize = new Size(7, 7);
        }
        private Size _ksize = new Size(7, 7);
        [Display(Name = "KSize", GroupName = "数据")]
        public Size KSize
        {
            get { return _ksize; }
            set
            {
                _ksize = value;
                DispatcherRaisePropertyChanged();
            }
        }


        private double _sigmaX;
        [DefaultValue(0.0)]
        [Display(Name = "SigmaX", GroupName = "数据")]
        public double SigmaX
        {
            get { return _sigmaX; }
            set
            {
                _sigmaX = value;
                DispatcherRaisePropertyChanged();
            }
        }


        private double _sigmaY;
        [DefaultValue(0.0)]
        [Display(Name = "SigmaY", GroupName = "数据")]
        public double SigmaY
        {
            get { return _sigmaY; }
            set
            {
                _sigmaY = value;
                DispatcherRaisePropertyChanged();
            }
        }

        private BorderTypes _borderType = BorderTypes.Default;
        [DefaultValue(BorderTypes.Default)]
        [Display(Name = "BorderType", GroupName = "数据")]
        public BorderTypes BorderType
        {
            get { return _borderType; }
            set
            {
                _borderType = value;
                DispatcherRaisePropertyChanged();
            }
        }

        protected override IFlowableResult Refresh()
        {
            var preMat = this._preMat;
            Cv2.GaussianBlur(preMat, preMat, KSize, SigmaX, SigmaY, BorderType);
            Mat = preMat;
            RefreshMatToView();
            return base.Refresh();
        }
    }
}
