namespace H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Image;
[Display(Name = "公园", GroupName = "数据源", Order = 20)]
public class Goryokaku : OpenCVImageNodeDataBase
{
    protected override string GetImagePath()
    {
        return ImagePath.Goryokaku;
    }
}
