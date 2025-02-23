

namespace H.Controls.Diagram.Extensions.OpenCV;



[Display(Name = "开运算", GroupName = "形态学", Description = "腐蚀 + 膨胀 ，去除大图形外的小图形", Order = 0)]
public class Open : MorphologyActionNodeDataBase
{
    protected override MorphTypes GetMorphType()
    {
        return MorphTypes.Open;
    }
}
