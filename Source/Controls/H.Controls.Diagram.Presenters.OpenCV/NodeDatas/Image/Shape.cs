namespace H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Start;
[Display(Name = "一堆形状", GroupName = "数据源", Order = 0)]
public class Shape : Base.OpenCVImageNodeDataBase
{
    protected override string GetImagePath()
    {
        return ImagePath.Shapes;
    }
}
