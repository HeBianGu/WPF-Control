namespace H.Controls.Diagram.Presenter.PortDatas;

public abstract class PortDataBase : DisplayBindableBase, IPortData
{
    public PortDataBase()
    {
        this.Name = this.Name == this.ID ? "端口" : this.Name;
        this.ID = Guid.NewGuid().ToString();
    }

    public PortDataBase(string nodeID) : this()
    {
        this.NodeID = nodeID;
    }

    [Browsable(false)]
    public Dock Dock { get; set; }
    [Browsable(false)]
    public string NodeID { get; set; }
    public PortType PortType { get; set; }
    public Thickness PortMargin { get; set; } = new Thickness(0, 0, 0, 0);

    [XmlIgnore]
    [Display(Name = "删除", GroupName = "操作")]
    public DisplayCommand DeleteCommand => new DisplayCommand(e =>
    {
        if (e is Part part)
            part.Delete();
    });

    [XmlIgnore]
    [Display(Name = "恢复默认", GroupName = "操作")]
    public override DisplayCommand LoadDefaultCommand => new DisplayCommand(e =>
    {
        this.LoadDefault();
    });

    [XmlIgnore]
    [Display(Name = "保存模板", GroupName = "操作")]
    public DisplayCommand SaveAsTemplateCommand => new DisplayCommand(e =>
    {
        if (e is Node node)
        {

        }
    });

    [XmlIgnore]
    [Display(Name = "缩放定位", GroupName = "操作")]
    public DisplayCommand LocateFullCommand => new DisplayCommand(e =>
    {
        if (e is Port node)
        {
            //if (node.GetDiagram().DataContext is IZoomableDiagramData diagram)
            //    diagram.ZoomTo(node);
        }
    });

    [XmlIgnore]
    [Display(Name = "平移定位", GroupName = "操作")]
    public DisplayCommand LocateMoveCommand => new DisplayCommand(e =>
    {
        if (e is Port node)
        {
            //if (node.GetDiagram().DataContext is IZoomableDiagramData diagram)
            //    diagram.PanTo(node);
        }
    });

    [XmlIgnore]
    [Display(Name = "应用到全部", GroupName = "操作")]
    public DisplayCommand ApplyToAllCommand => new DisplayCommand(e =>
    {
        if (e is Port part)
        {
            Diagram diagram = part.GetParent<Diagram>();

            foreach (PortData item in diagram.Nodes.SelectMany(x => x.GetPorts()).Select(x => x.Content).OfType<PortData>())
            {
                this.ApplayStyleTo(item);
            }
        }
    });

    [XmlIgnore]
    [Display(Name = "应用到同类型", GroupName = "操作")]
    public DisplayCommand ApplyToTypeCommand => new DisplayCommand(e =>
    {
        if (e is Port node)
        {
            Diagram diagram = node.GetParent<Diagram>();
            foreach (PortData item in diagram.Nodes.SelectMany(x => x.GetPorts()).Select(x => x.Content).OfType<PortData>().Where(x => x.GetType().IsAssignableFrom(this.GetType())))
            {
                this.ApplayStyleTo(item);
            }
        }
    });

    public virtual void ApplayStyleTo(IPortData node)
    {

    }

    public virtual void InitLink(Link link)
    {

    }

    private bool _hasError;
    /// <summary> 说明  </summary>
    public bool HasError
    {
        get { return _hasError; }
        set
        {
            _hasError = value;
            RaisePropertyChanged();
        }
    }
}
