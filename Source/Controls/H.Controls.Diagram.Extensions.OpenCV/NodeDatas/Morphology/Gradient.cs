

namespace H.Controls.Diagram.Extensions.OpenCV;



[Display(Name = "梯度", GroupName = "形态学", Description = " 原图 - 腐蚀  ，求图形的边缘", Order = 0)]
public class Gradient : MorphologyActionNodeDataBase
{
    protected override MorphTypes GetMorphType()
    {
        return MorphTypes.Gradient;
    }
}
