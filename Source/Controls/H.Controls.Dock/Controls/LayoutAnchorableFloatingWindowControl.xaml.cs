
using H.Controls.Dock.Commands;
using H.Controls.Dock.Converters;
using H.Controls.Dock.Layout;
using Microsoft.Windows.Shell;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;

namespace H.Controls.Dock.Controls
{
    /// <inheritdoc cref="LayoutFloatingWindowControl"/>
    /// <inheritdoc cref="IOverlayWindowHost"/>
    /// <summary>
    /// Class visualizes floating <see cref="LayoutAnchorable"/> (toolwindows) in H.Controls.Dock.
    /// </summary>
    /// <seealso cref="LayoutFloatingWindowControl"/>
    /// <seealso cref="IOverlayWindowHost"/>
    public class LayoutAnchorableFloatingWindowControl : LayoutFloatingWindowControl, IOverlayWindowHost
    {
        #region fields

        private readonly LayoutAnchorableFloatingWindow _model;
        private OverlayWindow _overlayWindow = null;
        private List<IDropArea> _dropAreas = null;

        #endregion fields

        #region Constructors

        static LayoutAnchorableFloatingWindowControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LayoutAnchorableFloatingWindowControl), new FrameworkPropertyMetadata(typeof(LayoutAnchorableFloatingWindowControl)));
        }

        internal LayoutAnchorableFloatingWindowControl(LayoutAnchorableFloatingWindow model, bool isContentImmutable)
           : base(model, isContentImmutable)
        {
            _model = model;
            this.HideWindowCommand = new RelayCommand<object>((p) => OnExecuteHideWindowCommand(p), (p) => CanExecuteHideWindowCommand(p));
            this.CloseWindowCommand = new RelayCommand<object>((p) => OnExecuteCloseWindowCommand(p), (p) => CanExecuteCloseWindowCommand(p));
            Activated += LayoutAnchorableFloatingWindowControl_Activated;
            UpdateThemeResources();
            this.MinWidth = _model.RootPanel.CalculatedDockMinWidth();
            this.MinHeight = _model.RootPanel.CalculatedDockMinHeight();
            if (_model.Root is LayoutRoot root) root.Updated += OnRootUpdated;
            _model.IsVisibleChanged += _model_IsVisibleChanged;
        }

        private void OnRootUpdated(object sender, EventArgs e)
        {
            if (_model?.RootPanel == null) return;
            this.MinWidth = _model.RootPanel.CalculatedDockMinWidth();
            this.MinHeight = _model.RootPanel.CalculatedDockMinHeight();
        }

        private void LayoutAnchorableFloatingWindowControl_Activated(object sender, EventArgs e)
        {
            // Issue similar to: http://avalondock.codeplex.com/workitem/15036
            BindingExpression visibilityBinding = GetBindingExpression(VisibilityProperty);
            if (visibilityBinding == null && this.Visibility == Visibility.Visible) SetVisibilityBinding();
        }

        internal LayoutAnchorableFloatingWindowControl(LayoutAnchorableFloatingWindow model)
            : this(model, false)
        {
        }

        #endregion Constructors

        #region Properties

        /// <inheritdoc />
        public override ILayoutElement Model => _model;

        #region SingleContentLayoutItem

        /// <summary><see cref="SingleContentLayoutItem"/> dependency property.</summary>
        public static readonly DependencyProperty SingleContentLayoutItemProperty = DependencyProperty.Register(nameof(SingleContentLayoutItem), typeof(LayoutItem), typeof(LayoutAnchorableFloatingWindowControl),
                new FrameworkPropertyMetadata(null, OnSingleContentLayoutItemChanged));

        /// <summary>Gets/sets the layout item of the selected content when shown in a single anchorable pane.</summary>
        [Bindable(true), Description("Gets/sets the layout item of the selected content when shown in a single anchorable pane."), Category("Anchorable")]
        public LayoutItem SingleContentLayoutItem
        {
            get => (LayoutItem)GetValue(SingleContentLayoutItemProperty);
            set => SetValue(SingleContentLayoutItemProperty, value);
        }

        /// <summary>Handles changes to the <see cref="SingleContentLayoutItem"/> property.</summary>
        private static void OnSingleContentLayoutItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) => ((LayoutAnchorableFloatingWindowControl)d).OnSingleContentLayoutItemChanged(e);

        /// <summary>Provides derived classes an opportunity to handle changes to the <see cref="SingleContentLayoutItem"/> property.</summary>
        protected virtual void OnSingleContentLayoutItemChanged(DependencyPropertyChangedEventArgs e)
        {
        }

        #endregion SingleContentLayoutItem

        public ICommand HideWindowCommand { get; }

        public ICommand CloseWindowCommand { get; }

        DockingManager IOverlayWindowHost.Manager => _model.Root.Manager;

        #endregion Properties

        #region Public Methods

        /// <inheritdoc />
        public override void EnableBindings()
        {
            _model.PropertyChanged += _model_PropertyChanged;
            SetVisibilityBinding();
            if (this.Model.Root is LayoutRoot layoutRoot) layoutRoot.Updated += OnRootUpdated;
            base.EnableBindings();
        }

        /// <inheritdoc />
        public override void DisableBindings()
        {
            if (this.Model.Root is LayoutRoot layoutRoot) layoutRoot.Updated -= OnRootUpdated;
            BindingOperations.ClearBinding(_model, VisibilityProperty);
            _model.PropertyChanged -= _model_PropertyChanged;
            base.DisableBindings();
        }

        #region IOverlayWindowHost

        bool IOverlayWindowHost.HitTestScreen(Point dragPoint)
        {
            return HitTest(this.TransformToDeviceDPI(dragPoint));
        }

        private bool HitTest(Point dragPoint)
        {
            Rect detectionRect = new Rect(this.PointToScreenDPIWithoutFlowDirection(new Point()), this.TransformActualSizeToAncestor());
            return detectionRect.Contains(dragPoint);
        }

        void IOverlayWindowHost.HideOverlayWindow()
        {
            _dropAreas = null;
            _overlayWindow.Owner = null;
            _overlayWindow.HideDropTargets();
            _overlayWindow.Close();
            _overlayWindow = null;
        }

        IOverlayWindow IOverlayWindowHost.ShowOverlayWindow(LayoutFloatingWindowControl draggingWindow)
        {
            CreateOverlayWindow(draggingWindow);
            _overlayWindow.EnableDropTargets();
            _overlayWindow.Show();
            return _overlayWindow;
        }

        IEnumerable<IDropArea> IOverlayWindowHost.GetDropAreas(LayoutFloatingWindowControl draggingWindow)
        {
            if (_dropAreas != null) return _dropAreas;
            _dropAreas = new List<IDropArea>();
            if (draggingWindow.Model is LayoutDocumentFloatingWindow) return _dropAreas;
            System.Windows.Media.Visual rootVisual = (this.Content as FloatingWindowContentHost).RootVisual;
            foreach (LayoutAnchorablePaneControl areaHost in rootVisual.FindVisualChildren<LayoutAnchorablePaneControl>())
                _dropAreas.Add(new DropArea<LayoutAnchorablePaneControl>(areaHost, DropAreaType.AnchorablePane));
            foreach (LayoutDocumentPaneControl areaHost in rootVisual.FindVisualChildren<LayoutDocumentPaneControl>())
                _dropAreas.Add(new DropArea<LayoutDocumentPaneControl>(areaHost, DropAreaType.DocumentPane));
            return _dropAreas;
        }

        #endregion IOverlayWindowHost

        #endregion Public Methods

        #region Overrides

        /// <inheritdoc />
        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            DockingManager manager = _model.Root.Manager;
            this.Content = manager.CreateUIElementForModel(_model.RootPanel);
            //SetBinding(VisibilityProperty, new Binding("IsVisible") { Source = _model, Converter = new BoolToVisibilityConverter(), Mode = BindingMode.OneWay, ConverterParameter = Visibility.Hidden });

            //Issue: http://avalondock.codeplex.com/workitem/15036
            IsVisibleChanged += LayoutAnchorableFloatingWindowControl_IsVisibleChanged;
            SetBinding(SingleContentLayoutItemProperty, new Binding("Model.SinglePane.SelectedContent") { Source = this, Converter = new LayoutItemFromLayoutModelConverter() });
            _model.PropertyChanged += _model_PropertyChanged;
        }

        /// <inheritdoc />
        protected override void OnClosed(EventArgs e)
        {
            ILayoutRoot root = this.Model.Root;
            if (root != null)
            {
                if (root is LayoutRoot layoutRoot) layoutRoot.Updated -= OnRootUpdated;
                root.Manager.RemoveFloatingWindow(this);
                root.CollectGarbage();
            }
            if (_overlayWindow != null)
            {
                _overlayWindow.Close();
                _overlayWindow = null;
            }
            base.OnClosed(e);
            if (!this.CloseInitiatedByUser) root?.FloatingWindows.Remove(_model);

            // We have to clear binding instead of creating a new empty binding.
            BindingOperations.ClearBinding(_model, VisibilityProperty);

            _model.PropertyChanged -= _model_PropertyChanged;
            _model.IsVisibleChanged -= _model_IsVisibleChanged;
            Activated -= LayoutAnchorableFloatingWindowControl_Activated;
            IsVisibleChanged -= LayoutAnchorableFloatingWindowControl_IsVisibleChanged;
            BindingOperations.ClearBinding(this, VisibilityProperty);
            BindingOperations.ClearBinding(this, SingleContentLayoutItemProperty);
        }

        /// <inheritdoc />
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            bool canHide = this.HideWindowCommand.CanExecute(null);
            if (this.CloseInitiatedByUser && !this.KeepContentVisibleOnClose && !canHide) e.Cancel = true;
            base.OnClosing(e);
        }

        /// <inheritdoc />
        protected override IntPtr FilterMessage(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            switch (msg)
            {
                case Win32Helper.WM_ACTIVATE:
                    LayoutAnchorablePane anchorablePane = _model.Descendents().OfType<LayoutAnchorablePane>()
                        .FirstOrDefault(p => p.ChildrenCount > 0 && p.SelectedContent != null);

                    if (anchorablePane != null)
                    {
                        bool isActive = !(((int)wParam & 0xFFFF) == Win32Helper.WA_INACTIVE);
                        anchorablePane.SelectedContent.IsActive = isActive;

                        handled = true;
                    }

                    break;

                case Win32Helper.WM_NCRBUTTONUP:
                    if (wParam.ToInt32() == Win32Helper.HT_CAPTION)
                    {
                        WindowChrome windowChrome = WindowChrome.GetWindowChrome(this);
                        if (windowChrome != null)
                        {
                            if (OpenContextMenu()) handled = true;
                            windowChrome.ShowSystemMenu = _model.Root.Manager.ShowSystemMenu && !handled;
                        }
                    }
                    break;
            }
            return base.FilterMessage(hwnd, msg, wParam, lParam, ref handled);
        }

        /// <inheritdoc />
        internal override void UpdateThemeResources(Themes.Theme oldTheme = null)
        {
            base.UpdateThemeResources(oldTheme);
            _overlayWindow?.UpdateThemeResources(oldTheme);
        }

        #endregion Overrides

        #region Private Methods

        private void _model_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(LayoutAnchorableFloatingWindow.RootPanel):
                    if (_model.RootPanel == null) InternalClose();
                    break;

                case nameof(LayoutAnchorableFloatingWindow.IsVisible):
                    if (_model.IsVisible != this.IsVisible)
                        this.Visibility = _model.IsVisible ? Visibility.Visible : Visibility.Hidden;
                    break;
            }
        }

        private void _model_IsVisibleChanged(object sender, EventArgs e)
        {
            if (!this.IsVisible && _model.IsVisible) Show();
        }

        private void CreateOverlayWindow(LayoutFloatingWindowControl draggingWindow)
        {
            if (_overlayWindow == null) _overlayWindow = new OverlayWindow(this);

            // Usually, the overlay window is made a child of the main window. However, if the floating
            // window being dragged isn't also a child of the main window (because OwnedByDockingManagerWindow
            // is set to false to allow the parent window to be minimized independently of floating windows)
            if (draggingWindow?.OwnedByDockingManagerWindow ?? true)
                _overlayWindow.Owner = Window.GetWindow(_model.Root.Manager);
            else
                _overlayWindow.Owner = null;

            Rect rectWindow = new Rect(this.PointToScreenDPIWithoutFlowDirection(new Point()), this.TransformActualSizeToAncestor());
            _overlayWindow.Left = rectWindow.Left;
            _overlayWindow.Top = rectWindow.Top;
            _overlayWindow.Width = rectWindow.Width;
            _overlayWindow.Height = rectWindow.Height;
        }

        private bool OpenContextMenu()
        {
            System.Windows.Controls.ContextMenu ctxMenu = _model.Root.Manager.AnchorableContextMenu;
            if (ctxMenu == null || this.SingleContentLayoutItem == null) return false;
            ctxMenu.PlacementTarget = null;
            ctxMenu.Placement = PlacementMode.MousePoint;
            ctxMenu.DataContext = this.SingleContentLayoutItem;
            ctxMenu.IsOpen = true;
            return true;
        }

        private void SetVisibilityBinding()
        {
            SetBinding(
              VisibilityProperty,
              new Binding(nameof(this.IsVisible))
              {
                  Source = _model,
                  Converter = new BoolToVisibilityConverter(),
                  Mode = BindingMode.OneWay,
                  ConverterParameter = Visibility.Hidden
              }
            );
        }

        /// <summary>IsVisibleChanged Event Handler.</summary>
        private void LayoutAnchorableFloatingWindowControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            BindingExpression visibilityBinding = GetBindingExpression(VisibilityProperty);
            if (this.IsVisible && visibilityBinding == null)
                SetBinding(VisibilityProperty, new Binding(nameof(this.IsVisible))
                { Source = _model, Converter = new BoolToVisibilityConverter(), Mode = BindingMode.OneWay, ConverterParameter = Visibility.Hidden });
        }

        #region HideWindowCommand

        private bool CanExecuteHideWindowCommand(object parameter)
        {
            DockingManager manager = this.Model?.Root?.Manager;
            if (manager == null) return false;
            bool canExecute = false;
            foreach (LayoutAnchorable anchorable in this.Model.Descendents().OfType<LayoutAnchorable>().ToArray())
            {
                if (!anchorable.CanHide)
                {
                    canExecute = false;
                    break;
                }
                LayoutAnchorableItem anchorableLayoutItem = manager.GetLayoutItemFromModel(anchorable) as LayoutAnchorableItem;
                if (anchorableLayoutItem?.HideCommand == null || !anchorableLayoutItem.HideCommand.CanExecute(parameter))
                {
                    canExecute = false;
                    break;
                }
                canExecute = true;
            }
            return canExecute;
        }

        private void OnExecuteHideWindowCommand(object parameter)
        {
            DockingManager manager = this.Model.Root.Manager;
            foreach (LayoutAnchorable anchorable in this.Model.Descendents().OfType<LayoutAnchorable>().ToArray())
            {
                LayoutAnchorableItem anchorableLayoutItem = manager.GetLayoutItemFromModel(anchorable) as LayoutAnchorableItem;
                anchorableLayoutItem.HideCommand.Execute(parameter);
            }
            Hide(); // Bring toolwindows inside hidden FloatingWindow back requires restart of app
        }

        #endregion HideWindowCommand

        #region CloseWindowCommand

        private bool CanExecuteCloseWindowCommand(object parameter)
        {
            DockingManager manager = this.Model?.Root?.Manager;
            if (manager == null) return false;
            bool canExecute = false;
            foreach (LayoutAnchorable anchorable in this.Model.Descendents().OfType<LayoutAnchorable>().ToArray())
            {
                if (!anchorable.CanClose)
                {
                    canExecute = false;
                    break;
                }
                LayoutAnchorableItem anchorableLayoutItem = manager.GetLayoutItemFromModel(anchorable) as LayoutAnchorableItem;
                if (anchorableLayoutItem?.CloseCommand == null || !anchorableLayoutItem.CloseCommand.CanExecute(parameter))
                {
                    canExecute = false;
                    break;
                }
                canExecute = true;
            }
            return canExecute;
        }

        private void OnExecuteCloseWindowCommand(object parameter)
        {
            DockingManager manager = this.Model.Root.Manager;
            foreach (LayoutAnchorable anchorable in this.Model.Descendents().OfType<LayoutAnchorable>().ToArray())
            {
                LayoutAnchorableItem anchorableLayoutItem = manager.GetLayoutItemFromModel(anchorable) as LayoutAnchorableItem;
                anchorableLayoutItem.CloseCommand.Execute(parameter);
            }
        }

        #endregion CloseWindowCommand

        #endregion Private Methods
    }
}
