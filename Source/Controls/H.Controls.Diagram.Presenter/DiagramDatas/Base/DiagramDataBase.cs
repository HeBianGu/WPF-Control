global using H.Controls.Diagram.Layouts;
global using H.Controls.Diagram.Layouts.Base;
global using H.Controls.Diagram.LinkDrawers;
global using H.Controls.Diagram.Presenter.NodeDatas.Card;
global using System.Windows.Input;
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

    private ObservableCollection<Node> _nodes = new ObservableCollection<Node>();
    [JsonIgnore]
    [XmlIgnore]
    public ObservableCollection<Node> Nodes
    {
        get { return _nodes; }
        set
        {
            _nodes = value;
            RaisePropertyChanged();
        }
    }

    private Part _selectedPart;
    [JsonIgnore]
    [XmlIgnore]
    public Part SelectedPart
    {
        get { return _selectedPart; }
        set
        {
            _selectedPart = value;
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
    [Icon(FontIcons.Refresh)]
    [Display(Name = "默认样式", GroupName = "操作", Order = 6, Description = "点击此功能，恢复所有节点、连线和端口默认样式")]
    public new DisplayCommand LoadDefaultCommand => new DisplayCommand(e =>
    {
        foreach (Node node in this.Nodes)
        {
            IEnumerable<IDefaultable> displayers = node.GetParts().Select(x => x.Content).OfType<IDefaultable>();
            foreach (IDefaultable item in displayers)
            {
                item.LoadDefault();
            }
            if (node.Content is IDefaultable displayer)
                displayer.LoadDefault();
        }
    }, x => this.Nodes.Count > 0);

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

    [Icon(FontIcons.Clear)]
    [Display(Name = "删除", GroupName = "操作", Order = 4, Description = "点击此功能，删除选中的节点、连线或端口")]
    public virtual DisplayCommand DeleteCommand => new DisplayCommand(async e =>
    {
        await IocMessage.Dialog.ShowDeleteDialog(x =>
        {
            this.SelectedPart.Delete();
        });
    }, x => this.SelectedPart != null);

    [Icon(FontIcons.ClearSelection)]
    [Display(Name = "删除选中节点", GroupName = "操作", Order = 4, Description = "点击此功能，删除选中的所有节点")]
    public virtual DisplayCommand DeleteCheckedCommand => new DisplayCommand(async e =>
    {
        await IocMessage.Dialog.ShowDeleteDialog(x =>
        {
            foreach (var item in this.Nodes.Where(x => x.IsSelected).ToList())
            {
                item.Delete();
            }
        });
    }, x => this.SelectedPart != null);

    [Icon(FontIcons.Delete)]
    [Display(Name = "清空节点", GroupName = "操作", Order = 5, Description = "点击此功能，删除所有节点、连线和端口")]
    public virtual DisplayCommand ClearCommand => new DisplayCommand(async e =>
    {
        await IocMessage.Dialog.ShowDeleteAllDialog(x =>
             {
                 this.Clear();
             });
    }, x => this.Nodes.Count > 0);

    [Icon(FontIcons.AlignCenter)]
    [System.Text.Json.Serialization.JsonIgnore]
    [Display(Name = "对齐节点", GroupName = "操作", Order = 5)]
    public virtual DisplayCommand AlignmentCommand => new DisplayCommand(e =>
    {
        this.Aligment();
    }, x => this.Nodes.Count > 0);

    [System.Text.Json.Serialization.JsonIgnore]

    [Icon(FontIcons.Previous)]
    [Display(Name = "上一个节点", GroupName = "操作", Order = 5)]
    public virtual DisplayCommand ProviewCommand => new DisplayCommand(e =>
    {
        this.OnPreivewPart();
    }, x => this.SelectedPart?.GetPrevious() != null);

    protected virtual void OnPreivewPart()
    {
        var find = this.SelectedPart.GetPrevious();
        this.SelectedPart.IsSelected = false;
        this.SelectedPart = find;
        find.IsSelected = true;
    }

    [System.Text.Json.Serialization.JsonIgnore]

    [Icon(FontIcons.Next)]
    [Display(Name = "下一个节点", GroupName = "操作", Order = 5)]
    public virtual DisplayCommand NextCommand => new DisplayCommand(e =>
    {
        this.OnNextPart();
    }, x => this.SelectedPart?.GetNext() != null);

    protected virtual void OnNextPart()
    {
        var find = this.SelectedPart.GetNext();
        this.SelectedPart.IsSelected = false;
        this.SelectedPart = find;
        find.IsSelected = true;
    }

    public virtual void Aligment()
    {
        foreach (Node item in this.Nodes)
        {
            item.AligmentLayout();
        }
    }

    public RelayCommand ItemsChangedCommand => new RelayCommand(e =>
    {
        this.OnItemsChanged();
    });

    protected virtual void OnItemsChanged()
    {

    }

    public RelayCommand SelectedPartChangedCommand => new RelayCommand(e =>
    {
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
        foreach (Node item in this.Nodes.ToList())
        {
            item.Delete();
        }
        this.SelectedPart = null;
    }

    #region - Serializing -

    public Datas Datas { get; } = new Datas();

    //List<INodeData> IDiagramSerializable.NodeDatas { get; set; }
    //List<ILinkData> IDiagramSerializable.LinkDatas { get; set; }

    //protected List<INodeData> NodeDatas = new List<INodeData>();
    //protected List<ILinkData> LinkDatas = new List<ILinkData>();
    protected virtual IEnumerable<Node> LoadToNodes(IEnumerable<INodeData> nodeDatas, IEnumerable<ILinkData> linkDatas)
    {
        return nodeDatas.LoadToNodes(linkDatas);
    }

    protected virtual Tuple<IEnumerable<INodeData>, IEnumerable<ILinkData>> SaveToDatas(IEnumerable<Node> nodes)
    {
        return nodes.SaveToDatas();
    }

    [OnSerializing]
    protected void OnSerializingMethod(StreamingContext context)
    {
        this.OnSerializing();
    }

    protected virtual void OnSerializing()
    {
        var tuples = this.SaveToDatas(this.Nodes);
        this.Datas.LinkDatas = tuples.Item2.ToList();
        this.Datas.NodeDatas = tuples.Item1.ToList();
    }

    [OnDeserialized]
    protected void OnDeserializedMethod(StreamingContext context)
    {
        this.OnDeserialized();
    }

    protected virtual void OnDeserialized()
    {
        this.Nodes = LoadToNodes(this.Datas.NodeDatas, this.Datas.LinkDatas).ToObservable();
    }
    #endregion

    #region - 序列化 -

    //public string ToXmlString()
    //{
    //    //XmlDocument xmlDoc = XmlableSerializor.Instance.SaveAs(this);
    //    //return xmlDoc.OuterXml;
    //    return null;
    //}

    //public static T CreateFromXml<T>(string xml) where T : class, IDiagramData
    //{
    //    T instance = CreateFromXml(xml, typeof(T)) as T;
    //    return instance;
    //}

    //public static IDiagramData CreateFromXml(string xml, Type type)
    //{
    //    IDiagramData instance = Application.Current.Dispatcher.Invoke(() =>
    //    {
    //        return Activator.CreateInstance(type) as IDiagramData;
    //    });

    //    XmlDocument xmlDoc = new XmlDocument();
    //    xmlDoc.LoadXml(xml);
    //    //XmlableSerializor.Instance.Load(xmlDoc, instance);
    //    return instance;
    //}

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

    //public void FromXml(XmlElement xmlEle, XmlDocument cnt, Func<PropertyInfo, object, bool> match = null)
    //{
    //    //XmlDiagramData data = new XmlDiagramData();
    //    //XmlableSerializor.Instance.FromXml(xmlEle, this, cnt);

    //    //Application.Current.Dispatcher.Invoke(() =>
    //    //{
    //    //    XmlableSerializor.Instance.FromXml(xmlEle, data, cnt);
    //    //    var nodeDatas = data.Nodes.ToList();
    //    //    var linkDatas = data.Links.ToList();
    //    //    this.Nodes = this.LoadNodes(nodeDatas, linkDatas).ToObservable();
    //    //});

    //    //foreach (var file in data.ReferenceTemplates)
    //    //{
    //    //    if (!File.Exists(file.Value))
    //    //    {
    //    //        this.ReferenceTemplateNodeDatas.Add(new FlowableDiagramTemplateNodeData() { FilePath = file.Value });
    //    //        continue;
    //    //    }
    //    //    DiagramTemplate template = XmlableSerializor.Instance.Load<DiagramTemplate>(file.Value);
    //    //    var nodeData = new FlowableDiagramTemplateNodeData(template);
    //    //    this.ReferenceTemplateNodeDatas.Add(nodeData);
    //    //}
    //}

    //public void ToXml(XmlElement xmlEle, XmlDocument cnt, Func<PropertyInfo, object, bool> match = null)
    //{
    //    //XmlableSerializor.Instance.ToXml(xmlEle, this, this.GetType().Name, cnt, null, false);
    //    //XmlDiagramData data = new XmlDiagramData();
    //    //var ns = Application.Current.Dispatcher.Invoke(() =>
    //    //{
    //    //    return this.Nodes.ToList();
    //    //});

    //    //DiagramDataSourceConverter converter = new DiagramDataSourceConverter(ns);
    //    //var datas = this.SaveDatas(ns);
    //    //var links = datas.Item2;
    //    //var nodes = datas.Item1;

    //    //foreach (var node in nodes)
    //    //{
    //    //    data.Nodes.Add(node);
    //    //}
    //    //foreach (var link in links)
    //    //{
    //    //    data.Links.Add(link);
    //    //}

    //    //data.ReferenceTemplates = this.ReferenceTemplateNodeDatas.Select(x => new XmlStringData(x.FilePath)).ToList();
    //    //XmlableSerializor.Instance.ToXml(xmlEle, data, this.GetType().Name, cnt, null, false);
    //}

    #endregion
}

public class Datas
{
    public List<INodeData> NodeDatas { get; set; } = new List<INodeData>();
    public List<ILinkData> LinkDatas { get; set; } = new List<ILinkData>();
}
