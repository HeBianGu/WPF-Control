using System.Collections.ObjectModel;

namespace H.Controls.Diagram.Extensions.OpenCV.NodeDataGroup;

public interface INodeDataGroup : IDisplayBindable
{
    ObservableCollection<INodeData> NodeDatas { get; set; }
}
