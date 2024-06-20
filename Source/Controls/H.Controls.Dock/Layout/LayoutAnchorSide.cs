








using System;
using System.Windows.Markup;

namespace H.Controls.Dock.Layout
{
    /// <summary>
    /// Implements the viewmodel for a a side element (left, right, top, bottom) in H.Controls.Dock's
    /// visual root of the <see cref="DockingManager"/>.
    /// </summary>
    [ContentProperty(nameof(Children))]
    [Serializable]
    public class LayoutAnchorSide : LayoutGroup<LayoutAnchorGroup>
    {
        #region fields

        private AnchorSide _side;

        #endregion fields

        #region Properties

        /// <summary>Gets the side (top, bottom, left, right) that this layout is anchored in the layout.</summary>
        public AnchorSide Side
        {
            get => _side;
            private set
            {
                if (value == _side) return;
                RaisePropertyChanging(nameof(this.Side));
                _side = value;
                RaisePropertyChanged(nameof(this.Side));
            }
        }

        #endregion Properties

        #region Overrides

        /// <inheritdoc />
        protected override bool GetVisibility() => this.Children.Count > 0;

        /// <inheritdoc />
        protected override void OnParentChanged(ILayoutContainer oldValue, ILayoutContainer newValue)
        {
            base.OnParentChanged(oldValue, newValue);
            UpdateSide();
        }

        #endregion Overrides

        #region Private Methods

        private void UpdateSide()
        {
            if (this == this.Root.LeftSide) this.Side = AnchorSide.Left;
            else if (this == this.Root.TopSide) this.Side = AnchorSide.Top;
            else if (this == this.Root.RightSide) this.Side = AnchorSide.Right;
            else if (this == this.Root.BottomSide) this.Side = AnchorSide.Bottom;
        }

        #endregion Private Methods
    }
}