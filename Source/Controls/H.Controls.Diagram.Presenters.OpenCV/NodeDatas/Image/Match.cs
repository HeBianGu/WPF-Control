namespace H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Image;
[Display(Name = "一堆书籍", GroupName = "数据源", Order = 20)]
public class Match : OpenCVImageNodeDataBase
{
    protected override string GetImagePath()
    {
        return ImagePath.Match2;
    }
}
