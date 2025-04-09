
using H.Styles.Controls;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace H.Controls.Dock.Controls
{
    /// <inheritdoc />
    /// <summary>
    /// Implements a button that is used to display a <see cref="ContextMenuEx"/>
    /// when the user clicks on it
    /// (see templates for <see cref="LayoutAnchorableFloatingWindowControl"/>,
    /// <see cref="AnchorablePaneTitle"/>, and <see cref="LayoutDocumentPaneControl"/>).
    /// </summary>
    /// <seealso cref="ToggleButton"/>
    public class DropDownButton : FontIconToggleButton
    {
        #region Constructors

        public DropDownButton()
        {
            Unloaded += DropDownButton_Unloaded;
        }

        #endregion Constructors

        #region Properties

        #region DropDownContextMenu

        /// <summary><see cref="DropDownContextMenu"/> dependency property.</summary>
        public static readonly DependencyProperty DropDownContextMenuProperty = DependencyProperty.Register(nameof(DropDownContextMenu), typeof(ContextMenu), typeof(DropDownButton),
                new FrameworkPropertyMetadata(null, OnDropDownContextMenuChanged));

        /// <summary>Gets/sets the drop down menu to show up when user click on an anchorable menu pin.</summary>
        [Bindable(true), Description("Gets/sets the drop down menu to show up when user click on an anchorable menu pin."), Category("Menu")]
        public ContextMenu DropDownContextMenu
        {
            get => (ContextMenu)GetValue(DropDownContextMenuProperty);
            set => SetValue(DropDownContextMenuProperty, value);
        }

        /// <summary>Handles changes to the <see cref="DropDownContextMenu"/> property.</summary>
        private static void OnDropDownContextMenuChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) => ((DropDownButton)d).OnDropDownContextMenuChanged(e);

        /// <summary>Provides derived classes an opportunity to handle changes to the <see cref="DropDownContextMenu"/> property.</summary>
        protected virtual void OnDropDownContextMenuChanged(DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue is ContextMenu oldContextMenu && this.IsChecked.GetValueOrDefault())
                oldContextMenu.Closed -= OnContextMenuClosed;
        }

        #endregion DropDownContextMenu

        #region DropDownContextMenuDataContext

        /// <summary><see cref="DropDownContextMenuDataContext"/> dependency property.</summary>
        public static readonly DependencyProperty DropDownContextMenuDataContextProperty = DependencyProperty.Register(nameof(DropDownContextMenuDataContext), typeof(object), typeof(DropDownButton),
                new FrameworkPropertyMetadata(null));

        /// <summary>Gets/sets the DataContext to set for the DropDownContext menu property.</summary>
        [Bindable(true), Description("Gets/sets the DataContext to set for the DropDownContext menu property."), Category("Menu")]
        public object DropDownContextMenuDataContext
        {
            get => GetValue(DropDownContextMenuDataContextProperty);
            set => SetValue(DropDownContextMenuDataContextProperty, value);
        }

        #endregion DropDownContextMenuDataContext

        #endregion Properties

        #region Overrides

        /// <inheritdoc />
        protected override void OnClick()
        {
            if (this.DropDownContextMenu != null)
            {
                //IsChecked = true;
                this.DropDownContextMenu.PlacementTarget = this;
                this.DropDownContextMenu.Placement = PlacementMode.Bottom;
                this.DropDownContextMenu.DataContext = this.DropDownContextMenuDataContext;
                this.DropDownContextMenu.Closed += OnContextMenuClosed;
                this.DropDownContextMenu.IsOpen = true;
            }
            base.OnClick();
        }

        #endregion Overrides

        #region Private Methods

        private void OnContextMenuClosed(object sender, RoutedEventArgs e)
        {
            //Debug.Assert(IsChecked.GetValueOrDefault());
            ContextMenu ctxMenu = sender as ContextMenu;
            ctxMenu.Closed -= OnContextMenuClosed;
            this.IsChecked = false;
        }

        private void DropDownButton_Unloaded(object sender, RoutedEventArgs e)
        {
            // When changing theme, Unloaded event is called, erasing the DropDownContextMenu.
            // Prevent this on theme changes.
            if (this.IsLoaded) this.DropDownContextMenu = null;
        }

        #endregion Private Methods
    }
}