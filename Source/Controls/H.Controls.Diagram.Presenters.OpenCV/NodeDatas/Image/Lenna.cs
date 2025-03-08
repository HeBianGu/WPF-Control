namespace H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Image;
[Display(Name = "头像", GroupName = "数据源", Order = 0)]
public class Lenna : OpenCVImageNodeDataBase
{
    protected override string GetImagePath()
    {
        return ImagePath.Lenna;
    }
}
