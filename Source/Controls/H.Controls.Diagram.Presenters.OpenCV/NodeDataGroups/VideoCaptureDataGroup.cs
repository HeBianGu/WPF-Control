namespace H.Controls.Diagram.Presenters.OpenCV.NodeDataGroups;

[Icon(FontIcons.Camera)]
[Display(Name = "视频捕获数据源", GroupName = "设置输入视频捕获图片", Order = 0)]
public class VideoCaptureDataGroup : BasicDataGroupBase
{
    protected override IEnumerable<INodeData> CreateNodeDatas()
    {
        return typeof(IVideoCaptureImageImportNodeData).Assembly.GetInstances<IVideoCaptureImageImportNodeData>().OrderBy(x => x.Order); ;
    }
}