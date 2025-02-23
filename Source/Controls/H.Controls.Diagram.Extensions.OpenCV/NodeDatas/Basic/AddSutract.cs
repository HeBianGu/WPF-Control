global using H.Controls.Diagram.Extensions.OpenCV.Base;

namespace H.Controls.Diagram.Extensions.OpenCV.NodeDatas.Basic;


[Display(Name = "加减运算", GroupName = "基础函数", Description = "饱和度设置", Order = 0)]
public class AddSutract : BasicActionNodeDataBase
{
    private double _value = 10;
    [DefaultValue(10.0)]
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
        this.Mat = this._preMat + this.Value;
        this.RefreshMatToView();
        return base.Refresh();
    }
}
