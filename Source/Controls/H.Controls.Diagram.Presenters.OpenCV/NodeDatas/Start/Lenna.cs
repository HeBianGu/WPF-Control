namespace H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Start;
[Display(Name = "头像", GroupName = "数据源", Order = 0)]
public class Lenna : StartNodeDataBase
{
    protected override string GetImagePath()
    {
        return ImagePath.Lenna;
    }
}
