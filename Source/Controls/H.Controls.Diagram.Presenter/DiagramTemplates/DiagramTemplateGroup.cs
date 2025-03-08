namespace H.Controls.Diagram.Presenter.DiagramTemplates;

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
