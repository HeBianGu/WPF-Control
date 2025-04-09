global using H.Mvvm.ViewModels.Tree;

namespace H.Controls.Diagram.Presenter.DiagramDatas.Base;

public interface ITreeDigramData
{
    TreeNodeBase<Part> Root { get; set; }
}
