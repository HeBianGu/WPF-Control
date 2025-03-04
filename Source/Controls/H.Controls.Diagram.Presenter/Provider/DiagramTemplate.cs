using H.Controls.Diagram.Presenter.DiagramDatas.Base;
using H.Mvvm.ViewModels.Base;

namespace H.Controls.Diagram.Presenter.Provider;

public class DiagramTemplate : DisplayBindableBase, IDiagramTemplate
{
    public DiagramTemplate()
    {

    }

    public DiagramTemplate(IDiagramData diagram)
    {
        this.Diagram = diagram;
        this.Name = diagram.TypeName;
        this.GroupName = diagram.GroupName;
        this.TypeName = diagram.TypeName;
        //this.TabName = diagram.TabName;
    }
    private IDiagramData _diagram;
    /// <summary> 说明  </summary>
    [Browsable(false)]
    //[XmlIgnore]
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


    private string _typeName;
    /// <summary> 说明  </summary>
    [ReadOnly(true)]
    [Display(Name = "类型")]
    public string TypeName
    {
        get { return _typeName; }
        set
        {
            _typeName = value;
            RaisePropertyChanged();
        }
    }


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

    private string _bitmapData;
    [Browsable(false)]
    public string BitmapData
    {
        get { return _bitmapData; }
        set
        {
            _bitmapData = value;
            RaisePropertyChanged();
        }
    }


    private string _filePath;
    [Browsable(false)]
    public string FilePath
    {
        get { return _filePath; }
        set
        {
            _filePath = value;
            RaisePropertyChanged();
        }
    }

    private int _vip = 3;
    [Browsable(false)]
    public int Vip
    {
        get { return _vip; }
        set
        {
            _vip = value;
            RaisePropertyChanged();
        }
    }
}

//public class XmlDiagramTemplateData
//{
//    //public XmlDiagramData DiagramData { get; set; }

//    //public string Name { get; set; }

//    //public XmlClassData Datas;

//    public XmlDiagramTemplateData(DiagramTemplate template)
//    {
//        XmlClassData xmlClassData = new XmlClassData(template);
//        this.Data = xmlClassData;
//    }
//    public XmlDiagramTemplateData()
//    {

//    }

//    public XmlClassData Data { get; set; }
//}



public class DiagramTemplateGroup : BindableBase
{
    public DiagramTemplateGroup(IEnumerable<DiagramTemplate> collection)
    {
        this.Collection = collection.ToObservable();
    }

    public string Name { get; set; }


    private ObservableCollection<DiagramTemplate> _collection = new ObservableCollection<DiagramTemplate>();
    /// <summary> 说明  </summary>
    public ObservableCollection<DiagramTemplate> Collection
    {
        get { return _collection; }
        set
        {
            _collection = value;
            RaisePropertyChanged();
        }
    }


    private DiagramTemplate _selectedItem;
    /// <summary> 说明  </summary>
    public DiagramTemplate SelectedItem
    {
        get { return _selectedItem; }
        set
        {
            _selectedItem = value;
            RaisePropertyChanged();
        }
    }

}

public class DiagramTemplateGroups : BindableBase
{
    public DiagramTemplateGroups(IEnumerable<DiagramTemplateGroup> collection)
    {
        this.Collection = collection.ToObservable();
    }

    public string Name { get; set; }

    private ObservableCollection<DiagramTemplateGroup> _collection = new ObservableCollection<DiagramTemplateGroup>();
    /// <summary> 说明  </summary>
    public ObservableCollection<DiagramTemplateGroup> Collection
    {
        get { return _collection; }
        set
        {
            _collection = value;
            RaisePropertyChanged();
        }
    }
}
