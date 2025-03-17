global using H.Controls.Diagram.Layouts;
global using H.Controls.Diagram.Layouts.Base;
global using H.Controls.Diagram.LinkDrawers;
global using H.Controls.Diagram.Presenter.NodeDatas.Card;
global using System.Windows.Input;
using H.Controls.Diagram.GraphSource;
using H.Extensions.FontIcon;
using System.Text.Json.Serialization;
namespace H.Controls.Diagram.Presenter.DiagramDatas.Base;

public abstract class DiagramDataBase : DisplayBindableBase, IDiagramData
{
    public DiagramDataBase()
    {
        this.LinkDrawers = this.CreateLinkDrawerSource()?.ToObservable();
        this.LinkDrawer = this.LinkDrawers?.FirstOrDefault();
        this.Layouts = this.CreateLayousSource()?.ToObservable();
        this.Layout = this.Layouts?.FirstOrDefault();

        //this.DynamicStyle.Stroke = Application.Current.FindResource(BrushKeys.Red) as Brush;
        //this.DynamicCanDropStyle.Stroke = Application.Current.FindResource(BrushKeys.Green) as Brush;
        this.Datas = new Datas();
        this.DataSource = this.CreateDataSource();
    }

    private string _name;
    [Browsable(true)]
    [Display(Name = "名称", Order = 0, GroupName = "基础信息")]
    public override string Name
    {
        get { return _name; }
        set
        {
            _name = value;
            RaisePropertyChanged();
        }
    }

    private double _width;
    [DefaultValue(2000.0)]
    [Display(Name = "面板宽度", GroupName = "基础信息", Order = 0)]
    public double Width
    {
        get { return _width; }
        set
        {
            _width = value;
            RaisePropertyChanged();
        }
    }

    private double _height;
    [DefaultValue(1300.0)]
    [Display(Name = "面板高度", GroupName = "基础信息", Order = 0)]
    public double Height
    {
        get { return _height; }
        set
        {
            _height = value;
            RaisePropertyChanged();
        }
    }

    //private ObservableCollection<Node> _nodes = new ObservableCollection<Node>();
    //[JsonIgnore]
    //[XmlIgnore]
    //public ObservableCollection<Node> Nodes
    //{
    //    get { return _nodes; }
    //    set
    //    {
    //        _nodes = value;
    //        RaisePropertyChanged();
    //    }
    //}

    //private Part _selectedPart;
    //[JsonIgnore]
    //[XmlIgnore]
    //public Part SelectedPart
    //{
    //    get { return _selectedPart; }
    //    set
    //    {
    //        _selectedPart = value;
    //        RaisePropertyChanged();
    //    }
    //}


    private IPartData _selectedPartData;
    [JsonIgnore]
    [XmlIgnore]
    public IPartData SelectedPartData
    {
        get { return _selectedPartData; }
        set
        {
            _selectedPartData = value;
            RaisePropertyChanged();
        }
    }


    private ILinkDrawer _linkDrawer = new BrokenLinkDrawer();
    [System.Text.Json.Serialization.JsonIgnore]
    [XmlIgnore]
    public ILinkDrawer LinkDrawer
    {
        get { return _linkDrawer; }
        set
        {
            _linkDrawer = value;
            RaisePropertyChanged();
        }
    }

    private ObservableCollection<ILinkDrawer> _lLinkDrawers = new ObservableCollection<ILinkDrawer>();
    [System.Text.Json.Serialization.JsonIgnore]
    [XmlIgnore]
    [Browsable(false)]
    public ObservableCollection<ILinkDrawer> LinkDrawers
    {
        get { return _lLinkDrawers; }
        set
        {
            _lLinkDrawers = value;
            RaisePropertyChanged("LinkDrawers");
        }
    }

    private ObservableCollection<ILayout> _layouts = new ObservableCollection<ILayout>();
    [System.Text.Json.Serialization.JsonIgnore]
    [XmlIgnore]
    [Browsable(false)]
    public ObservableCollection<ILayout> Layouts
    {
        get { return _layouts; }
        set
        {
            _layouts = value;
            RaisePropertyChanged("Layouts");
        }
    }

    private ILayout _layout = new LocationLayout();
    [JsonIgnore]
    [XmlIgnore]
    public ILayout Layout
    {
        get { return _layout; }
        set
        {
            _layout = value;
            RaisePropertyChanged();
        }
    }

    private Type _nodeType = typeof(Node);
    [JsonIgnore]
    [XmlIgnore]
    public Type NodeType
    {
        get { return _nodeType; }
        set
        {
            _nodeType = value;
            RaisePropertyChanged();
        }
    }

    private Point _location = new Point(1000, 650);
    public Point Location
    {
        get { return _location; }
        set
        {
            _location = value;
            RaisePropertyChanged();
        }
    }


    [Icon(FontIcons.EditMirrored)]
    [Display(Name = "编辑面板", GroupName = "操作", Order = 0, Description = "点击此功能，编辑面板信息")]
    public virtual DisplayCommand EditCommand => new DisplayCommand(async e =>
    {
        await IocMessage.Form.ShowTabEdit(this);
    });

    [Icon(FontIcons.View)]
    [Display(Name = "查看面板", GroupName = "操作", Order = 0, Description = "点击此功能，编辑查看面板信息")]
    public virtual DisplayCommand ShowCommand => new DisplayCommand(async e =>
    {
        await IocMessage.Form.ShowTabEdit(this);
    });

    //[Icon(FontIcons.Cancel)]
    //[Display(Name = "删除", GroupName = "操作", Order = 4, Description = "点击此功能，删除选中的节点、连线或端口")]
    //public virtual DisplayCommand DeleteCommand => new DisplayCommand(async e =>
    //{
    //    if (e is Part part)
    //    {
    //        await IocMessage.Dialog.ShowDeleteDialog(x =>
    //        {
    //            part.Delete();
    //        });
    //    }

    //}, x => x is Part);

    //[Icon(FontIcons.DisconnectDrive)]
    //[Display(Name = "删除选中节点", GroupName = "操作", Order = 4, Description = "点击此功能，删除选中的所有节点")]
    //public virtual DisplayCommand DeleteCheckedCommand => new DisplayCommand(async e =>
    //{
    //    if (e is IEnumerable<Node> nodes)
    //    {
    //        await IocMessage.Dialog.ShowDeleteDialog(x =>
    //        {
    //            foreach (var item in nodes.Where(x => x.IsSelected).ToList())
    //            {
    //                item.Delete();
    //            }
    //        });
    //    }

    //}, x => x is IEnumerable<Node>);

    //[Icon(FontIcons.Delete)]
    //[Display(Name = "清空节点", GroupName = "操作", Order = 5, Description = "点击此功能，删除所有节点、连线和端口")]
    //public virtual DisplayCommand ClearCommand => new DisplayCommand(async e =>
    //{
    //    if (e is IEnumerable<Node> nodes)
    //    {
    //        await IocMessage.Dialog.ShowDeleteAllDialog(x =>
    //        {
    //            foreach (Node item in nodes.ToList())
    //            {
    //                item.Delete();
    //            }
    //            //this.SelectedPart = null;
    //        });
    //    }

    //}, x => x is IEnumerable<Node>);

    //[Icon(FontIcons.AlignCenter)]
    //[System.Text.Json.Serialization.JsonIgnore]
    //[Display(Name = "对齐节点", GroupName = "操作", Order = 5)]
    //public virtual DisplayCommand AlignmentCommand => new DisplayCommand(e =>
    //{
    //    if (e is IEnumerable<Node> nodes)
    //    {
    //        foreach (Node item in nodes)
    //        {
    //            item.AligmentLayout();
    //        }
    //    }
    //}, x => x is IEnumerable<Node>);

    //[System.Text.Json.Serialization.JsonIgnore]

    //[Icon(FontIcons.Previous)]
    //[Display(Name = "上一个节点", GroupName = "操作", Order = 5)]
    //public virtual DisplayCommand ProviewCommand => new DisplayCommand(e =>
    //{
    //    if(e )
    //    this.OnPreivewPart();
    //}, x => this.SelectedPart?.GetPrevious() != null);


    //[System.Text.Json.Serialization.JsonIgnore]
    //[Icon(FontIcons.Next)]
    //[Display(Name = "下一个节点", GroupName = "操作", Order = 5)]
    //public virtual DisplayCommand NextCommand => new DisplayCommand(e =>
    //{
    //    this.OnNextPart();
    //}, x => this.SelectedPart?.GetNext() != null);

    //protected virtual void OnNextPart()
    //{
    //    var find = this.SelectedPart.GetNext();
    //    this.SelectedPart.IsSelected = false;
    //    this.SelectedPart = find;
    //    find.IsSelected = true;
    //}


    public RelayCommand ItemsChangedCommand => new RelayCommand(e =>
    {
        this.OnItemsChanged();
    });

    protected virtual void OnItemsChanged()
    {

    }

    public RelayCommand SelectedPartChangedCommand => new RelayCommand(e =>
    {
        if (e is RoutedEventArgs args && args.Source is Diagram diagram)
            this.SelectedPartData = diagram.SelectedPart?.GetContent<IPartData>();
        this.OnSelectedPartChanged();
    });

    protected virtual void OnSelectedPartChanged()
    {

    }

    public RelayCommand MouseDoubleClickCommand => new RelayCommand(e =>
    {
        this.OnMouseDoubleClick(e);
    });

    protected virtual void OnMouseDoubleClick(object e)
    {
        if (e is MouseButtonEventArgs args && args.OriginalSource is FrameworkElement framework)
        {
            if (framework?.DataContext == null)
                return;
            {
                IocMessage.Form?.ShowTabEdit(framework?.DataContext, x =>
                {

                }, null, x =>
                {
                    //x.UseGroupNames = "数据";
                    x.UseCommand = false;
                    //x.TabNames = new ObservableCollection<string>();
                });
            }
        }
    }

    protected virtual IEnumerable<ILinkDrawer> CreateLinkDrawerSource()
    {
        yield return new BrokenLinkDrawer();
        yield return new LineLinkDrawer();
        yield return new BezierLinkDrawer();
        yield return new ArcLinkDrawer();
    }

    protected virtual IEnumerable<ILayout> CreateLayousSource()
    {
        yield return new LocationLayout();
    }

    public void Clear()
    {
        this.Datas.NodeDatas.Clear();
        this.Datas.LinkDatas.Clear();
    }

    #region - Serializing -

    public Datas Datas { get; } = new Datas();

    IList<INodeData> IDiagramData.NodeDatas => this.DataSource.GetNodeDatas();
    IList<ILinkData> IDiagramData.LinkDatas => this.DataSource.GetLinkDatas();

    private IDiagramDataSource _dataSource;
    [JsonIgnore]
    public IDiagramDataSource DataSource
    {
        get { return _dataSource; }
        set
        {
            _dataSource = value;
            RaisePropertyChanged();
        }
    }

    [OnSerializing]
    protected void OnSerializingMethod(StreamingContext context)
    {
        this.OnSerializing();
    }

    protected virtual void OnSerializing()
    {
        this.Datas.LinkDatas = this.DataSource.GetLinkDatas();
        this.Datas.NodeDatas = this.DataSource.GetNodeDatas();
    }

    [OnDeserialized]
    protected void OnDeserializedMethod(StreamingContext context)
    {
        this.OnDeserialized();
    }

    protected virtual void OnDeserialized()
    {
        this.DataSource = this.CreateDataSource();
    }


    protected IDiagramDataSource CreateDataSource()
    {
        return new DiagramDataSource(this.Datas.NodeDatas, this.Datas.LinkDatas);
    }
    #endregion

    #region - 序列化 -
    public object Clone()
    {
        //  ToDo：用序列化复制
        return this;
        //this.CloneBy();
        //string xml = this.ToXmlString();
        //return CreateFromXml(xml, this.GetType());
        //return Application.Current.Dispatcher.Invoke(() =>
        //  {
        //      //this._layout.DoLayout(this.Nodes.ToArray());
        //      return CreateFromXml(xml, this.GetType());
        //  });
    }

    object ICloneable.Clone()
    {
        throw new NotImplementedException();
    }

    #endregion
}

public class Datas
{
    public List<INodeData> NodeDatas { get; set; } = new List<INodeData>();
    public List<ILinkData> LinkDatas { get; set; } = new List<ILinkData>();
}
