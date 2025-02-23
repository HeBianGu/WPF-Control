namespace H.Controls.Diagram.Extensions.OpenCV;
[Display(Name = "山魈", GroupName = "数据源", Order = 0)]
public class Mandrill : StartNodeDataBase
{
    protected override string GetImagePath()
    {
        return ImagePath.Mandrill;
    }
}
