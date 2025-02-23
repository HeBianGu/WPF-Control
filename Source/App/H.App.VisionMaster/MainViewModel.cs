using H.Controls.Diagram;
using H.Controls.Diagram.Extensions.OpenCV.NodeDataGroup;
using H.Extensions.Common;
using H.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace H.App.VisionMaster;

public class MainViewModel : BindableBase
{
    public MainViewModel()
    {
        this.NodeDataGroups = this.CreateNodeDataGroups().ToObservable();
    }

    public IEnumerable<INodeDataGroup> CreateNodeDataGroups()
    {
        return typeof(INodeDataGroup).Assembly.GetInstances<INodeDataGroup>().OrderBy(x => x.Order);
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

    private ObservableCollection<Node> _nodes = new ObservableCollection<Node>();
    public ObservableCollection<Node> Nodes
    {
        get { return _nodes; }
        set
        {
            _nodes = value;
            RaisePropertyChanged();
        }
    }
    public RelayCommand StartCommand => new RelayCommand((s, e) =>
    {
        if (e is UIElement element)
        {
            var diagram = GetDiagram(element);
            diagram?.Start();
        }
    }, (s, e) =>
    {
        if (e is UIElement element)
        {
            return GetDiagram(element)?.CanStart() == true;
        }
        return false;
    });

    public RelayCommand StopCommand => new RelayCommand((s, e) =>
    {
        if (e is UIElement element)
        {
            var diagram = GetDiagram(element);
            diagram?.Stop();
        }
    }, (s, e) =>
    {
        if (e is UIElement element)
        {
            return GetDiagram(element)?.CanStop() == true;
        }
        return false;
    });


    public Diagram GetDiagram(UIElement element)
    {
        var viewbox = element.GetElement<Viewbox>();
        return viewbox?.Child as Diagram;
    }

}
