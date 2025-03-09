namespace H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Morphology;

[Display(Name = "闭运算", GroupName = "形态学", Description = " 膨胀 + 腐蚀，先膨胀后腐蚀，用于填充小孔或连接断裂区域", Order = 20)]
public class Close : MorphologyOpenCVNodeDataBase
{
    protected override MorphTypes GetMorphType()
    {
        return MorphTypes.Close;
    }
}
