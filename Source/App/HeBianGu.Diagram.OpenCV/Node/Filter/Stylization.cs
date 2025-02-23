// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

namespace HeBianGu.Diagram.OpenCV
{


    [Display(Name = "边缘感知", GroupName = "滤波/降噪/模糊", Order = 0)]
    public class Stylization : OpenCVNodeData
    {
        private float _sigmaS = 60f;
        [DefaultValue(60f)]
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


        private float _sigmaR = 0.45f;
        [DefaultValue(0.45f)]
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

        public override IFlowableResult Invoke(Part previors, Node current)
        {
            var src = this._preMat;
            var stylized = new Mat();
            Cv2.Stylization(src, stylized, this.SigmaS, this.SigmaR);
            this.Mat = stylized;
            this.RefreshMatToView();
            return base.Invoke(previors, current);
        }

        protected override IFlowableResult Refresh()
        {
            var src = this._preMat;
            var stylized = new Mat();
            Cv2.Stylization(src, stylized, this.SigmaS, this.SigmaR);
            this.Mat = stylized;
            this.RefreshMatToView();
            return base.Refresh();
        }
    }
}
