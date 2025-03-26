global using H.Mvvm.ViewModels;

namespace H.Controls.Diagram.Presenter.DiagramDatas;

public interface INodeDataGroup : IDisplayBindable
{
    ObservableCollection<INodeData> NodeDatas { get; set; }
}
