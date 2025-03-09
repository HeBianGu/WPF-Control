namespace H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Image;

[Display(Name = "笔迹", GroupName = "数据源", Order = 20)]
public class Cvmorph : OpenCVImageNodeDataBase
{
    protected override string GetImagePath()
    {
        return ImagePath.Cvmorph;
    }
}
