namespace H.Controls.Diagram.Presenter.Provider.Tree;

public abstract class PartTreeNodeBase : TreeNodeBase<Part>
{
    public PartTreeNodeBase(Part model) : base(model)

    {

    }
    public void RefreshSelected()
    {
        this.IsSelected = this.Model.IsSelected;
        if (this.IsSelected == true && this.Parent != null)
            this.Parent.IsExpanded = true;
    }
}
