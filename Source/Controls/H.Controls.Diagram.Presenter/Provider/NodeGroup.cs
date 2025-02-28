using H.Controls.Diagram.Presenter.DiagramDatas.Base;

namespace H.Controls.Diagram.Presenter.Provider;

public class NodeGroup : NodeDataGroupBase
{
    protected override IEnumerable<INodeData> CreateNodeDatas()
    {
        return Enumerable.Empty<INodeData>();
    }
}
