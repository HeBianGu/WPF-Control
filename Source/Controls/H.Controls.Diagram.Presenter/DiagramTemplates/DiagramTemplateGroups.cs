namespace H.Controls.Diagram.Presenter.DiagramTemplates;

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
