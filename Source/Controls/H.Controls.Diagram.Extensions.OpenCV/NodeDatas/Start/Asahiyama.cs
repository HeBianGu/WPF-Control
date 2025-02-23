namespace H.Controls.Diagram.Extensions.OpenCV;
[Display(Name = "行人", GroupName = "数据源", Order = 0)]
public class Asahiyama : StartNodeDataBase
{
    protected override string GetImagePath()
    {
        return ImagePath.Asahiyama;
    }
}
