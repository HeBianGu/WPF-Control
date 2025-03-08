namespace H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Morphology;

[Display(Name = "梯度", GroupName = "形态学", Description = " 原图 - 腐蚀  ，求图形的边缘", Order = 50)]
public class Gradient : MorphologyOpenCVNodeDataBase
{
    protected override MorphTypes GetMorphType()
    {
        return MorphTypes.Gradient;
    }
}
