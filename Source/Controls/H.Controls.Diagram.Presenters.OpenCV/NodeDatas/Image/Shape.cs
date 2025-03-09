namespace H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Image;
[Display(Name = "一堆形状", GroupName = "数据源", Order = 20)]
public class Shape : OpenCVImageNodeDataBase
{
    protected override string GetImagePath()
    {
        return ImagePath.Shapes;
    }
}
