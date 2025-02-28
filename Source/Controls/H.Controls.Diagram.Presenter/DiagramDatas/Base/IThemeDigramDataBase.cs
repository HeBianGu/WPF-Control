namespace H.Controls.Diagram.Presenter.DiagramDatas.Base;

public interface IThemeDigramDataBase : IDiagramData
{
    ObservableCollection<DiagramThemeGroup> DiagramThemeGroups { get; set; }
}
