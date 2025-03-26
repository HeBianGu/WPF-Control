using System.Collections.ObjectModel;
using System.Windows.Markup;
using System.Xml.Serialization;

namespace H.Presenters.Design.Base;

[ContentProperty("Presenters")]
[DefaultProperty("Presenters")]
public abstract class PanelPresenterBase : DropAdornerDesignPresenterBase
{
    public PanelPresenterBase()
    {
        //this.MinHeight = 100;
        this.MinWidth = 300;
    }
    private ObservableCollection<IDesignPresenter> _presenters = new ObservableCollection<IDesignPresenter>();
    [Browsable(false)]
    [System.Text.Json.Serialization.JsonIgnore]

    [XmlIgnore]
    public ObservableCollection<IDesignPresenter> Presenters
    {
        get { return _presenters; }
        set
        {
            _presenters = value;
            RaisePropertyChanged();
        }
    }

    private HorizontalAlignment _childrenHorizontalAlignment = HorizontalAlignment.Center;
    [Display(Name = "水平内部对齐", GroupName = "常用,样式")]
    public HorizontalAlignment ChildrenHorizontalAlignment
    {
        get { return _childrenHorizontalAlignment; }
        set
        {
            _childrenHorizontalAlignment = value;
            RaisePropertyChanged();
        }
    }

    private VerticalAlignment _childrenVerticalAlignment = VerticalAlignment.Center;
    [Display(Name = "垂直内部对齐", GroupName = "常用,样式")]
    public VerticalAlignment ChildrenVerticalAlignment
    {
        get { return _childrenVerticalAlignment; }
        set
        {
            _childrenVerticalAlignment = value;
            RaisePropertyChanged();
        }
    }

    public override void DragEnter(UIElement element, DragEventArgs e)
    {
        IDraggableAdorner adorner = e.Data.GetData("DragGroup") as IDraggableAdorner;
        if (adorner.GetData() is DesignPresenter value)
        {
            this.Presenters.Add(value);
            _dropBackup = value;
            _dropBackup.Opacity = 0.5;
        }
    }

    protected IDesignPresenter _dropBackup;
    public override void Drop(UIElement element, DragEventArgs e)
    {
        this.Presenters.Remove(_dropBackup);
        _dropBackup.Opacity = 1;
        this.Presenters.Add(_dropBackup.Clone() as DesignPresenter);
        _dropBackup = null;
    }

    public override void DragLeave(UIElement element, DragEventArgs e)
    {
        this.Presenters.Remove(_dropBackup);
        _dropBackup = null;

    }
}
