
namespace H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Image;
[Display(Name = "行人", GroupName = "数据源", Order = 20)]
public class Asahiyama : OpenCVImageNodeDataBase
{
    protected override string GetImagePath()
    {
        return ImagePath.Asahiyama;
    }
}
