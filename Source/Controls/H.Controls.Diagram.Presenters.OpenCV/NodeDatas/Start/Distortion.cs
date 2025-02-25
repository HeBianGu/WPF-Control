namespace H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Start;
[Display(Name = "变形网格", GroupName = "数据源", Order = 0)]
public class Distortion : StartNodeDataBase
{
    protected override string GetImagePath()
    {
        return ImagePath.Distortion;
    }
}
