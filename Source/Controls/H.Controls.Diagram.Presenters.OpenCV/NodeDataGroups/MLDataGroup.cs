namespace H.Controls.Diagram.Presenters.OpenCV.NodeDataGroups;

[Icon(FontIcons.DialShape1)]
[Display(Name = "机器学习模型", Description = "应用机器学习模型处理图像", Order = 90)]
public class MLDataGroup : BasicDataGroupBase, IVideoDataGroup, IImageDataGroup
{
    protected override IEnumerable<INodeData> CreateNodeDatas()
    {
        return typeof(IMLOpenCVNodeData).Assembly.GetInstances<IMLOpenCVNodeData>().OrderBy(x => x.Order);
    }
}
