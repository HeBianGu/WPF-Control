
using H.Controls.Diagram.Presenter.DiagramDatas.Base;

namespace H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Basic;
[Display(Name = "二值化", GroupName = "基础函数", Description = "降噪成黑白色", Order = 3)]
public class Threshold : BasicOpenCVNodeDataBase
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
            RaisePropertyChanged();
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
            RaisePropertyChanged();
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
            RaisePropertyChanged();
        }
    }
    protected override FlowableResult<Mat> Invoke(ISrcImageNodeData srcImageNodeData, IOpenCVNodeData from, IFlowableDiagramData diagram)
    {
        var mat = from.Mat.Threshold(this.Thresh, this.Maxval, this.ThresholdType);
        return this.OK(mat);
    }
}
