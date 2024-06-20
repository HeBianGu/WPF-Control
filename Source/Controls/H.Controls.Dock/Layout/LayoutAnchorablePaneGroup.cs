








using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Markup;

namespace H.Controls.Dock.Layout
{
    /// <summary>
    /// Implements an element in the layout model tree that can contain and arrange multiple
    /// <see cref="LayoutAnchorablePane"/> elements in x or y directions, which in turn contain
    /// <see cref="LayoutAnchorable"/> elements.
    /// </summary>
    [ContentProperty(nameof(Children))]
    [Serializable]
    public class LayoutAnchorablePaneGroup : LayoutPositionableGroup<ILayoutAnchorablePane>, ILayoutAnchorablePane, ILayoutOrientableGroup
    {
        #region fields

        private Orientation _orientation;

        #endregion fields

        #region Constructors

        /// <summary>Class constructor</summary>
        public LayoutAnchorablePaneGroup()
        {
        }

        /// <summary>Class constructor <paramref name="firstChild"/> to be inserted into collection of children models.</summary>
        public LayoutAnchorablePaneGroup(LayoutAnchorablePane firstChild)
        {
            this.Children.Add(firstChild);
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets/sets the <see cref="System.Windows.Controls.Orientation"/> of this object.
        /// </summary>
        public Orientation Orientation
        {
            get => _orientation;
            set
            {
                if (value == _orientation) return;
                RaisePropertyChanging(nameof(this.Orientation));
                _orientation = value;
                RaisePropertyChanged(nameof(this.Orientation));
            }
        }

        #endregion Properties

        #region Overrides

        /// <inheritdoc />
        protected override bool GetVisibility() => this.Children.Count > 0 && this.Children.Any(c => c.IsVisible);

        /// <inheritdoc />
        protected override void OnIsVisibleChanged()
        {
            UpdateParentVisibility();
            base.OnIsVisibleChanged();
        }

        /// <inheritdoc />
        protected override void OnDockWidthChanged()
        {
            if (this.DockWidth.IsAbsolute && this.ChildrenCount == 1)
                ((ILayoutPositionableElement)this.Children[0]).DockWidth = this.DockWidth;
            base.OnDockWidthChanged();
        }

        /// <inheritdoc />
        protected override void OnDockHeightChanged()
        {
            if (this.DockHeight.IsAbsolute && this.ChildrenCount == 1)
                ((ILayoutPositionableElement)this.Children[0]).DockHeight = this.DockHeight;
            base.OnDockHeightChanged();
        }

        /// <inheritdoc />
        protected override void OnChildrenCollectionChanged()
        {
            if (this.DockWidth.IsAbsolute && this.ChildrenCount == 1)
                ((ILayoutPositionableElement)this.Children[0]).DockWidth = this.DockWidth;
            if (this.DockHeight.IsAbsolute && this.ChildrenCount == 1)
                ((ILayoutPositionableElement)this.Children[0]).DockHeight = this.DockHeight;
            base.OnChildrenCollectionChanged();
        }

        /// <inheritdoc />
        public override void WriteXml(System.Xml.XmlWriter writer)
        {
            writer.WriteAttributeString(nameof(this.Orientation), this.Orientation.ToString());
            base.WriteXml(writer);
        }

        /// <inheritdoc />
        public override void ReadXml(System.Xml.XmlReader reader)
        {
            if (reader.MoveToAttribute(nameof(this.Orientation)))
                this.Orientation = (Orientation)Enum.Parse(typeof(Orientation), reader.Value, true);
            base.ReadXml(reader);
        }

#if TRACE
        /// <inheritdoc />
        public override void ConsoleDump(int tab)
        {
            System.Diagnostics.Trace.Write(new string(' ', tab * 4));
            System.Diagnostics.Trace.WriteLine(string.Format("AnchorablePaneGroup({0})", this.Orientation));

            foreach (LayoutElement child in this.Children)
                child.ConsoleDump(tab + 1);
        }
#endif

        #endregion Overrides

        #region Private Methods

        private void UpdateParentVisibility()
        {
            if (this.Parent is ILayoutElementWithVisibility parentPane) parentPane.ComputeVisibility();
        }

        #endregion Private Methods
    }
}