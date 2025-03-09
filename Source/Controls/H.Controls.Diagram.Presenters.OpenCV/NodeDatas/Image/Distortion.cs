namespace H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Image;
[Display(Name = "变形网格", GroupName = "数据源", Order = 20)]
public class Distortion : OpenCVImageNodeDataBase
{
    protected override string GetImagePath()
    {
        return ImagePath.Distortion;
    }
}
