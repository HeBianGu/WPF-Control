namespace H.Controls.Diagram.Presenters.OpenCV.NodeDatas.Start;

[Display(Name = "笔迹", GroupName = "数据源", Order = 0)]
public class Cvmorph : StartNodeDataBase
{
    protected override string GetImagePath()
    {
        return ImagePath.Cvmorph;
    }
}
