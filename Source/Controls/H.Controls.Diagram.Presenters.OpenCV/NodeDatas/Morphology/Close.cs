namespace H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Morphology;

[Display(Name = "闭运算", GroupName = "形态学", Description = " 膨胀 + 腐蚀  ，去除大图形内的小图形", Order = 20)]
public class Close : MorphologyOpenCVNodeDataBase
{
    protected override MorphTypes GetMorphType()
    {
        return MorphTypes.Close;
    }
}
