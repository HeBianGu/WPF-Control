namespace H.Controls.Diagram.Presenter.DiagramTemplates;

public class DiagramTemplate : DisplayBindableBase, IDiagramTemplate
{
    public DiagramTemplate()
    {

    }

    public DiagramTemplate(IDiagramData diagram)
    {
        this.Diagram = diagram;
        this.Name = diagram.Name;
        this.GroupName = diagram.GroupName;
    }
    private IDiagramData _diagram;
    [Browsable(false)]
    public IDiagramData Diagram
    {
        get { return _diagram; }
        set
        {
            _diagram = value;
            RaisePropertyChanged();
        }
    }

    private string _name;
    /// <summary> 说明  </summary>
    [Display(Name = "名称")]
    public new string Name
    {
        get { return _name; }
        set
        {
            _name = value;
            RaisePropertyChanged();
        }
    }

    //private string _groupName;
    ///// <summary> 说明  </summary>
    //[ReadOnly(true)]
    //[Display(Name = "分组")]
    //public string GroupName
    //{
    //    get { return _groupName; }
    //    set
    //    {
    //        _groupName = value;
    //        RaisePropertyChanged();
    //    }
    //}

    //private string _typeName;
    ///// <summary> 说明  </summary>
    //[ReadOnly(true)]
    //[Display(Name = "类型")]
    //public string TypeName
    //{
    //    get { return _typeName; }
    //    set
    //    {
    //        _typeName = value;
    //        RaisePropertyChanged();
    //    }
    //}

    //private int _order;
    ///// <summary> 说明  </summary> 
    //[Display(Name = "排序")]
    //public int Order
    //{
    //    get { return _order; }
    //    set
    //    {
    //        _order = value;
    //        RaisePropertyChanged();
    //    }
    //}

    //private string _description;
    ///// <summary> 说明  </summary>
    //[Display(Name = "描述")]
    //public string Description
    //{
    //    get { return _description; }
    //    set
    //    {
    //        _description = value;
    //        RaisePropertyChanged();
    //    }
    //}

    //private string _bitmapData;
    //[Browsable(false)]
    //public string BitmapData
    //{
    //    get { return _bitmapData; }
    //    set
    //    {
    //        _bitmapData = value;
    //        RaisePropertyChanged();
    //    }
    //}

    //private string _filePath;
    //[Browsable(false)]
    //public string FilePath
    //{
    //    get { return _filePath; }
    //    set
    //    {
    //        _filePath = value;
    //        RaisePropertyChanged();
    //    }
    //}

    //private int _vip = 3;
    //[Browsable(false)]
    //public int Vip
    //{
    //    get { return _vip; }
    //    set
    //    {
    //        _vip = value;
    //        RaisePropertyChanged();
    //    }
    //}
}
