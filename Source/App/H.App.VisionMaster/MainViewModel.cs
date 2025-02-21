using H.Controls.Diagram.Extensions.Workflow;
using H.Mvvm;
using System.Collections.ObjectModel;

namespace H.App.VisionMaster;

public class MainViewModel : BindableBase
{
    public MainViewModel()
    {
        for (int i = 0; i < 5; i++)
        {
            {
                var group = new NodeDataGroup();
                group.Name = $"Group {i}";
                for (int j = 0; j < 20; j++)
                {
                    group.NodeDatas.Add(new CircleNodeData() { Width = 50, Height = 30 });
                }
                this.NodeDataGroups.Add(group);
            }
        }
    }
    private ObservableCollection<INodeDataGroup> _nodeDataGroups = new ObservableCollection<INodeDataGroup>();
    public ObservableCollection<INodeDataGroup> NodeDataGroups
    {
        get { return _nodeDataGroups; }
        set
        {
            _nodeDataGroups = value;
            RaisePropertyChanged();
        }
    }
}