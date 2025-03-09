global using H.Controls.Diagram.Presenters.OpenCV.Base;

namespace H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Basic;

[Display(Name = "加减运算(饱和度)", GroupName = "基础函数", Description = "设置饱和度", Order = 50)]
public class AddSutract : BasicOpenCVNodeDataBase
{
    private double _value = 50;
    [DefaultValue(50.0)]
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
        this.Mat = this.PreviourMat + this.Value;
        this.RefreshMatToView();
        return base.Invoke();
    }
}
