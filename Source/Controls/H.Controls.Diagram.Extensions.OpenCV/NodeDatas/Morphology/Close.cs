

namespace H.Controls.Diagram.Extensions.OpenCV;



[Display(Name = "闭运算", GroupName = "形态学", Description = " 膨胀 + 腐蚀  ，去除大图形内的小图形", Order = 0)]
public class Close : MorphologyActionNodeDataBase
{
    protected override MorphTypes GetMorphType()
    {
        return MorphTypes.Close;
    }
}
