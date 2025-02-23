// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

namespace HeBianGu.Diagram.OpenCV
{



    [Display(Name = "色彩变换", GroupName = "基础函数", Order = 0)]
    public class CvtColor : OpenCVNodeData
    {
        private ColorConversionCodes _colorConversionCode = ColorConversionCodes.BGR2GRAY;
        [DefaultValue(ColorConversionCodes.BGR2GRAY)]
        [Display(Name = "转换规则", GroupName = "数据")]
        public ColorConversionCodes ColorConversionCode
        {
            get { return _colorConversionCode; }
            set
            {
                _colorConversionCode = value;
                DispatcherRaisePropertyChanged();
            }
        }

        private int _dstCn = 0;
        [DefaultValue(0)]
        [Display(Name = "通道数", GroupName = "数据")]
        public int DstCn
        {
            get { return _dstCn; }
            set
            {
                _dstCn = value;
                DispatcherRaisePropertyChanged();
            }
        }

        //public override IFlowableResult Invoke(Part previors, Node current)
        //{
        //    this.Mat = this.GetFromMat(current).CvtColor(this.ColorConversionCode, this.DstCn);
        //    this.RefreshMatToView();
        //    return base.Invoke(previors, current);
        //}

        protected override IFlowableResult Refresh()
        {
            this.Mat = this._preMat.CvtColor(this.ColorConversionCode, this.DstCn);
            this.RefreshMatToView();
            return base.Refresh();
        }

    }
}
