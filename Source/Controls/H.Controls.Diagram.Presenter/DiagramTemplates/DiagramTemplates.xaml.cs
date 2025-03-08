using H.Extensions.FontIcon;
using System.Text.Json.Serialization;

namespace H.Controls.Diagram.Presenter.DiagramTemplates;

[Icon(FontIcons.Preview)]
[Display(Name = "选择模板")]
public class DiagramTemplates : DisplayBindableBase, IDiagramTempaltes
{
    private ObservableCollection<IDiagramTemplate> _collection = new ObservableCollection<IDiagramTemplate>();
    public ObservableCollection<IDiagramTemplate> Collection
    {
        get { return _collection; }
        set
        {
            _collection = value;
            RaisePropertyChanged();
            this.SelectedDiagramTemplate = value?.FirstOrDefault();
        }
    }

    private IDiagramTemplate _selectedDiagramTemplate;
    [JsonIgnore]
    public IDiagramTemplate SelectedDiagramTemplate
    {
        get { return _selectedDiagramTemplate; }
        set
        {
            _selectedDiagramTemplate = value;
            RaisePropertyChanged();
        }
    }

}
