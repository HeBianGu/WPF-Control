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
            RaisePropertyChanged();
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
            RaisePropertyChanged();
        }
    }

    protected override FlowableResult<Mat> Invoke(ISrcImageNodeData srcImageNodeData, IOpenCVNodeData from, IFlowableDiagramData diagram)
    {
        var mat = from.Mat.CvtColor(this.ColorConversionCode, this.DstCn);
        return this.OK(mat);
    }
}
