global using H.Controls.Diagram.Presenters.OpenCV.Base;

namespace H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Basic;
/// <summary>
/// 图像增强：
/// 通过加法或加权加法增强图像的亮度或对比度。
/// 背景去除：
/// 通过减法或绝对值减法提取前景目标。
/// 图像融合：
/// 将多幅图像融合成一幅图像。
/// 运动检测：
/// 在视频中检测运动物体。
/// 差异分析：
/// 比较两幅图像的差异，用于变化检测或缺陷检测。
/// </summary>
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
            RaisePropertyChanged();
        }
    }

    private bool _useAbs;
    [Display(Name = "启用绝对值", GroupName = "数据")]
    public bool UseAbs
    {
        get { return _useAbs; }
        set
        {
            _useAbs = value;
            RaisePropertyChanged();
        }
    }


    protected override FlowableResult<Mat> Invoke(ISrcImageNodeData srcImageNodeData, IOpenCVNodeData from, IFlowableDiagramData diagram)
    {
        var mat = new Mat();
        if(this.UseAbs)
        {
            // 绝对值减法
            Cv2.Absdiff(from.Mat, this.Value, mat);
        }
        else
        {
            mat = from.Mat + this.Value;
        }
        return this.OK(mat);
    }
}
