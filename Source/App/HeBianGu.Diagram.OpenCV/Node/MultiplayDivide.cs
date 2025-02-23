// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

namespace HeBianGu.Diagram.OpenCV
{



    [Display(Name = "乘除运算", GroupName = "基础函数", Description = "图片亮度设置", Order = 0)]
    public class MultiplayDivide : OpenCVNodeData
    {
        private double _value = 1.2;
        [DefaultValue(1.2)]
        [Display(Name = "数值", GroupName = "数据")]
        public double Value
        {
            get { return _value; }
            set
            {
                _value = value;
                DispatcherRaisePropertyChanged();
            }
        }

        protected override IFlowableResult Refresh()
        {
            this.Mat = this._preMat * this.Value;
            this.RefreshMatToView();
            return base.Refresh();
        }
    }
}
