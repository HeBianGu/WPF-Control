
using H.Controls.Dock.Commands;
using H.Controls.Dock.Layout;
using Microsoft.Windows.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace H.Controls.Dock.Controls
{
    /// <summary>
    /// Implements a floating window control that can host other controls
    /// (<see cref="LayoutAnchorableControl"/>, <see cref="LayoutDocumentControl"/>)
    /// and be dragged (independently of the <see cref="DockingManager"/>) around the screen.
    /// </summary>
    public class LayoutDocumentFloatingWindowControl : LayoutFloatingWindowControl, IOverlayWindowHost
    {
        #region fields

        private readonly LayoutDocumentFloatingWindow _model;
        private List<IDropArea> _dropAreas = null;

        #endregion fields

        #region Constructors

        /// <summary>Static class constructor</summary>
        static LayoutDocumentFloatingWindowControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LayoutDocumentFloatingWindowControl), new FrameworkPropertyMetadata(typeof(LayoutDocumentFloatingWindowControl)));
        }

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="model"></param>
        /// <param name="isContentImmutable"></param>
        internal LayoutDocumentFloatingWindowControl(LayoutDocumentFloatingWindow model, bool isContentImmutable)
            : base(model, isContentImmutable)
        {
            _model = model;
            this.HideWindowCommand = new RelayCommand<object>(OnExecuteHideWindowCommand, CanExecuteHideWindowCommand);
            this.CloseWindowCommand = new RelayCommand<object>(OnExecuteCloseWindowCommand, CanExecuteCloseWindowCommand);
            Closed += (sender, args) => { this.Owner?.Focus(); };
            UpdateThemeResources();
        }

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="model"></param>
        internal LayoutDocumentFloatingWindowControl(LayoutDocumentFloatingWindow model)
            : this(model, false)
        {
        }

        #endregion Constructors

        #region Overrides

        /// <inheritdoc />
        public override ILayoutElement Model => _model;

        #region SingleContentLayoutItem

        /// <summary><see cref="SingleContentLayoutItem"/> dependency property.</summary>
        public static readonly DependencyProperty SingleContentLayoutItemProperty = DependencyProperty.Register(nameof(SingleContentLayoutItem), typeof(LayoutItem), typeof(LayoutDocumentFloatingWindowControl),
                new FrameworkPropertyMetadata(null, OnSingleContentLayoutItemChanged));

        /// <summary>
        /// Gets or sets the <see cref="SingleContentLayoutItem"/> property.  This dependency property
        /// indicates the layout item of the selected content when is shown a single document pane.
        /// </summary>
        public LayoutItem SingleContentLayoutItem
        {
            get => (LayoutItem)GetValue(SingleContentLayoutItemProperty);
            set => SetValue(SingleContentLayoutItemProperty, value);
        }

        /// <summary>Handles changes to the <see cref="SingleContentLayoutItem"/> property.</summary>
        private static void OnSingleContentLayoutItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) => ((LayoutDocumentFloatingWindowControl)d).OnSingleContentLayoutItemChanged(e);

        /// <summary>Provides derived classes an opportunity to handle changes to the <see cref="SingleContentLayoutItem"/> property.</summary>
        protected virtual void OnSingleContentLayoutItemChanged(DependencyPropertyChangedEventArgs e)
        {
        }

        #endregion SingleContentLayoutItem

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            DockingManager manager = _model.Root.Manager;
            this.Content = manager.CreateUIElementForModel(_model.RootPanel);
            // TODO IsVisibleChanged
            //SetBinding(SingleContentLayoutItemProperty, new Binding("Model.SinglePane.SelectedContent") { Source = this, Converter = new LayoutItemFromLayoutModelConverter() });
            _model.RootPanel.ChildrenCollectionChanged += RootPanelOnChildrenCollectionChanged;
        }

        private void Model_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(LayoutDocumentFloatingWindow.RootPanel) && _model.RootPanel == null) InternalClose();
        }

        private void ActiveOfSinglePane(bool isActive)
        {
            LayoutDocumentPane layoutDocumentPane = _model.Descendents().OfType<LayoutDocumentPane>()
                .FirstOrDefault(p => p.ChildrenCount > 0 && p.SelectedContent != null);

            if (layoutDocumentPane != null)
            {
                layoutDocumentPane.SelectedContent.IsActive = isActive;
            }
            // When the floating tool window is mixed with the floating document window
            // and the document pane in the floating document window is dragged out.

            // Only the Tool panes is left in the floating document window.
            // The Children Count is greater than 0 and the Selected Content is null.

            // Then we only need to activate the last active content.
            else
            {
                ActiveLastActivationOfItems(isActive);
            }
        }

        private LayoutDocumentPaneControl FindDocumentPaneControlByMousePoint()
        {
            Point mousePosition = Win32Helper.GetMousePosition();
            System.Windows.Media.Visual rootVisual = ((FloatingWindowContentHost)this.Content).RootVisual;
            IEnumerable<LayoutDocumentPaneControl> areaHosts = rootVisual.FindVisualChildren<LayoutDocumentPaneControl>();

            foreach (LayoutDocumentPaneControl areaHost in areaHosts)
            {
                Rect area = areaHost.GetScreenArea();
                Point pos = areaHost.TransformFromDeviceDPI(mousePosition);
                bool b = area.Contains(pos);

                if (b)
                {
                    return areaHost;
                }
            }

            return null;
        }

        private void ActiveLastActivationOfPane(LayoutDocumentPane model)
        {
            if (model.Children.Count > 0)
            {
                int index = 0;
                if (model.Children.Count > 1)
                {
                    DateTime? tmTimeStamp = model.Children[0].LastActivationTimeStamp;
                    for (int i = 1; i < model.Children.Count; i++)
                    {
                        LayoutContent item = model.Children[i];
                        if (item.LastActivationTimeStamp > tmTimeStamp)
                        {
                            tmTimeStamp = item.LastActivationTimeStamp;
                            index = i;
                        }
                    }
                }

                model.SelectedContentIndex = index;
            }
        }

        private void ActiveLastActivationOfItems(bool isActive)
        {
            List<LayoutContent> items = _model.Descendents().OfType<LayoutContent>().ToList();
            if (items.Count > 0)
            {
                int index = 0;
                if (items.Count > 1)
                {
                    DateTime? tmpTimeStamp2 = items[0].LastActivationTimeStamp;
                    for (int i = 1; i < items.Count; i++)
                    {
                        LayoutContent item = items[i];
                        if (item.LastActivationTimeStamp > tmpTimeStamp2)
                        {
                            tmpTimeStamp2 = item.LastActivationTimeStamp;
                            index = i;
                        }
                    }
                }

                items[index].IsActive = isActive;
            }
        }

        private void ActiveOfMultiPane(bool isActive)
        {
            if (isActive)
            {
                LayoutDocumentPaneControl documentPane = FindDocumentPaneControlByMousePoint();
                if (documentPane != null)
                {
                    LayoutDocumentPane model = (LayoutDocumentPane)documentPane.Model;
                    if (model.SelectedContent != null)
                    {
                        model.SelectedContent.IsActive = true;
                        return;
                    }
                    else
                    {
                        ActiveLastActivationOfPane(model);
                        return;
                    }
                }
            }
            ActiveLastActivationOfItems(isActive);
        }

        /// <inheritdoc />
        protected override IntPtr FilterMessage(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            switch (msg)
            {
                case Win32Helper.WM_ACTIVATE:
                    bool isInactive = ((int)wParam & 0xFFFF) == Win32Helper.WA_INACTIVE;
                    if (_model.IsSinglePane)
                    {
                        ActiveOfSinglePane(!isInactive);
                    }
                    else
                    {
                        ActiveOfMultiPane(!isInactive);
                    }

                    handled = true;
                    break;

                case Win32Helper.WM_NCRBUTTONUP:
                    if (wParam.ToInt32() == Win32Helper.HT_CAPTION)
                    {
                        WindowChrome windowChrome = WindowChrome.GetWindowChrome(this);
                        if (windowChrome != null)
                        {
                            if (OpenContextMenu())
                                handled = true;

                            if (_model.Root.Manager.ShowSystemMenu)
                                windowChrome.ShowSystemMenu = !handled;
                            else
                                windowChrome.ShowSystemMenu = false;
                        }
                    }
                    break;

                case Win32Helper.WM_CLOSE:
                    if (this.CloseInitiatedByUser)
                    {
                        // We want to force the window to go through our standard logic for closing.
                        // So, if the window close is initiated outside of our code (such as from the taskbar),
                        // we cancel that close and trigger our close logic instead.
                        this.CloseWindowCommand.Execute(null);
                        handled = true;
                    }
                    break;
            }
            return base.FilterMessage(hwnd, msg, wParam, lParam, ref handled);
        }

        /// <inheritdoc />
        protected override void OnClosed(EventArgs e)
        {
            ILayoutRoot root = this.Model.Root;
            // MK sometimes root is null, prevent crash, or should it always be set??
            if (root != null)
            {
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
            _model.PropertyChanged -= Model_PropertyChanged;
        }

        #endregion Overrides

        #region Private Methods

        private void RootPanelOnChildrenCollectionChanged(object sender, EventArgs e)
        {
            if (_model.RootPanel == null || _model.RootPanel.Children.Count == 0) InternalClose();
        }

        private bool OpenContextMenu()
        {
            System.Windows.Controls.ContextMenu ctxMenu = _model.Root.Manager.DocumentContextMenu;
            if (ctxMenu == null || this.SingleContentLayoutItem == null) return false;
            ctxMenu.PlacementTarget = null;
            ctxMenu.Placement = PlacementMode.MousePoint;
            ctxMenu.DataContext = this.SingleContentLayoutItem;
            ctxMenu.IsOpen = true;
            return true;
        }

        /// <inheritdoc />
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            // TODO
            if (this.CloseInitiatedByUser && !this.KeepContentVisibleOnClose)
            {
                e.Cancel = true;
                //_model.Descendents().OfType<LayoutDocument>().ToArray().ForEach<LayoutDocument>((a) => a.Hide());
            }
            base.OnClosing(e);
        }

        bool IOverlayWindowHost.HitTestScreen(Point dragPoint)
        {
            return HitTest(this.TransformToDeviceDPI(dragPoint));
        }

        private bool HitTest(Point dragPoint)
        {
            Rect detectionRect = new Rect(this.PointToScreenDPIWithoutFlowDirection(new Point()), this.TransformActualSizeToAncestor());
            return detectionRect.Contains(dragPoint);
        }

        DockingManager IOverlayWindowHost.Manager => _model.Root.Manager;

        private OverlayWindow _overlayWindow = null;

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

        IOverlayWindow IOverlayWindowHost.ShowOverlayWindow(LayoutFloatingWindowControl draggingWindow)
        {
            CreateOverlayWindow(draggingWindow);
            _overlayWindow.EnableDropTargets();
            _overlayWindow.Show();
            return _overlayWindow;
        }

        public void HideOverlayWindow()
        {
            _dropAreas = null;
            _overlayWindow.Owner = null;
            _overlayWindow.HideDropTargets();
            _overlayWindow.Close();
            _overlayWindow = null;
        }

        public IEnumerable<IDropArea> GetDropAreas(LayoutFloatingWindowControl draggingWindow)
        {
            if (_dropAreas != null) return _dropAreas;
            _dropAreas = new List<IDropArea>();
            bool isDraggingDocuments = draggingWindow.Model is LayoutDocumentFloatingWindow;

            // Determine if floatingWindow is configured to dock as document or not
            bool dockAsDocument = true;
            if (!isDraggingDocuments)
            {
                if (draggingWindow.Model is LayoutAnchorableFloatingWindow)
                {
                    foreach (LayoutAnchorable item in GetAnchorableInFloatingWindow(draggingWindow))
                    {
                        if (item.CanDockAsTabbedDocument != false) continue;
                        dockAsDocument = false;
                        break;
                    }
                }
            }

            System.Windows.Media.Visual rootVisual = ((FloatingWindowContentHost)this.Content).RootVisual;

            foreach (LayoutAnchorablePaneControl areaHost in rootVisual.FindVisualChildren<LayoutAnchorablePaneControl>())
                _dropAreas.Add(new DropArea<LayoutAnchorablePaneControl>(areaHost, DropAreaType.AnchorablePane));

            if (dockAsDocument)
            {
                foreach (LayoutDocumentPaneControl areaHost in rootVisual.FindVisualChildren<LayoutDocumentPaneControl>())
                {
                    if ((areaHost is LayoutDocumentPaneControl) == true)
                        _dropAreas.Add(new DropArea<LayoutDocumentPaneControl>(areaHost, DropAreaType.DocumentPane));
                }
            }

            return _dropAreas;
        }

        /// <summary>
        /// Finds all <see cref="LayoutAnchorable"/> objects (tool windows) within a
        /// <see cref="LayoutFloatingWindow"/> (if any) and return them.
        /// </summary>
        /// <param name="draggingWindow"></param>
        /// <returns></returns>
        private IEnumerable<LayoutAnchorable> GetAnchorableInFloatingWindow(LayoutFloatingWindowControl draggingWindow)
        {
            if (!(draggingWindow.Model is LayoutAnchorableFloatingWindow layoutAnchorableFloatingWindow)) yield break;
            //big part of code for getting type

            if (layoutAnchorableFloatingWindow.SinglePane is LayoutAnchorablePane layoutAnchorablePane && layoutAnchorableFloatingWindow.IsSinglePane && layoutAnchorablePane.SelectedContent != null)
            {
                LayoutAnchorable layoutAnchorable = ((LayoutAnchorablePane)layoutAnchorableFloatingWindow.SinglePane).SelectedContent as LayoutAnchorable;
                yield return layoutAnchorable;
            }
            else
                foreach (LayoutAnchorable item in GetLayoutAnchorable(layoutAnchorableFloatingWindow.RootPanel))
                    yield return item;
        }

        /// <summary>
        /// Finds all <see cref="LayoutAnchorable"/> objects (toolwindows) within a
        /// <see cref="LayoutAnchorablePaneGroup"/> (if any) and return them.
        /// </summary>
        /// <param name="layoutAnchPaneGroup"></param>
        /// <returns>All the anchorable items found.</returns>
        /// <seealso cref="LayoutAnchorable"/>
        /// <seealso cref="LayoutAnchorablePaneGroup"/>
        internal IEnumerable<LayoutAnchorable> GetLayoutAnchorable(LayoutAnchorablePaneGroup layoutAnchPaneGroup)
        {
            if (layoutAnchPaneGroup == null) yield break;
            foreach (LayoutAnchorable anchorable in layoutAnchPaneGroup.Descendents().OfType<LayoutAnchorable>())
                yield return anchorable;
        }

        #region HideWindowCommand

        public ICommand HideWindowCommand { get; }

        private bool CanExecuteHideWindowCommand(object parameter)
        {
            ILayoutRoot root = this.Model?.Root;
            DockingManager manager = root?.Manager;
            if (manager == null) return false;

            // TODO check CanHide of anchorables
            bool canExecute = false;
            foreach (LayoutContent content in this.Model.Descendents().OfType<LayoutContent>().ToArray())
            {
                if ((content is LayoutAnchorable anchorable && !anchorable.CanHide) || !content.CanClose)
                {
                    canExecute = false;
                    break;
                }

                //if (!(manager.GetLayoutItemFromModel(content) is LayoutAnchorableItem layoutAnchorableItem) ||
                //	 layoutAnchorableItem.HideCommand == null ||
                //	 !layoutAnchorableItem.HideCommand.CanExecute(parameter))
                //{
                //	canExecute = false;
                //	break;
                //}
                if (!(manager.GetLayoutItemFromModel(content) is LayoutItem layoutItem) || layoutItem.CloseCommand == null || !layoutItem.CloseCommand.CanExecute(parameter))
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
            foreach (LayoutContent anchorable in this.Model.Descendents().OfType<LayoutContent>().ToArray())
            {
                //if (manager.GetLayoutItemFromModel(anchorable) is LayoutAnchorableItem layoutAnchorableItem) layoutAnchorableItem.HideCommand.Execute(parameter);
                //else
                if (manager.GetLayoutItemFromModel(anchorable) is LayoutItem layoutItem) layoutItem.CloseCommand.Execute(parameter);
            }
        }

        #endregion HideWindowCommand

        #region CloseWindowCommand

        public ICommand CloseWindowCommand { get; }

        private bool CanExecuteCloseWindowCommand(object parameter)
        {
            DockingManager manager = this.Model?.Root?.Manager;
            if (manager == null) return false;

            bool canExecute = false;
            foreach (LayoutDocument document in this.Model.Descendents().OfType<LayoutDocument>().ToArray())
            {
                if (!document.CanClose)
                {
                    canExecute = false;
                    break;
                }

                if (!(manager.GetLayoutItemFromModel(document) is LayoutDocumentItem documentLayoutItem) || documentLayoutItem.CloseCommand == null || !documentLayoutItem.CloseCommand.CanExecute(parameter))
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
            foreach (LayoutDocument document in this.Model.Descendents().OfType<LayoutDocument>().ToArray())
            {
                LayoutDocumentItem documentLayoutItem = manager.GetLayoutItemFromModel(document) as LayoutDocumentItem;
                documentLayoutItem?.CloseCommand.Execute(parameter);
            }
        }

        #endregion CloseWindowCommand

        #endregion Private Methods
    }
}
