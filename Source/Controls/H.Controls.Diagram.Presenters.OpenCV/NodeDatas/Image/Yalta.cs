namespace H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Start;
[Display(Name = "合照", GroupName = "数据源", Order = 0)]
public class Yalta : Base.OpenCVImageNodeDataBase
{
    protected override string GetImagePath()
    {
        return ImagePath.Yalta;
    }
}
