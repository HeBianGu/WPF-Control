namespace H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Image;
[Display(Name = "合照", GroupName = "数据源", Order = 0)]
public class Yalta : OpenCVImageNodeDataBase
{
    protected override string GetImagePath()
    {
        return ImagePath.Yalta;
    }
}
