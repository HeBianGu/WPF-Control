// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

namespace HeBianGu.Diagram.OpenCV
{


    [Display(Name = "细节增强", GroupName = "滤波/降噪/模糊", Order = 0)]
    public class DetailEnhance : OpenCVNodeData
    {
        private float _sigmaS = 10f;
        [DefaultValue(10f)]
        [Display(Name = "SigmaS", GroupName = "数据")]
        [Range(0, 200)]
        public float SigmaS
        {
            get { return _sigmaS; }
            set
            {
                _sigmaS = value;
                DispatcherRaisePropertyChanged();
            }
        }


        private float _sigmaR = 0.15f;
        [DefaultValue(0.15f)]
        [Display(Name = "SigmaR", GroupName = "数据")]
        [Range(0, 1.0)]
        public float SigmaR
        {
            get { return _sigmaR; }
            set
            {
                _sigmaR = value;
                DispatcherRaisePropertyChanged();
            }
        }

        protected override IFlowableResult Refresh()
        {
            var src = this._preMat;
            var detailEnhance = new Mat();
            Cv2.DetailEnhance(src, detailEnhance, this.SigmaS, this.SigmaR);
            this.Mat = detailEnhance;
            this.RefreshMatToView();
            return base.Refresh();
        }
    }
}
