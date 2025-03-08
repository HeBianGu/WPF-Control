namespace H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Basic;
[Display(Name = "乘除运算", GroupName = "基础函数", Description = "图片亮度设置", Order = 51)]
public class MultiplayDivide : BasicOpenCVNodeDataBase
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

    protected override IFlowableResult Invoke()
    {
        this.Mat = this.PreviourMat * this.Value;
        this.RefreshMatToView();
        return base.Invoke();
    }
}
