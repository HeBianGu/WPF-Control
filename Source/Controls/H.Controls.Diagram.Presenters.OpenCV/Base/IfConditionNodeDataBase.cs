namespace H.Controls.Diagram.Presenters.OpenCV.Base;

public interface IIfConditionNodeData : INodeData, IDisplayBindable
{

}

[Icon(FontIcons.Dial6)]
public abstract class IfConditionNodeDataBase : OpenCVNodeDataBase, IIfConditionNodeData
{

}
