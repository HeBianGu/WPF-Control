namespace H.Controls.Diagram.Extensions.OpenCV;
[Display(Name = "头像", GroupName = "数据源", Order = 0)]
public class Lenna : StartNodeDataBase
{
    protected override string GetImagePath()
    {
        return ImagePath.Lenna;
    }
}
