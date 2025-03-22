namespace H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Basic;

/// <summary>
/// 图像增强：
/// 通过乘法或加权乘法增强图像的亮度或对比度。
/// 图像融合：
/// 将多幅图像融合成一幅图像。
/// 颜色校正：
/// 通过除法或加权除法调整图像的亮度和对比度。
/// 图像恢复：
/// 在去噪或图像恢复中，除法运算可以用于去除不均匀光照。
/// </summary>
[Display(Name = "乘除运算(亮度)", GroupName = "基础函数", Description = "设置图片亮度", Order = 51)]
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
            RaisePropertyChanged();
        }
    }

    protected override FlowableResult<Mat> Invoke(ISrcImageNodeData srcImageNodeData, IOpenCVNodeData from, IFlowableDiagramData diagram)
    {
        return this.OK(from.Mat * this.Value);
    }
}
