using H.Services.Common;
using System.Collections.ObjectModel;

namespace H.App.VisionMaster;

public interface IVisionProjectItem : IProjectItem
{
    ObservableCollection<IVisionOpenCVDiagramData> DiagramDatas { get; set; }

    IVisionOpenCVDiagramData SelectedDiagramData { get; set; }
}
