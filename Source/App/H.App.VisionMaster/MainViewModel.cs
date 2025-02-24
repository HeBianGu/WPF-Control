using H.Controls.Diagram;
using H.Controls.Diagram.Extension;
using H.Controls.Diagram.Extensions.OpenCV;
using H.Controls.Diagram.Extensions.OpenCV.NodeDataGroup;
using H.Extensions.Common;
using H.Mvvm;
using H.Services.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace H.App.VisionMaster;

public class MainViewModel : BindableBase
{
    public MainViewModel()
    {
        this.NodeDataGroups = this.CreateNodeDataGroups().ToObservable();
        this.Images = OpenCVImages.GetImageSources().ToObservable();
        this.SelectedImage = this.Images.FirstOrDefault();
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

    public RelayCommand MouseDoubleClickCommand => new RelayCommand((s, e) =>
    {
        if (e is MouseButtonEventArgs args && args.OriginalSource is FrameworkElement framework)
        {
            if (framework?.DataContext is ITextNodeData nodeData)
            {
                IocMessage.Form?.ShowTabEdit(nodeData, x =>
                {
                    x.Title = nodeData.Text;
                }, null, x =>
                {
                    x.UseGroupNames = "数据";
                    x.UseCommand = false;
                    x.TabNames = new ObservableCollection<string>() { "数据", "样式", "工具", "常用" };
                });
            }
        }
    });

    public Diagram GetDiagram(UIElement element)
    {
        var viewbox = element.GetElement<Viewbox>();
        return viewbox?.Child as Diagram;
    }


    private ObservableCollection<ImageSource> _images = new ObservableCollection<ImageSource>();
    public ObservableCollection<ImageSource> Images
    {
        get { return _images; }
        set
        {
            _images = value;
            RaisePropertyChanged();
        }
    }

    private ImageSource _selectedImage;
    public ImageSource SelectedImage
    {
        get { return _selectedImage; }
        set
        {
            _selectedImage = value;
            RaisePropertyChanged();
        }
    }


}
