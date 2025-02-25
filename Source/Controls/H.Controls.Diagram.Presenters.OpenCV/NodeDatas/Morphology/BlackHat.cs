namespace H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Morphology;


[Display(Name = "黑帽", GroupName = "形态学", Description = " 原图 - 闭运算  ，大图形内的小图形", Order = 0)]
public class BlackHat : MorphologyActionNodeDataBase
{
    protected override MorphTypes GetMorphType()
    {
        return MorphTypes.BlackHat;
    }
}
