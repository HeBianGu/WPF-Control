namespace H.Controls.Diagram.Presenters.OpenCV.NodeDataGroups;

[Icon(FontIcons.Camera)]
[Display(Name = "视频捕获数据源", Description = "设置输入视频捕获图片", Order = 0)]
public class VideoCaptureDataGroup : VideoDataGroupBase, IVideoDataGroup
{
    protected override IEnumerable<INodeData> CreateNodeDatas()
    {
        return typeof(IVideoNodeData).Assembly.GetInstances<IVideoNodeData>().OrderBy(x => x.Order); ;
    }
}
