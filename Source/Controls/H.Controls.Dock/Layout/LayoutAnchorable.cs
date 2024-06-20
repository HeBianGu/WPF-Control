








using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Serialization;

namespace H.Controls.Dock.Layout
{
    /// <summary>Implements the model for a layout anchorable control (tool window).
    /// A <see cref="LayoutAnchorable"/> can be anchored to the left, right, top, or bottom side of
    /// the Layout property of the <see cref="DockingManager"/>. It can contain
    /// custom application content (WPF controls) and other children elements.
    /// </summary>
    [Serializable]
    public class LayoutAnchorable : LayoutContent
    {
        #region fields

        private double _autohideWidth = 0.0;
        private double _autohideMinWidth = 100.0;
        private double _autohideHeight = 0.0;
        private double _autohideMinHeight = 100.0;
        private bool _canHide = true;
        private bool _canAutoHide = true;
        private bool _canDockAsTabbedDocument = true;
        // BD: 17.08.2020 Remove that bodge and handle CanClose=false && CanHide=true in XAML
        //private bool _canCloseValueBeforeInternalSet;
        private bool _canMove = true;

        #endregion fields

        #region Constructors

        /// <summary>Class constructor</summary>
        public LayoutAnchorable()
        {
            // LayoutAnchorable will hide by default, not close.
            // BD: 14.08.2020 Inverting both _canClose and _canCloseDefault to false as anchorables are only hidden but not closed
            //     That would allow CanClose to be properly serialized if set to true for an instance of LayoutAnchorable
            _canClose = _canCloseDefault = false;
        }

        #endregion Constructors

        #region Events

        /// <summary>Event is invoked when the visibility of this object has changed.</summary>
        public event EventHandler IsVisibleChanged;

        public event EventHandler<CancelEventArgs> Hiding;

        #endregion Events

        #region Properties

        /// <summary>Gets/sets the width for this anchorable in AutoHide mode.</summary>
        public double AutoHideWidth
        {
            get => _autohideWidth;
            set
            {
                if (value == _autohideWidth) return;
                RaisePropertyChanging(nameof(this.AutoHideWidth));
                value = Math.Max(value, _autohideMinWidth);
                _autohideWidth = value;
                RaisePropertyChanged(nameof(this.AutoHideWidth));
            }
        }

        /// <summary>Gets/sets the minimum width for this anchorable in AutoHide mode.</summary>
        public double AutoHideMinWidth
        {
            get => _autohideMinWidth;
            set
            {
                if (value == _autohideMinWidth) return;
                RaisePropertyChanging(nameof(this.AutoHideMinWidth));
                if (value < 0) throw new ArgumentOutOfRangeException("Negative value is not allowed.", nameof(value));
                _autohideMinWidth = value;
                RaisePropertyChanged(nameof(this.AutoHideMinWidth));
            }
        }

        /// <summary>Gets/sets the height for this anchorable in AutoHide mode.</summary>
        public double AutoHideHeight
        {
            get => _autohideHeight;
            set
            {
                if (value == _autohideHeight) return;
                RaisePropertyChanging(nameof(this.AutoHideHeight));
                value = Math.Max(value, _autohideMinHeight);
                _autohideHeight = value;
                RaisePropertyChanged(nameof(this.AutoHideHeight));
            }
        }

        /// <summary>Gets/sets the minimum height for this anchorable in AutoHide mode.</summary>
        public double AutoHideMinHeight
        {
            get => _autohideMinHeight;
            set
            {
                if (value == _autohideMinHeight) return;
                RaisePropertyChanging(nameof(this.AutoHideMinHeight));
                if (value < 0) throw new ArgumentOutOfRangeException("Negative value is not allowed.", nameof(value));

                _autohideMinHeight = value;
                RaisePropertyChanged(nameof(this.AutoHideMinHeight));
            }
        }

        /// <summary>Gets/sets whether the anchorable can hide (be invisible in the UI) or not.</summary>
        public bool CanHide
        {
            get => _canHide;
            set
            {
                if (value == _canHide) return;
                _canHide = value;
                RaisePropertyChanged(nameof(this.CanHide));
            }
        }

        /// <summary>Gets/sets whether the anchorable can be anchored to an achor side in an AutoHide status or not.</summary>
        public bool CanAutoHide
        {
            get => _canAutoHide;
            set
            {
                if (value == _canAutoHide) return;
                _canAutoHide = value;
                RaisePropertyChanged(nameof(this.CanAutoHide));
            }
        }

        /// <summary>Gets/sets whether the anchorable can be docked as a tabbed document or not.</summary>
        public bool CanDockAsTabbedDocument
        {
            get => _canDockAsTabbedDocument;
            set
            {
                if (_canDockAsTabbedDocument == value) return;
                _canDockAsTabbedDocument = value;
                RaisePropertyChanged(nameof(this.CanDockAsTabbedDocument));
            }
        }

        /// <summary>Gets/sets whether a document can be dragged (to be dropped in a different location) or not.
        /// Use this property in conjunction with <see cref="CanMove"/> and <see cref="CanClose"/> and <see cref="LayoutPanel.CanDock"/>
        /// to lock an anchorable in its layout position.</summary>
        public bool CanMove
        {
            get => _canMove;
            set
            {
                if (value == _canMove) return;
                _canMove = value;
                RaisePropertyChanged(nameof(this.CanMove));
            }
        }

        /// <summary>Get a value indicating if the anchorable is anchored to an achor side in an AutoHide status or not.</summary>
        public bool IsAutoHidden => this.Parent is LayoutAnchorGroup;

        /// <summary>Gets whether this object is in a state where it is not visible in the UI or not.</summary>
        [XmlIgnore]
        public bool IsHidden => this.Parent is LayoutRoot;

        /// <summary>Gets/sets whether this object is in a state where it is visible in the UI or not.</summary>
        [XmlIgnore]
        public bool IsVisible
        {
            get => this.Parent != null && !(this.Parent is LayoutRoot);
            set { if (value) Show(); else Hide(); }
        }

        #endregion Properties

        #region Overrides

        /// <inheritdoc />
        protected override void OnParentChanged(ILayoutContainer oldValue, ILayoutContainer newValue)
        {
            UpdateParentVisibility();
            RaisePropertyChanged(nameof(this.IsVisible));
            NotifyIsVisibleChanged();
            RaisePropertyChanged(nameof(this.IsHidden));
            RaisePropertyChanged(nameof(this.IsAutoHidden));
            base.OnParentChanged(oldValue, newValue);
        }

        /// <inheritdoc />
        protected override void InternalDock()
        {
            LayoutRoot root = this.Root as LayoutRoot;
            LayoutAnchorablePane anchorablePane = null;

            //look for active content parent pane
            if (root.ActiveContent != null && root.ActiveContent != this) anchorablePane = root.ActiveContent.Parent as LayoutAnchorablePane;
            //look for a pane on the right side
            if (anchorablePane == null) anchorablePane = root.Descendents().OfType<LayoutAnchorablePane>().FirstOrDefault(pane => !pane.IsHostedInFloatingWindow && pane.GetSide() == AnchorSide.Right);
            //look for an available pane
            if (anchorablePane == null) anchorablePane = root.Descendents().OfType<LayoutAnchorablePane>().FirstOrDefault();
            bool added = false;
            if (root.Manager.LayoutUpdateStrategy != null)
                added = root.Manager.LayoutUpdateStrategy.BeforeInsertAnchorable(root, this, anchorablePane);

            if (!added)
            {
                if (anchorablePane == null)
                {
                    LayoutPanel mainLayoutPanel = new LayoutPanel { Orientation = Orientation.Horizontal };
                    if (root.RootPanel != null)
                        mainLayoutPanel.Children.Add(root.RootPanel);

                    root.RootPanel = mainLayoutPanel;
                    anchorablePane = new LayoutAnchorablePane { DockWidth = new GridLength(200.0, GridUnitType.Pixel) };
                    mainLayoutPanel.Children.Add(anchorablePane);
                }
                anchorablePane.Children.Add(this);
            }
            root.Manager.LayoutUpdateStrategy?.AfterInsertAnchorable(root, this);
            base.InternalDock();
        }

        /// <inheritdoc />
        public override void ReadXml(System.Xml.XmlReader reader)
        {
            if (reader.MoveToAttribute(nameof(this.CanHide)))
                this.CanHide = bool.Parse(reader.Value);
            if (reader.MoveToAttribute(nameof(this.CanAutoHide)))
                this.CanAutoHide = bool.Parse(reader.Value);
            if (reader.MoveToAttribute(nameof(this.AutoHideWidth)))
                this.AutoHideWidth = double.Parse(reader.Value, CultureInfo.InvariantCulture);
            if (reader.MoveToAttribute(nameof(this.AutoHideHeight)))
                this.AutoHideHeight = double.Parse(reader.Value, CultureInfo.InvariantCulture);
            if (reader.MoveToAttribute(nameof(this.AutoHideMinWidth)))
                this.AutoHideMinWidth = double.Parse(reader.Value, CultureInfo.InvariantCulture);
            if (reader.MoveToAttribute(nameof(this.AutoHideMinHeight)))
                this.AutoHideMinHeight = double.Parse(reader.Value, CultureInfo.InvariantCulture);
            if (reader.MoveToAttribute(nameof(this.CanDockAsTabbedDocument)))
                this.CanDockAsTabbedDocument = bool.Parse(reader.Value);
            if (reader.MoveToAttribute(nameof(this.CanMove)))
                this.CanMove = bool.Parse(reader.Value);
            base.ReadXml(reader);
        }

        /// <inheritdoc />
        public override void WriteXml(System.Xml.XmlWriter writer)
        {
            if (!this.CanHide)
                writer.WriteAttributeString(nameof(this.CanHide), this.CanHide.ToString());
            if (!this.CanAutoHide)
                writer.WriteAttributeString(nameof(this.CanAutoHide), this.CanAutoHide.ToString(CultureInfo.InvariantCulture));
            if (this.AutoHideWidth > 0)
                writer.WriteAttributeString(nameof(this.AutoHideWidth), this.AutoHideWidth.ToString(CultureInfo.InvariantCulture));
            if (this.AutoHideHeight > 0)
                writer.WriteAttributeString(nameof(this.AutoHideHeight), this.AutoHideHeight.ToString(CultureInfo.InvariantCulture));
            if (this.AutoHideMinWidth != 25.0)
                writer.WriteAttributeString(nameof(this.AutoHideMinWidth), this.AutoHideMinWidth.ToString(CultureInfo.InvariantCulture));
            if (this.AutoHideMinHeight != 25.0)
                writer.WriteAttributeString(nameof(this.AutoHideMinHeight), this.AutoHideMinHeight.ToString(CultureInfo.InvariantCulture));
            if (!this.CanDockAsTabbedDocument)
                writer.WriteAttributeString(nameof(this.CanDockAsTabbedDocument), this.CanDockAsTabbedDocument.ToString(CultureInfo.InvariantCulture));
            if (!this.CanMove)
                writer.WriteAttributeString(nameof(this.CanMove), this.CanMove.ToString());
            base.WriteXml(writer);
        }

        /// <inheritdoc />
        public override void Close()
        {
            if (this.Root?.Manager != null)
            {
                DockingManager dockingManager = this.Root.Manager;
                dockingManager.ExecuteCloseCommand(this);
            }
            else
                CloseAnchorable();
        }

#if TRACE
        /// <inheritdoc />
        public override void ConsoleDump(int tab)
        {
            System.Diagnostics.Trace.Write(new string(' ', tab * 4));
            System.Diagnostics.Trace.WriteLine("Anchorable()");
        }
#endif

        /// <summary>Method can be invoked to fire the <see cref="Hiding"/> event.</summary>
        /// <param name="args"></param>
        protected virtual void OnHiding(CancelEventArgs args) => Hiding?.Invoke(this, args);

        #endregion Overrides

        #region Public Methods
        public void Hide()
        {
            if (this.Root?.Manager is DockingManager dockingManager)
                dockingManager.ExecuteHideCommand(this);
            else
                HideAnchorable(true);
        }

        /// <summary>Hide this contents.</summary>
        /// <remarks>Add this content to <see cref="ILayoutRoot.Hidden"/> collection of parent root.</remarks>
        /// <param name="cancelable"></param>
        internal bool HideAnchorable(bool cancelable)
        {
            if (!this.IsVisible)
            {
                this.IsSelected = true;
                this.IsActive = true;
                return false;
            }

            if (cancelable)
            {
                CancelEventArgs args = new CancelEventArgs();
                OnHiding(args);
                if (args.Cancel) return false;
            }

            RaisePropertyChanging(nameof(this.IsHidden));
            RaisePropertyChanging(nameof(this.IsVisible));
            if (this.Parent is ILayoutGroup)
            {
                ILayoutGroup parentAsGroup = this.Parent as ILayoutGroup;
                this.PreviousContainer = parentAsGroup;
                this.PreviousContainerIndex = parentAsGroup.IndexOfChild(this);
            }
            this.Root?.Hidden?.Add(this);
            RaisePropertyChanged(nameof(this.IsVisible));
            RaisePropertyChanged(nameof(this.IsHidden));
            NotifyIsVisibleChanged();

            return true;
        }

        /// <summary>Show the content.</summary>
        /// <remarks>Try to show the content where it was previously hidden.</remarks>
        public void Show()
        {
            if (this.IsVisible) return;
            if (!this.IsHidden) throw new InvalidOperationException();
            RaisePropertyChanging(nameof(this.IsHidden));
            RaisePropertyChanging(nameof(this.IsVisible));
            bool added = false;
            ILayoutRoot root = this.Root;
            if (root?.Manager?.LayoutUpdateStrategy != null)
                added = root.Manager.LayoutUpdateStrategy.BeforeInsertAnchorable(root as LayoutRoot, this, this.PreviousContainer);

            if (!added && this.PreviousContainer != null)
            {
                ILayoutGroup previousContainerAsLayoutGroup = this.PreviousContainer as ILayoutGroup;
                if (this.PreviousContainerIndex < previousContainerAsLayoutGroup.ChildrenCount)
                    previousContainerAsLayoutGroup.InsertChildAt(this.PreviousContainerIndex, this);
                else
                    previousContainerAsLayoutGroup.InsertChildAt(previousContainerAsLayoutGroup.ChildrenCount, this);

                this.Parent = previousContainerAsLayoutGroup;
                this.IsSelected = true;
                this.IsActive = true;
            }

            root?.Manager?.LayoutUpdateStrategy?.AfterInsertAnchorable(root as LayoutRoot, this);
            this.PreviousContainer = null;
            this.PreviousContainerIndex = -1;
            RaisePropertyChanged(nameof(this.IsVisible));
            RaisePropertyChanged(nameof(this.IsHidden));
            NotifyIsVisibleChanged();
        }

        /// <summary>Add the anchorable to a <see cref="DockingManager"/> layout.</summary>
        /// <param name="manager"></param>
        /// <param name="strategy"></param>
        public void AddToLayout(DockingManager manager, AnchorableShowStrategy strategy)
        {
            if (this.IsVisible || this.IsHidden) throw new InvalidOperationException();

            bool most = (strategy & AnchorableShowStrategy.Most) == AnchorableShowStrategy.Most;
            bool left = (strategy & AnchorableShowStrategy.Left) == AnchorableShowStrategy.Left;
            bool right = (strategy & AnchorableShowStrategy.Right) == AnchorableShowStrategy.Right;
            bool top = (strategy & AnchorableShowStrategy.Top) == AnchorableShowStrategy.Top;
            bool bottom = (strategy & AnchorableShowStrategy.Bottom) == AnchorableShowStrategy.Bottom;

            if (!most)
            {
                AnchorSide side = AnchorSide.Left;
                if (left) side = AnchorSide.Left;
                if (right) side = AnchorSide.Right;
                if (top) side = AnchorSide.Top;
                if (bottom) side = AnchorSide.Bottom;

                LayoutAnchorablePane anchorablePane = manager.Layout.Descendents().OfType<LayoutAnchorablePane>().FirstOrDefault(p => p.GetSide() == side);
                if (anchorablePane != null)
                    anchorablePane.Children.Add(this);
                else
                    most = true;
            }

            if (!most) return;
            if (manager.Layout.RootPanel == null)
                manager.Layout.RootPanel = new LayoutPanel { Orientation = left || right ? Orientation.Horizontal : Orientation.Vertical };

            if (left || right)
            {
                if (manager.Layout.RootPanel.Orientation == Orientation.Vertical && manager.Layout.RootPanel.ChildrenCount > 1)
                    manager.Layout.RootPanel = new LayoutPanel(manager.Layout.RootPanel);
                manager.Layout.RootPanel.Orientation = Orientation.Horizontal;
                if (left)
                    manager.Layout.RootPanel.Children.Insert(0, new LayoutAnchorablePane(this));
                else
                    manager.Layout.RootPanel.Children.Add(new LayoutAnchorablePane(this));
            }
            else
            {
                if (manager.Layout.RootPanel.Orientation == Orientation.Horizontal && manager.Layout.RootPanel.ChildrenCount > 1)
                    manager.Layout.RootPanel = new LayoutPanel(manager.Layout.RootPanel);
                manager.Layout.RootPanel.Orientation = Orientation.Vertical;
                if (top)
                    manager.Layout.RootPanel.Children.Insert(0, new LayoutAnchorablePane(this));
                else
                    manager.Layout.RootPanel.Children.Add(new LayoutAnchorablePane(this));
            }
        }

        /// <summary>
        /// Reduce this object into an achored side panel position if it is currently docked or
        /// dock this object in the parent group if it is currently anchored in a side panel (AutoHide is active).
        /// </summary>
        public void ToggleAutoHide()
        {
            #region Anchorable is already auto hidden

            if (this.IsAutoHidden)
            {
                LayoutAnchorGroup parentGroup = this.Parent as LayoutAnchorGroup;
                LayoutAnchorSide parentSide = parentGroup.Parent as LayoutAnchorSide;
                LayoutAnchorablePane previousContainer = ((ILayoutPreviousContainer)parentGroup).PreviousContainer as LayoutAnchorablePane;

                if (previousContainer == null)
                {
                    AnchorSide side = ((LayoutAnchorSide)parentGroup.Parent).Side;
                    switch (side)
                    {
                        case AnchorSide.Right:
                            if (parentGroup.Root.RootPanel.Orientation == Orientation.Horizontal)
                            {
                                previousContainer = new LayoutAnchorablePane
                                {
                                    DockMinWidth = this.AutoHideMinWidth,
                                    DockMinHeight = this.AutoHideMinHeight
                                };
                                parentGroup.Root.RootPanel.Children.Add(previousContainer);
                            }
                            else
                            {
                                previousContainer = new LayoutAnchorablePane
                                {
                                    DockMinHeight = this.AutoHideMinHeight,
                                    DockMinWidth = this.AutoHideMinWidth
                                };
                                LayoutPanel panel = new LayoutPanel { Orientation = Orientation.Horizontal };
                                LayoutRoot root = parentGroup.Root as LayoutRoot;
                                LayoutPanel oldRootPanel = parentGroup.Root.RootPanel;
                                root.RootPanel = panel;
                                panel.Children.Add(oldRootPanel);
                                panel.Children.Add(previousContainer);
                            }
                            break;

                        case AnchorSide.Left:
                            if (parentGroup.Root.RootPanel.Orientation == Orientation.Horizontal)
                            {
                                previousContainer = new LayoutAnchorablePane
                                {
                                    DockMinWidth = this.AutoHideMinWidth,
                                    DockMinHeight = this.AutoHideMinHeight
                                };
                                parentGroup.Root.RootPanel.Children.Insert(0, previousContainer);
                            }
                            else
                            {
                                previousContainer = new LayoutAnchorablePane
                                {
                                    DockMinHeight = this.AutoHideMinHeight,
                                    DockMinWidth = this.AutoHideMinWidth
                                };
                                LayoutPanel panel = new LayoutPanel { Orientation = Orientation.Horizontal };
                                LayoutRoot root = parentGroup.Root as LayoutRoot;
                                LayoutPanel oldRootPanel = parentGroup.Root.RootPanel;
                                root.RootPanel = panel;
                                panel.Children.Add(previousContainer);
                                panel.Children.Add(oldRootPanel);
                            }
                            break;

                        case AnchorSide.Top:
                            if (parentGroup.Root.RootPanel.Orientation == Orientation.Vertical)
                            {
                                previousContainer = new LayoutAnchorablePane
                                {
                                    DockMinHeight = this.AutoHideMinHeight,
                                    DockMinWidth = this.AutoHideMinWidth
                                };
                                parentGroup.Root.RootPanel.Children.Insert(0, previousContainer);
                            }
                            else
                            {
                                previousContainer = new LayoutAnchorablePane
                                {
                                    DockMinWidth = this.AutoHideMinWidth,
                                    DockMinHeight = this.AutoHideMinHeight
                                };
                                LayoutPanel panel = new LayoutPanel { Orientation = Orientation.Vertical };
                                LayoutRoot root = parentGroup.Root as LayoutRoot;
                                LayoutPanel oldRootPanel = parentGroup.Root.RootPanel;
                                root.RootPanel = panel;
                                panel.Children.Add(previousContainer);
                                panel.Children.Add(oldRootPanel);
                            }
                            break;

                        case AnchorSide.Bottom:
                            if (parentGroup.Root.RootPanel.Orientation == Orientation.Vertical)
                            {
                                previousContainer = new LayoutAnchorablePane
                                {
                                    DockMinHeight = this.AutoHideMinHeight,
                                    DockMinWidth = this.AutoHideMinWidth
                                };
                                parentGroup.Root.RootPanel.Children.Add(previousContainer);
                            }
                            else
                            {
                                previousContainer = new LayoutAnchorablePane
                                {
                                    DockMinWidth = this.AutoHideMinWidth,
                                    DockMinHeight = this.AutoHideMinHeight
                                };
                                LayoutPanel panel = new LayoutPanel { Orientation = Orientation.Vertical };
                                LayoutRoot root = parentGroup.Root as LayoutRoot;
                                LayoutPanel oldRootPanel = parentGroup.Root.RootPanel;
                                root.RootPanel = panel;
                                panel.Children.Add(oldRootPanel);
                                panel.Children.Add(previousContainer);
                            }
                            break;
                    }
                }
                else
                {
                    //I'm about to remove parentGroup, redirect any content (ie hidden contents) that point to it
                    //to previousContainer
                    LayoutRoot root = parentGroup.Root as LayoutRoot;
                    foreach (ILayoutPreviousContainer cnt in root.Descendents().OfType<ILayoutPreviousContainer>().Where(c => c.PreviousContainer == parentGroup))
                        cnt.PreviousContainer = previousContainer;
                }

                int selectedIndex = -1;
                LayoutAnchorable selectedItem = parentGroup.Children.FirstOrDefault(x => x.IsActive);
                if (selectedItem != null)
                    selectedIndex = parentGroup.Children.IndexOf(selectedItem);

                foreach (LayoutAnchorable anchorableToToggle in parentGroup.Children.ToArray())
                    previousContainer.Children.Add(anchorableToToggle);

                if (selectedIndex != -1)
                    previousContainer.SelectedContentIndex = selectedIndex;

                parentSide.Children.Remove(parentGroup);

                LayoutGroupBase parent = previousContainer.Parent as LayoutGroupBase;
                while (parent != null)
                {
                    if (parent is LayoutGroup<ILayoutPanelElement> layoutGroup)
                        layoutGroup.ComputeVisibility();
                    parent = parent.Parent as LayoutGroupBase;
                }
            }

            #endregion Anchorable is already auto hidden

            #region Anchorable is docked

            else if (this.Parent is LayoutAnchorablePane)
            {
                ILayoutRoot root = this.Root;
                LayoutAnchorablePane parentPane = this.Parent as LayoutAnchorablePane;
                LayoutAnchorGroup newAnchorGroup = new LayoutAnchorGroup();
                ((ILayoutPreviousContainer)newAnchorGroup).PreviousContainer = parentPane;

                foreach (LayoutAnchorable anchorableToImport in parentPane.Children.ToArray())
                    newAnchorGroup.Children.Add(anchorableToImport);

                //detect anchor side for the pane
                AnchorSide anchorSide = parentPane.GetSide();

                switch (anchorSide)
                {
                    case AnchorSide.Right: root.RightSide?.Children.Add(newAnchorGroup); break;
                    case AnchorSide.Left: root.LeftSide?.Children.Add(newAnchorGroup); break;
                    case AnchorSide.Top: root.TopSide?.Children.Add(newAnchorGroup); break;
                    case AnchorSide.Bottom: root.BottomSide?.Children.Add(newAnchorGroup); break;
                }
            }

            #endregion Anchorable is docked
        }

        #endregion Public Methods

        #region Internal Methods

        internal bool CloseAnchorable()
        {
            if (!TestCanClose()) return false;
            if (this.IsAutoHidden) ToggleAutoHide();
            CloseInternal();
            return true;
        }

        // BD: 17.08.2020 Remove that bodge and handle CanClose=false && CanHide=true in XAML
        //internal void SetCanCloseInternal(bool canClose)
        //{
        //	_canCloseValueBeforeInternalSet = _canClose;
        //	_canClose = canClose;
        //}

        //internal void ResetCanCloseInternal()
        //{
        //	_canClose = _canCloseValueBeforeInternalSet;
        //}

        #endregion Internal Methods

        #region Private Methods

        private void NotifyIsVisibleChanged() => IsVisibleChanged?.Invoke(this, EventArgs.Empty);

        private void UpdateParentVisibility()
        {
            // Element is Hidden since it has no parent but a previous parent
            if (this.PreviousContainer != null && this.Parent == null)
            {
                // Go back to using previous parent
                this.Parent = this.PreviousContainer;
                ////        PreviousContainer = null;
            }

            if (this.Parent is ILayoutElementWithVisibility parentPane)
                parentPane.ComputeVisibility();
        }

        #endregion Private Methods
    }
}
