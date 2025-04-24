namespace H.Controls.Diagram.Presenters.OpenCV.Base;

[Icon(FontIcons.Favicon2)]
public abstract class YolovOpenCVNodeDataBase : OpenCVNodeDataBase, IYolovOpenCVNodeData
{
    private int _outPutThickness = 3;
    [DefaultValue(3)]
    [Display(Name = "绘制线条粗细", GroupName = "输出样式")]
    public int OutPutThickness
    {
        get { return _outPutThickness; }
        set
        {
            _outPutThickness = value;
            RaisePropertyChanged();
        }
    }

    private Color _outputColor = Colors.Chartreuse;
    [Display(Name = "绘制颜色", GroupName = "输出样式")]
    public Color OutputColor
    {
        get { return _outputColor; }
        set
        {
            _outputColor = value;
            RaisePropertyChanged();
        }
    }

    private Color _outputLabelColor = Colors.Black;
    [Display(Name = "标签颜色", GroupName = "输出样式")]
    public Color OutputLabelColor
    {
        get { return _outputLabelColor; }
        set
        {
            _outputLabelColor = value;
            RaisePropertyChanged();
        }
    }
}
