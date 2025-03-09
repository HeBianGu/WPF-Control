namespace H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Morphology;

[Display(Name = "开运算", GroupName = "形态学", Description = "腐蚀 + 膨胀，先腐蚀后膨胀，用于去除小物体或噪声", Order = 21)]
public class Open : MorphologyOpenCVNodeDataBase
{
    protected override MorphTypes GetMorphType()
    {
        return MorphTypes.Open;
    }
}
