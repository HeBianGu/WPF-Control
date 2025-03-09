namespace H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Basic;
[Display(Name = "色彩变换", GroupName = "基础函数", Description = "设置图片颜色", Order = 2)]
public class CvtColor : BasicOpenCVNodeDataBase
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

    protected override IFlowableResult Invoke()
    {
        this.Mat = this.PreviourMat.CvtColor(this.ColorConversionCode, this.DstCn);
        this.RefreshMatToView();
        return base.Invoke();
    }

}
