using H.Extensions.FontIcon;

namespace H.Controls.Diagram.Presenter.NodeDatas.Base;

public abstract class NodeData : NodeDataBase, INodeData, ITemplate, ILinkDataCreator, IPortDataCreator
{
    public NodeData()
    {
        //NodeTypeAttribute type = GetType().GetCustomAttributes<NodeTypeAttribute>()?.FirstOrDefault();
        //this.Columns = type?.GroupColumns ?? 4;
    }

    [Icon(FontIcons.Delete)]
    [Display(Name = "删除", GroupName = "操作")]
    public DisplayCommand DeleteCommand => new DisplayCommand(async e =>
    {
        if (e is Part part)
            await IocMessage.Dialog.ShowDeleteDialog(x => part.Delete());
    }, x => x is Part);

    [Icon(FontIcons.Refresh)]
    [Display(Name = "恢复默认", GroupName = "操作")]
    public override RelayCommand LoadDefaultCommand => new RelayCommand(e =>
    {
        LoadDefault();
    });

    [Icon(FontIcons.AlignCenter)]
    [Display(Name = "自动对齐", GroupName = "操作")]
    public DisplayCommand AlignmentCommand => new DisplayCommand(e =>
    {
        if (e is Node node)
            node.AligmentLayout();
    }, x => x is Node);

    [Icon(FontIcons.Zoom)]
    [Display(Name = "节点缩放", GroupName = "操作,工具")]
    public DisplayCommand LocateFullCommand => new DisplayCommand(e =>
    {
        if (e is Node node)
            node.GetDiagram().ZoomToFit(node);
    }, x => x is Node);

    [Icon(FontIcons.Forward)]
    [Display(Name = "节点定位", GroupName = "操作,工具")]
    public DisplayCommand LocateMoveCommand => new DisplayCommand(e =>
    {
        if (e is Node node)
        {
            //if (node.GetDiagram().DataContext is IZoomableDiagramData diagram)
            //    diagram.PanTo(node);
        }
    }, x => x is Node);

    [Icon(FontIcons.Color)]
    [Display(Name = "应用样式到全部", GroupName = "操作")]
    public DisplayCommand ApplyToAllCommand => new DisplayCommand(e =>
   {
       if (e is Node node)
       {
           Diagram diagram = node.GetParent<Diagram>();
           diagram.Nodes.ForEach(x =>
           {
               if (x.Content is NodeData nodeData)
                   ApplayStyleTo(nodeData);
           });
       }
   }, x => x is Node);

    [Icon(FontIcons.FontColor)]
    [Display(Name = "应用样式到同类型", GroupName = "操作")]
    public DisplayCommand ApplyToTypeCommand => new DisplayCommand(e =>
   {
       if (e is Node node)
       {
           Diagram diagram = node.GetParent<Diagram>();

           IEnumerable<NodeData> finds = diagram.Nodes.Select(x => x.Content).OfType<NodeData>().Where(x => x.GetType().IsAssignableFrom(GetType()));

           foreach (NodeData item in finds)
           {
               ApplayStyleTo(item);
           }
       }
   }, x => x is Node);

    [Icon(FontIcons.Setting)]
    [Display(Name = "设置", GroupName = "操作")]
    public DisplayCommand SettingCommand => new DisplayCommand(e =>
    {
        IocMessage.Form?.ShowEdit(this);
    });

    [Icon(FontIcons.View)]
    [Display(Name = "详情", GroupName = "操作")]
    public DisplayCommand ViewCommand => new DisplayCommand(e =>
    {
        IocMessage.Form?.ShowView(this);
    });

    private Point _location;
    [Display(Name = "位置坐标", GroupName = "样式")]
    public Point Location
    {
        get { return _location; }
        set
        {
            if (_location == value)
                return;
            _location = value;
            RaisePropertyChanged();
        }
    }

    private bool _isTemplate = true;
    [DefaultValue(true)]
    public bool IsTemplate
    {
        get { return _isTemplate; }
        set
        {
            _isTemplate = value;
            RaisePropertyChanged();
        }
    }

    private double _height;
    [DefaultValue(60)]
    [Display(Name = "高度", GroupName = "样式")]
    public double Height
    {
        get { return _height; }
        set
        {
            _height = value;
            RaisePropertyChanged();
        }
    }

    private double _width;
    [DefaultValue(100)]
    [Display(Name = "宽度", GroupName = "样式")]
    public double Width
    {
        get { return _width; }
        set
        {
            _width = value;
            RaisePropertyChanged();
        }
    }

    private Brush _fill;
    //[PropertyItem(typeof(BrushPropertyItem))]
    [DefaultValue(null)]
    [Display(Name = "背景颜色", GroupName = "常用")]
    public Brush Fill
    {
        get { return _fill; }
        set
        {
            _fill = value;
            RaisePropertyChanged();
        }
    }

    private Brush _stroke;
    //[PropertyItem(typeof(BrushPropertyItem))]
    [DefaultValue(null)]
    [Display(Name = "边框颜色", GroupName = "常用")]
    public Brush Stroke
    {
        get { return _stroke; }
        set
        {
            _stroke = value;
            RaisePropertyChanged();
        }
    }

    private double _strokeThickness = 1.0;
    [DefaultValue(1.0)]
    [Display(Name = "边框宽度", GroupName = "常用")]
    public double StrokeThickness
    {
        get { return _strokeThickness; }
        set
        {
            _strokeThickness = value;
            RaisePropertyChanged();
        }
    }

    private bool _isSelected;
    public bool IsSelected
    {
        get { return _isSelected; }
        set
        {
            _isSelected = value;
            RaisePropertyChanged();
        }
    }

    private double _cornerRadius = 4.0;
    [DefaultValue(4.0)]
    [Display(Name = "圆角", GroupName = "样式")]
    public double CornerRadius
    {
        get { return _cornerRadius; }
        set
        {
            _cornerRadius = value;
            RaisePropertyChanged();
        }
    }

    //private double _blurRadius;
    //[DefaultValue(0)]
    //[Display(Name = "模糊", GroupName = "阴影")]
    //public double BlurRadius
    //{
    //    get { return _blurRadius; }
    //    set
    //    {
    //        _blurRadius = value;
    //        RaisePropertyChanged();
    //    }
    //}

    //private Color _effectColor;
    //[Display(Name = "阴影颜色", GroupName = "阴影")]
    //public Color EffectColor
    //{
    //    get { return _effectColor; }
    //    set
    //    {
    //        _effectColor = value;
    //        RaisePropertyChanged();
    //    }
    //}

    //private double _derection;
    //[Display(Name = "方向", GroupName = "阴影")]
    //public double Direction
    //{
    //    get { return _derection; }
    //    set
    //    {
    //        _derection = value;
    //        RaisePropertyChanged();
    //    }
    //}

    //private double _effectOpacity;
    //[Display(Name = "透明度", GroupName = "阴影")]
    //public double EffectOpacity
    //{
    //    get { return _effectOpacity; }
    //    set
    //    {
    //        _effectOpacity = value;
    //        RaisePropertyChanged();
    //    }
    //}

    //private RenderingBias _renderingBias;
    //[Display(Name = "偏好", GroupName = "阴影")]
    //public RenderingBias RenderingBias
    //{
    //    get { return _renderingBias; }
    //    set
    //    {
    //        _renderingBias = value;
    //        RaisePropertyChanged();
    //    }
    //}

    //private ShadowDepth _shadowDepth;
    //[Display(Name = "深度", GroupName = "阴影")]
    //public ShadowDepth ShadowDepth
    //{
    //    get { return _shadowDepth; }
    //    set
    //    {
    //        _shadowDepth = value;
    //        RaisePropertyChanged();
    //    }
    //}

    public virtual void ApplayStyleTo(INodeData to)
    {
        if (to is not NodeData node)
            return;
        //node.Width = this.Width;
        //node.Height = this.Height;
        node.Fill = this.Fill;
        node.Stroke = this.Stroke;
        node.StrokeThickness = this.StrokeThickness;
        //node.Stretch = this.Stretch;
        //node.BlurRadius = this.BlurRadius;
        //node.EffectColor = this.EffectColor;
        //node.Direction = this.Direction;
        //node.ShadowDepth = this.ShadowDepth;
        //node.RenderingBias = this.RenderingBias;
        //node.ShadowDepth = this.ShadowDepth;
        //node.EffectOpacity = this.EffectOpacity;
    }

    public override object Clone()
    {
        NodeData data = base.Clone() as NodeData;
        data.ID = Guid.NewGuid().ToString();
        return data;
    }

    public virtual ILinkData CreateLinkData()
    {
        return new DefaultLinkData() { FromNodeID = this.ID };
    }

    public virtual IPortData CreatePortData()
    {
        return new TextPortData(this.ID, PortType.Both);
    }
}
