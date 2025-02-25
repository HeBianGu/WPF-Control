namespace H.Controls.Diagram.Presenter.NodeDatas.Base;

public abstract class NodeData : NodeDataBase, INodeData, ITemplate, ILinkDataCreator, IPortDataCreator
{
    public NodeData()
    {
        //NodeTypeAttribute type = GetType().GetCustomAttributes<NodeTypeAttribute>()?.FirstOrDefault();
        //this.Columns = type?.GroupColumns ?? 4;
    }

    [XmlIgnore]
    [Display(Name = "删除", GroupName = "操作")]
    public RelayCommand DeleteCommand => new RelayCommand((s, e) =>
    {
        if (e is Part part)
            part.Delete();
    });

    [XmlIgnore]
    [Display(Name = "恢复默认", GroupName = "操作")]
    public override RelayCommand LoadDefaultCommand => new RelayCommand((s, e) =>
    {
        LoadDefault();
    });

    [XmlIgnore]
    [Display(Name = "保存模板", GroupName = "操作")]
    public RelayCommand SaveAsTemplateCommand => new RelayCommand((s, e) =>
    {
        if (e is Node node)
        {

        }
    });

    [XmlIgnore]
    [Display(Name = "自动对齐", GroupName = "操作")]
    public RelayCommand AlignmentCommand => new RelayCommand((s, e) =>
    {
        if (e is Node node)
            node.Aligment();
    });

    [XmlIgnore]
    [Display(Name = "节点缩放", GroupName = "操作,工具")]
    public RelayCommand LocateFullCommand => new RelayCommand((s, e) =>
    {
        if (e is Node node)
        {
            if (node.GetDiagram().DataContext is DiagramDataBase diagram)
                diagram.LocateRect(node);
        }
    });

    [XmlIgnore]
    [Display(Name = "节点定位", GroupName = "操作,工具")]
    public RelayCommand LocateMoveCommand => new RelayCommand((s, e) =>
    {
        if (e is Node node)
        {
            if (node.GetDiagram().DataContext is DiagramDataBase diagram)
                diagram.LocateCenter(node);
        }
    });

    [XmlIgnore]
    [Display(Name = "应用样式到全部", GroupName = "操作")]
    public RelayCommand ApplyToAllCommand => new RelayCommand((s, e) =>
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
   });

    [XmlIgnore]
    [Display(Name = "应用样式到同类型", GroupName = "操作")]
    public RelayCommand ApplyToTypeCommand => new RelayCommand((s, e) =>
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
   });

    [XmlIgnore]
    [Display(Name = "设置", GroupName = "操作")]
    public RelayCommand SettingCommand => new RelayCommand((s, e) =>
    {
        IocMessage.Form.ShowEdit(this);
    });

    [XmlIgnore]
    [Display(Name = "详情", GroupName = "操作")]
    public RelayCommand ViewCommand => new RelayCommand((s, e) =>
    {
        IocMessage.Form.ShowView(this);
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

    [Browsable(false)]
    public int Columns { get; set; }

    [Browsable(false)]
    public bool IsTemplate { get; set; } = true;

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

    private double _strokeThickness = 1;
    [DefaultValue(1)]
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

    private double _cornerRadius;
    [DefaultValue(4)]
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
