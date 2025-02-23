namespace H.Controls.Diagram.Extensions.OpenCV;
[Display(Name = "变形网格", GroupName = "数据源", Order = 0)]
public class Distortion : StartNodeDataBase
{
    protected override string GetImagePath()
    {
        return ImagePath.Distortion;
    }
}
