namespace H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Start;
[Display(Name = "山魈", GroupName = "数据源", Order = 0)]
public class Mandrill : Base.OpenCVImageNodeDataBase
{
    protected override string GetImagePath()
    {
        return ImagePath.Mandrill;
    }
}
