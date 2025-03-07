namespace H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Basic;
[Display(Name = "二值化", GroupName = "基础函数", Description = "降噪成黑白色", Order = 0)]
public class Threshold : BasicActionNodeDataBase
{
    private double _thresh = 0.0;
    [DefaultValue(0.0)]
    [Range(0, 255)]
    [Display(Name = "阈值", GroupName = "数据")]
    public double Thresh
    {
        get { return _thresh; }
        set
        {
            _thresh = value;
            DispatcherRaisePropertyChanged();
        }
    }


    private double _maxval = 255;
    [Range(0, 255)]
    [Display(Name = "最大阈值", GroupName = "数据")]
    public double Maxval
    {
        get { return _maxval; }
        set
        {
            _maxval = value;
            DispatcherRaisePropertyChanged();
        }
    }

    private ThresholdTypes _thresholdTypes = ThresholdTypes.Otsu | ThresholdTypes.Binary;
    [Display(Name = "阈值类型", GroupName = "数据")]
    public ThresholdTypes ThresholdType
    {
        get { return _thresholdTypes; }
        set
        {
            _thresholdTypes = value;
            DispatcherRaisePropertyChanged();
        }
    }


    protected override IFlowableResult Invoke()
    {
        this.Mat = this.PreviourMat.Threshold(this.Thresh, this.Maxval, this.ThresholdType);
        this.RefreshMatToView();
        return base.Invoke();
    }
}
