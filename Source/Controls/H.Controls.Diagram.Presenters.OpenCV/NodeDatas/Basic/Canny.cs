namespace H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Basic;
[Display(Name = "边缘检测", GroupName = "基础函数", Description = "查找图片边缘，图片>灰度>二值化>边缘检测", Order = 10)]
public class Canny : BasicOpenCVNodeDataBase
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
            RaisePropertyChanged();
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
            RaisePropertyChanged();
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
            RaisePropertyChanged();
        }
    }

    protected override FlowableResult<Mat> Invoke(ISrcImageNodeData srcImageNodeData, IOpenCVNodeData from, IFlowableDiagramData diagram)
    {
        Mat preMat = from.Mat;
        Cv2.Canny(preMat, preMat, 50, 200, 3, false);
        return this.OK(preMat);
    }
}
