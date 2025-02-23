namespace H.Controls.Diagram.Extensions.OpenCV;
[Display(Name = "一堆形状", GroupName = "数据源", Order = 0)]
public class Shape : StartNodeDataBase
{
    protected override string GetImagePath()
    {
        return ImagePath.Shapes;
    }
}
