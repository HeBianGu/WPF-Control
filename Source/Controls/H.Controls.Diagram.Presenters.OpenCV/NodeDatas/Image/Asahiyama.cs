namespace H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Start;
[Display(Name = "行人", GroupName = "数据源", Order = 0)]
public class Asahiyama : OpenCVImageNodeDataBase
{
    protected override string GetImagePath()
    {
        return ImagePath.Asahiyama;
    }
}
