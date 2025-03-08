namespace H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Image;
[Display(Name = "山魈", GroupName = "数据源", Order = 0)]
public class Mandrill : OpenCVImageNodeDataBase
{
    protected override string GetImagePath()
    {
        return ImagePath.Mandrill;
    }
}
