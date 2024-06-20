
using H.Controls.Dock.Layout;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace H.Controls.Dock.Controls
{
    /// <summary>
    /// This control defines the Title area of a <see cref="LayoutAnchorableControl"/>.
    /// It is used to show a title bar with docking window buttons to let users interact
    /// with a <see cref="LayoutAnchorable"/> via drop down menu click or drag & drop.
    /// </summary>
    public class AnchorablePaneTitle : System.Windows.Controls.Control
    {
        #region fields

        private bool _isMouseDown = false;

        #endregion fields

        #region Constructors

        /// <summary>
        /// Static class constructor
        /// </summary>
        static AnchorablePaneTitle()
        {
            IsHitTestVisibleProperty.OverrideMetadata(typeof(AnchorablePaneTitle), new FrameworkPropertyMetadata(true));
            FocusableProperty.OverrideMetadata(typeof(AnchorablePaneTitle), new FrameworkPropertyMetadata(false));
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AnchorablePaneTitle), new FrameworkPropertyMetadata(typeof(AnchorablePaneTitle)));
        }

        #endregion Constructors

        #region Model

        /// <summary><see cref="Model"/> dependency property.</summary>
        public static readonly DependencyProperty ModelProperty = DependencyProperty.Register(nameof(Model), typeof(LayoutAnchorable), typeof(AnchorablePaneTitle),
                new FrameworkPropertyMetadata(null, _OnModelChanged));

        /// <summary>Gets/sets the <see cref="LayoutAnchorable"/> model attached of this view.</summary>
        [Bindable(true), Description("Gets/sets the LayoutAnchorable model attached of this view."), Category("Anchorable")]
        public LayoutAnchorable Model
        {
            get => (LayoutAnchorable)GetValue(ModelProperty);
            set => SetValue(ModelProperty, value);
        }

        private static void _OnModelChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e) => ((AnchorablePaneTitle)sender).OnModelChanged(e);

        /// <summary>Provides derived classes an opportunity to handle changes to the <see cref="Model"/> property.</summary>
        protected virtual void OnModelChanged(DependencyPropertyChangedEventArgs e)
        {
            if (this.Model != null)
                SetLayoutItem(this.Model?.Root?.Manager?.GetLayoutItemFromModel(this.Model));
            else
                SetLayoutItem(null);
        }

        #endregion Model

        #region LayoutItem

        /// <summary><see cref="LayoutItem"/> Read-Only dependency property.</summary>
        private static readonly DependencyPropertyKey LayoutItemPropertyKey = DependencyProperty.RegisterReadOnly(nameof(LayoutItem), typeof(LayoutItem), typeof(AnchorablePaneTitle),
                new FrameworkPropertyMetadata((LayoutItem)null));

        public static readonly DependencyProperty LayoutItemProperty = LayoutItemPropertyKey.DependencyProperty;

        /// <summary>Gets the <see cref="LayoutItem"/> (<see cref="LayoutAnchorableItem"/> or <see cref="LayoutDocumentItem"/>) attached to this view.</summary>
        [Bindable(true), Description("Gets the LayoutItem (LayoutAnchorableItem or LayoutDocumentItem) attached to this object."), Category("Layout")]
        public LayoutItem LayoutItem => (LayoutItem)GetValue(LayoutItemProperty);

        /// <summary>
        /// Provides a secure method for setting the <see cref="LayoutItem"/> property.
        /// This dependency property indicates the <see cref="H.Controls.Dock.Controls.LayoutItem"/> attached to this tag item.
        /// </summary>
        /// <param name="value">The new value for the property.</param>
        protected void SetLayoutItem(LayoutItem value) => SetValue(LayoutItemPropertyKey, value);

        #endregion LayoutItem

        #region Overrides

        /// <inheritdoc />
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Pressed) _isMouseDown = false;
            base.OnMouseMove(e);
        }

        /// <inheritdoc />
        protected override void OnMouseLeave(MouseEventArgs e)
        {
            base.OnMouseLeave(e);
            if (_isMouseDown && e.LeftButton == MouseButtonState.Pressed)
            {
                LayoutAnchorablePaneControl pane = this.FindVisualAncestor<LayoutAnchorablePaneControl>();
                if (pane != null)
                {
                    LayoutAnchorablePane paneModel = pane.Model as LayoutAnchorablePane;
                    DockingManager manager = paneModel.Root.Manager;
                    manager.StartDraggingFloatingWindowForPane(paneModel);
                }
                else
                {
                    // Start dragging a LayoutAnchorable control that docked/reduced into a SidePanel and
                    // 1) made visible by clicking on to its name in AutoHide mode
                    // 2) user drags the top title bar of the LayoutAnchorable control to drag it out of its current docking position
                    this.Model?.Root?.Manager?.StartDraggingFloatingWindowForContent(this.Model);
                }
            }
            _isMouseDown = false;
        }

        /// <inheritdoc />
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            // Start a drag & drop action for a LayoutAnchorable
            if (e.Handled || this.Model.CanMove == false) return;
            bool attachFloatingWindow = false;
            LayoutAnchorableFloatingWindow parentFloatingWindow = this.Model.FindParent<LayoutAnchorableFloatingWindow>();
            if (parentFloatingWindow != null) attachFloatingWindow = parentFloatingWindow.Descendents().OfType<LayoutAnchorablePane>().Count() == 1;

            if (attachFloatingWindow)
            {
                //the pane is hosted inside a floating window that contains only an anchorable pane so drag the floating window itself
                LayoutFloatingWindowControl floatingWndControl = this.Model.Root.Manager.FloatingWindows.Single(fwc => fwc.Model == parentFloatingWindow);
                floatingWndControl.AttachDrag(false);
            }
            else
                _isMouseDown = true;//normal drag
        }

        /// <inheritdoc />
        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            _isMouseDown = false;
            base.OnMouseLeftButtonUp(e);
            if (this.Model != null) this.Model.IsActive = true;//FocusElementManager.SetFocusOnLastElement(Model);
        }

        #endregion Overrides
    }
}