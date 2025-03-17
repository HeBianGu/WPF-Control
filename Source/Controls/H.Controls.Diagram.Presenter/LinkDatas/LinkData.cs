namespace H.Controls.Diagram.Presenter.LinkDatas;

public abstract class LinkDataBase : DisplayBindableBase, ILinkData
{
    public LinkDataBase()
    {
        this.Name = "连线";
        this.ID = Guid.NewGuid().ToString();
    }

    [Browsable(false)]
    public string FromNodeID { get; set; }

    [Browsable(false)]
    public string ToNodeID { get; set; }

    [Browsable(false)]
    public string FromPortID { get; set; }

    [Browsable(false)]
    public string ToPortID { get; set; }

    [XmlIgnore]
    [Display(Name = "恢复默认", GroupName = "操作")]
    public override DisplayCommand LoadDefaultCommand => new DisplayCommand(e =>
    {
        this.LoadDefault();
    });

    [XmlIgnore]
    [Display(Name = "删除", GroupName = "操作")]
    public DisplayCommand DeleteCommand => new DisplayCommand(e =>
    {
        if (e is Part part)
            part.Delete();
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
        if (e is Link node)
        {
            //if (node.GetDiagram().DataContext is IZoomableDiagramData diagram)
            //    diagram.ZoomTo(node);
        }
    });

    [XmlIgnore]
    [Display(Name = "平移定位", GroupName = "操作")]
    public DisplayCommand LocateMoveCommand => new DisplayCommand(e =>
    {
        if (e is Link node)
        {
            //if (node.GetDiagram().DataContext is IZoomableDiagramData diagram)
            //    diagram.PanTo(node);
        }
    });

    [XmlIgnore]
    [Display(Name = "应用到全部", GroupName = "操作")]
    public DisplayCommand ApplyToAllCommand => new DisplayCommand(e =>
    {
        if (e is Link part)
        {
            Diagram diagram = part.GetParent<Diagram>();
            foreach (LinkData item in diagram.Nodes.SelectMany(x => x.GetAllLinks()).Select(x => x.Content).Distinct().OfType<LinkData>())
            {
                this.ApplayStyleTo(item);
            }
        }
    });

    [XmlIgnore]
    [Display(Name = "应用到同类型", GroupName = "操作")]
    public DisplayCommand ApplyToTypeCommand => new DisplayCommand(e =>
    {
        if (e is Link part)
        {
            Diagram diagram = part.GetParent<Diagram>();
            foreach (LinkData item in diagram.Nodes.SelectMany(x => x.GetAllLinks()).Select(x => x.Content).Distinct().OfType<LinkData>().Where(x => x.GetType().IsAssignableFrom(this.GetType())))
            {
                this.ApplayStyleTo(item);
            }
        }
    });

    public abstract void ApplayStyleTo(ILinkData to);
}

public abstract class LinkData : LinkDataBase
{
    public override void ApplayStyleTo(ILinkData to)
    {
        if (to is not LinkData data)
            return;
        data.Stroke = this.Stroke;
        data.StrokeDashArray = this.StrokeDashArray;
        data.StrokeDashCap = this.StrokeDashCap;
        data.StrokeDashOffset = this.StrokeDashOffset;
        data.StrokeEndLineCap = this.StrokeEndLineCap;
        data.StrokeThickness = this.StrokeThickness;
        data.StrokeLineJoin = this.StrokeLineJoin;
        data.StrokeStartLineCap = this.StrokeStartLineCap;
    }

    private SolidColorBrush _stroke;
    [DefaultValue(null)]
    [Display(Name = "连线颜色", GroupName = "常用")]
    public SolidColorBrush Stroke
    {
        get { return _stroke; }
        set
        {
            _stroke = value;
            RaisePropertyChanged();
        }
    }

    private double _strokeThickness = 1;
    [DefaultValue(1.0)]
    [Range(0.1, 10)]
    [Display(Name = "线条宽度", GroupName = "常用")]
    public double StrokeThickness
    {
        get { return _strokeThickness; }
        set
        {
            _strokeThickness = value;
            RaisePropertyChanged();
        }
    }

    private DoubleCollection _strokeDashArray;
    [DefaultValue(null)]
    [Display(Name = "线条虚线", GroupName = "样式")]
    public DoubleCollection StrokeDashArray
    {
        get { return _strokeDashArray; }
        set
        {
            _strokeDashArray = value;
            RaisePropertyChanged();
        }
    }

    private PenLineCap _strokeDashCap;
    [DefaultValue(PenLineCap.Round)]
    [Display(Name = "虚线帽子", GroupName = "样式")]
    public PenLineCap StrokeDashCap
    {
        get { return _strokeDashCap; }
        set
        {
            _strokeDashCap = value;
            RaisePropertyChanged();
        }
    }

    private double _strokeDashOffset;
    [DefaultValue(0.0)]
    [Display(Name = "虚线位移", GroupName = "样式")]
    public double StrokeDashOffset
    {
        get { return _strokeDashOffset; }
        set
        {
            _strokeDashOffset = value;
            RaisePropertyChanged();
        }
    }

    private PenLineCap _strokeStartLineCap;
    [DefaultValue(PenLineCap.Round)]
    [Display(Name = "起始帽子", GroupName = "样式")]
    public PenLineCap StrokeStartLineCap
    {
        get { return _strokeStartLineCap; }
        set
        {
            _strokeStartLineCap = value;
            RaisePropertyChanged();
        }
    }

    private PenLineCap _strokeEndLineCap;
    [DefaultValue(PenLineCap.Round)]
    [Display(Name = "末尾帽子", GroupName = "样式")]
    public PenLineCap StrokeEndLineCap
    {
        get { return _strokeEndLineCap; }
        set
        {
            _strokeEndLineCap = value;
            RaisePropertyChanged();
        }
    }

    private PenLineJoin _strokeLineJoin;
    [DefaultValue(PenLineJoin.Round)]
    [Display(Name = "连接帽子")]
    public PenLineJoin StrokeLineJoin
    {
        get { return _strokeLineJoin; }
        set
        {
            _strokeLineJoin = value;
            RaisePropertyChanged();
        }
    }
}
