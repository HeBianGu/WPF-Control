using H.Extensions.FontIcon;

namespace H.Controls.Diagram.Presenters.OpenCV.NodeDataGroups;

[Icon(FontIcons.Video)]
[Display(Name = "视频捕获数据源", GroupName = "设置输入视频捕获图片", Order = 0)]
public class VideoCaptureDataGroup : BasicDataGroupBase
{
    protected override IEnumerable<INodeData> CreateNodeDatas()
    {
        return typeof(IVideoCaptureNodeData).Assembly.GetInstances<IVideoCaptureNodeData>().OrderBy(x => x.Order); ;
    }
}