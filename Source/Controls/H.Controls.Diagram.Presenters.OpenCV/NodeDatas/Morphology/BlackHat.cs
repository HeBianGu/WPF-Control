namespace H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Morphology;

[Display(Name = "黑帽", GroupName = "形态学", Description = " 原图 - 闭运算，闭运算结果与原图像的差，用于提取比背景更暗的区域", Order = 40)]
public class BlackHat : MorphologyOpenCVNodeDataBase
{
    protected override MorphTypes GetMorphType()
    {
        return MorphTypes.BlackHat;
    }
}
