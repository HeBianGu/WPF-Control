namespace H.Controls.Diagram.Extensions.OpenCV.NodeDataGroup;

[Icon("\xE15A")]
[Display(Name = "形态", GroupName = "对图像进行腐蚀、膨胀、开运算和闭运算", Order = 4)]
public class MorphologyDataGroup : NodeDataGroupBase
{
    protected override IEnumerable<INodeData> CreateNodeDatas()
    {
        return typeof(IMorphology).Assembly.GetInstances<IMorphology>().OrderBy(x => x.Order); ;
    }
}

