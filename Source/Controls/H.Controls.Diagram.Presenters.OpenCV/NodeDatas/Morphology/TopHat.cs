namespace H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Morphology;

[Display(Name = "顶帽", GroupName = "形态学", Description = " 原图 - 开运算  ，大图形外的小图形", Order = 40)]
public class TopHat : MorphologyActionNodeDataBase
{
    protected override MorphTypes GetMorphType()
    {
        return MorphTypes.TopHat;
    }
}
