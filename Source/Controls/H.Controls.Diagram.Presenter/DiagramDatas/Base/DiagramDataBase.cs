global using H.Controls.Diagram.Layouts;
global using H.Controls.Diagram.Layouts.Base;
global using H.Controls.Diagram.LinkDrawers;
global using H.Controls.Diagram.Presenter.NodeDatas.Card;
global using System.Windows.Input;
global using H.Common.Attributes;
global using H.Controls.Diagram.GraphSource;
global using H.Controls.Form.PropertyItem.Attribute.SourcePropertyItem;
global using H.Controls.Form.PropertyItem.ComboBoxPropertyItems;
global using H.Extensions.FontIcon;
global using H.Mvvm.Commands;
global using H.Services.Message;
global using System.Text.Json.Serialization;
global using H.Services.Message.Dialog;
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
        this._datas = new Datas();
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
    [Display(Name = "连线样式", GroupName = "基础信息")]
    [PropertyNameSourcePropertyItem(typeof(ComboBoxPropertyItem), nameof(LinkDrawers))]
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
    [PropertyNameSourcePropertyItem(typeof(ComboBoxPropertyItem), nameof(Layouts))]
    [Display(Name = "布局方式", GroupName = "基础信息")]
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


    private string _message;
    public string Message
    {
        get { return _message; }
        set
        {
            _message = value;
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
    //    await IocMessage.Dialog.ShowDeleteDialog(x =>
    //    {
    //        part.Delete();
    //    });
    //}, x => x is Part);

    [Icon(FontIcons.DisconnectDrive)]
    [Display(Name = "删除选中节点", GroupName = "操作", Order = 4, Description = "点击此功能，删除选中的所有节点")]
    public virtual DisplayCommand DeleteCheckedCommand => new DisplayCommand(async e =>
    {
        await IocMessage.Dialog.ShowDeleteDialog(x =>
        {
            foreach (var item in this.DataSource.Nodes.Where(x => x.IsSelected).ToList())
            {
                item.Delete();
            }
        });
    }, x => this.DataSource.Nodes.Where(x => x.IsSelected).Count() > 0);

    [Icon(FontIcons.Delete)]
    [Display(Name = "清空节点", GroupName = "操作", Order = 5, Description = "点击此功能，删除所有节点、连线和端口")]
    public virtual DisplayCommand ClearCommand => new DisplayCommand(async e =>
    {
        await IocMessage.Dialog.ShowDeleteAllDialog(x =>
        {
            foreach (Node item in this.DataSource.Nodes.ToList())
            {
                item.Delete();
            }
            this.SelectedPartData = null;
        });
    }, x => this.DataSource.Nodes.Count > 0);

    [Icon(FontIcons.AlignCenter)]
    [System.Text.Json.Serialization.JsonIgnore]
    [Display(Name = "对齐节点", GroupName = "操作", Order = 5)]
    public virtual DisplayCommand AlignmentCommand => new DisplayCommand(e =>
    {
        foreach (Node item in this.DataSource.Nodes)
        {
            item.AligmentLayout();
        }
    }, x => this.DataSource.Nodes.Count > 0);



    public RelayCommand ItemsChangedCommand => new RelayCommand(e =>
    {
        this.OnItemsChanged();
    });

    protected virtual void OnItemsChanged()
    {
        this.InvalidateDatas();
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

    private Datas _datas;
    public Datas Datas
    {
        get
        {
            if (_datas == null)
            {
                _datas = new Datas()
                {
                    NodeDatas = this.DataSource.GetNodeDatas().ToList(),
                    LinkDatas = this.DataSource.GetLinkDatas().ToList()
                };
            }
            return _datas;
        }
    }

    protected void InvalidateDatas()
    {
        this._datas = null;
    }

    IList<INodeData> IDiagramData.NodeDatas => this.Datas.NodeDatas;
    IList<ILinkData> IDiagramData.LinkDatas => this.Datas.LinkDatas;

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
        this.InvalidateDatas();
        //this.Datas.LinkDatas = this.DataSource.GetLinkDatas();
        //this.Datas.NodeDatas = this.DataSource.GetNodeDatas();
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

    protected virtual IDiagramDataSource CreateDataSource()
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
