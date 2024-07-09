using H.Mvvm;

namespace H.Controls.Diagram.Extension
{
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
}
