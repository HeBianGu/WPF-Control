// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase




using OpenCvSharp;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace HeBianGu.Diagram.OpenCV
{
    
    
    
    [Display(Name = "边缘检测", GroupName = "基础函数", Order = 0)]
    public class Canny : OpenCVNodeData
    {
        private double _threshold1;
        [DefaultValue(0.0)]
        [Display(Name = "Threshold1", GroupName = "数据")]
        public double Threshold1
        {
            get { return _threshold1; }
            set
            {
                _threshold1 = value;
                DispatcherRaisePropertyChanged();
            }
        }


        private double _threshold2;
        [DefaultValue(0.0)]
        [Display(Name = "Threshold2", GroupName = "数据")]
        public double Threshold2
        {
            get { return _threshold2; }
            set
            {
                _threshold2 = value;
                DispatcherRaisePropertyChanged();
            }
        }


        private int _apertureSize = 3;
        [DefaultValue(3)]
        [Display(Name = "ApertureSize", GroupName = "数据")]
        public int ApertureSize
        {
            get { return _apertureSize; }
            set
            {
                _apertureSize = value;
                RaisePropertyChanged();
            }
        }


        private bool _l2gradient = false;
        [DefaultValue(false)]
        [Display(Name = "L2gradient", GroupName = "数据")]
        public bool L2gradient
        {
            get { return _l2gradient; }
            set
            {
                _l2gradient = value;
                DispatcherRaisePropertyChanged();
            }
        }

        protected override IFlowableResult Refresh()
        {
            var preMat = this._preMat;
            this.FilePath = this._srcFilePath;
            Cv2.Canny(preMat, preMat, 50, 200, 3, false);
            this.Mat = preMat;
            this.RefreshMatToView();
           return base.Refresh();
        }
    }
}
