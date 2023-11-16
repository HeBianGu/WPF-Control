

using System;
using System.Collections.ObjectModel;

namespace H.Controls.Diagram.Extension
{
    public interface IDiagram : ICloneable
    {
        string ID { get; set; }
        string Name { get; set; }
        string GroupName { get; set; }
        string TypeName { get; set; }
        string TabName { get; set; }
        Part SelectedPart { get; set; }
        void ZoomAll();
        void RefreshRoot();
        ObservableCollection<NodeGroup> NodeGroups { get; set; }
        void Clear();
        ObservableCollection<DiagramThemeGroup> DiagramThemeGroups { get; set; }
        ObservableCollection<FlowableDiagramTemplateNodeData> ReferenceTemplateNodeDatas { get; set; }
    }
}
