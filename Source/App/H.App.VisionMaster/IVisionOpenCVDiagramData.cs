using H.Controls.Diagram.Presenter.DiagramDatas.Base;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace H.App.VisionMaster;
public interface IVisionOpenCVDiagramData : INodeGroupsDiagramData
{
    ImageSource ResultImageSource { get; set; }

    ObservableCollection<IVisionMessage> Messages { get; set; }
}