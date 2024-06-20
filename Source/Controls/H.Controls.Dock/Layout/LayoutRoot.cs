








using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace H.Controls.Dock.Layout
{
    /// <summary>
    /// Implements the root of the layout viewmodel (see  <see cref="DockingManager.Layout"/> property).
    /// This root includes a  <see cref="RootPanel"/> property for binding content, side panel properties
    /// and many other layout related root items.
    ///
    /// This class implements <see cref="LayoutElement.PropertyChanged"/> and
    /// <see cref="LayoutElement.PropertyChanging"/> to support direct UI binding scenarios
    /// with view updates supported.
    /// </summary>
    [ContentProperty(nameof(RootPanel))]
    [Serializable]
    public class LayoutRoot : LayoutElement, ILayoutContainer, ILayoutRoot, IXmlSerializable
    {
        #region fields

        private LayoutPanel _rootPanel;
        private LayoutAnchorSide _topSide = null;
        private LayoutAnchorSide _rightSide;
        private LayoutAnchorSide _leftSide = null;
        private LayoutAnchorSide _bottomSide = null;

        private ObservableCollection<LayoutFloatingWindow> _floatingWindows = null;
        private ObservableCollection<LayoutAnchorable> _hiddenAnchorables = null;

        [field: NonSerialized]
        private WeakReference _activeContent = null;

        private bool _activeContentSet = false;

        [field: NonSerialized]
        private WeakReference _lastFocusedDocument = null;

        [NonSerialized]
        private DockingManager _manager = null;

        #endregion fields

        #region Constructors

        /// <summary>Standard class constructor</summary>
        public LayoutRoot()
        {
            this.RightSide = new LayoutAnchorSide();
            this.LeftSide = new LayoutAnchorSide();
            this.TopSide = new LayoutAnchorSide();
            this.BottomSide = new LayoutAnchorSide();
            this.RootPanel = new LayoutPanel(new LayoutDocumentPane());
        }

        #endregion Constructors

        #region Events

        /// <summary>
        /// Raised when the layout is updated. This event is raised via <see cref="FireLayoutUpdated()"/> method
        /// when a parent of a LayoutElement has changed.
        /// </summary>
        public event EventHandler Updated;

        /// <summary>Raised when an element is added to the layout.</summary>
        public event EventHandler<LayoutElementEventArgs> ElementAdded;

        /// <summary>Raised when an element is removed from the layout.</summary>
        public event EventHandler<LayoutElementEventArgs> ElementRemoved;

        #endregion Events

        #region Properties

        /// <summary>Gets/sets the root layout panel that contains the <see cref="LayoutDocumentPane"/>.</summary>
        public LayoutPanel RootPanel
        {
            get => _rootPanel;
            set
            {
                if (_rootPanel == value) return;
                RaisePropertyChanging(nameof(this.RootPanel));
                LayoutContent activeContent = this.ActiveContent;
                ILayoutRoot activeRoot = activeContent?.Root;
                if (_rootPanel != null && _rootPanel.Parent == this) _rootPanel.Parent = null;
                _rootPanel = value ?? new LayoutPanel(new LayoutDocumentPane());
                _rootPanel.Parent = this;
                if (this.ActiveContent == null && activeRoot == this && activeContent != null)
                {
                    this.ActiveContent = activeContent;
                    if (this.ActiveContent != activeContent)
                    {
                        this.ActiveContent = activeContent;
                    }
                }
                RaisePropertyChanged(nameof(this.RootPanel));
            }
        }

        /// <summary>Gets or sets the top side of the layout root.</summary>
        public LayoutAnchorSide TopSide
        {
            get => _topSide;
            set
            {
                if (_topSide == value) return;
                RaisePropertyChanging(nameof(this.TopSide));
                _topSide = value;
                if (_topSide != null) _topSide.Parent = this;
                RaisePropertyChanged(nameof(this.TopSide));
            }
        }

        /// <summary>Gets or sets the right side of the layout root.</summary>
        public LayoutAnchorSide RightSide
        {
            get => _rightSide;
            set
            {
                if (_rightSide == value) return;
                RaisePropertyChanging(nameof(this.RightSide));
                _rightSide = value;
                if (_rightSide != null) _rightSide.Parent = this;
                RaisePropertyChanged(nameof(this.RightSide));
            }
        }

        /// <summary>Gets or sets the left side of the layout root.</summary>
        public LayoutAnchorSide LeftSide
        {
            get => _leftSide;
            set
            {
                if (value == _leftSide) return;
                RaisePropertyChanging(nameof(this.LeftSide));
                _leftSide = value;
                if (_leftSide != null) _leftSide.Parent = this;
                RaisePropertyChanged(nameof(this.LeftSide));
            }
        }

        /// <summary>Gets or sets the bottom side of the layout root.</summary>
        public LayoutAnchorSide BottomSide
        {
            get => _bottomSide;
            set
            {
                if (value == _bottomSide) return;
                RaisePropertyChanging(nameof(this.BottomSide));
                _bottomSide = value;
                if (_bottomSide != null) _bottomSide.Parent = this;
                RaisePropertyChanged(nameof(this.BottomSide));
            }
        }

        /// <summary>Gets the floating windows that are part of this layout.</summary>
        public ObservableCollection<LayoutFloatingWindow> FloatingWindows
        {
            get
            {
                if (_floatingWindows == null)
                {
                    _floatingWindows = new ObservableCollection<LayoutFloatingWindow>();
                    _floatingWindows.CollectionChanged += _floatingWindows_CollectionChanged;
                }

                return _floatingWindows;
            }
        }

        /// <summary>Gets the hidden anchorables in the layout.</summary>
        public ObservableCollection<LayoutAnchorable> Hidden
        {
            get
            {
                if (_hiddenAnchorables == null)
                {
                    _hiddenAnchorables = new ObservableCollection<LayoutAnchorable>();
                    _hiddenAnchorables.CollectionChanged += _hiddenAnchorables_CollectionChanged;
                }

                return _hiddenAnchorables;
            }
        }

        #region Children

        /// <summary>Gets the child elements of the layout root.</summary>
        public IEnumerable<ILayoutElement> Children
        {
            get
            {
                if (this.RootPanel != null)
                    yield return this.RootPanel;
                if (_floatingWindows != null)
                {
                    foreach (LayoutFloatingWindow floatingWindow in _floatingWindows)
                        yield return floatingWindow;
                }
                if (this.TopSide != null)
                    yield return this.TopSide;
                if (this.RightSide != null)
                    yield return this.RightSide;
                if (this.BottomSide != null)
                    yield return this.BottomSide;
                if (this.LeftSide != null)
                    yield return this.LeftSide;
                if (_hiddenAnchorables != null)
                {
                    foreach (LayoutAnchorable hiddenAnchorable in _hiddenAnchorables)
                        yield return hiddenAnchorable;
                }
            }
        }

        /// <summary>Gets the number of child elements of the layout root.</summary>
        public int ChildrenCount => 5 + (_floatingWindows?.Count ?? 0) + (_hiddenAnchorables?.Count ?? 0);

        #endregion Children

        /// <summary>Gets the active LayoutContent-derived element.</summary>
        [XmlIgnore]
        public LayoutContent ActiveContent
        {
            get
            {
                return _activeContent.GetValueOrDefault<LayoutContent>();
            }
            set
            {
                LayoutContent currentValue = this.ActiveContent;
                if (currentValue != value)
                {
                    InternalSetActiveContent(currentValue, value);
                }
            }
        }

        [XmlIgnore]
        public LayoutContent LastFocusedDocument
        {
            get => _lastFocusedDocument.GetValueOrDefault<LayoutContent>();
            private set
            {
                LayoutContent currentValue = this.LastFocusedDocument;
                if (currentValue != value)
                {
                    RaisePropertyChanging(nameof(this.LastFocusedDocument));
                    if (currentValue != null) currentValue.IsLastFocusedDocument = false;
                    _lastFocusedDocument = new WeakReference(value);
                    currentValue = this.LastFocusedDocument;
                    if (currentValue != null) currentValue.IsLastFocusedDocument = true;
                    RaisePropertyChanged(nameof(this.LastFocusedDocument));
                }
            }
        }

        /// <summary>Gets/sets the docking manager root control for this library.</summary>
        [XmlIgnore]
        public DockingManager Manager
        {
            get => _manager;
            internal set
            {
                if (value == _manager) return;
                RaisePropertyChanging(nameof(this.Manager));
                _manager = value;
                RaisePropertyChanged(nameof(this.Manager));
            }
        }

        #endregion Properties

        #region Overrides

#if TRACE
        public override void ConsoleDump(int tab)
        {
            System.Diagnostics.Trace.Write(new string(' ', tab * 4));
            System.Diagnostics.Trace.WriteLine("RootPanel()");

            this.RootPanel.ConsoleDump(tab + 1);

            System.Diagnostics.Trace.Write(new string(' ', tab * 4));
            System.Diagnostics.Trace.WriteLine("FloatingWindows()");

            foreach (LayoutFloatingWindow fw in this.FloatingWindows)
                fw.ConsoleDump(tab + 1);

            System.Diagnostics.Trace.Write(new string(' ', tab * 4));
            System.Diagnostics.Trace.WriteLine("Hidden()");

            foreach (LayoutAnchorable hidden in this.Hidden)
                hidden.ConsoleDump(tab + 1);
        }
#endif

        #endregion Overrides

        #region Public Methods

        public void RemoveChild(ILayoutElement element)
        {
            if (element == this.RootPanel)
                this.RootPanel = null;
            else if (_floatingWindows != null && _floatingWindows.Contains(element))
                _floatingWindows.Remove(element as LayoutFloatingWindow);
            else if (_hiddenAnchorables != null && _hiddenAnchorables.Contains(element))
                _hiddenAnchorables.Remove(element as LayoutAnchorable);
            else if (element == this.TopSide)
                this.TopSide = null;
            else if (element == this.RightSide)
                this.RightSide = null;
            else if (element == this.BottomSide)
                this.BottomSide = null;
            else if (element == this.LeftSide)
                this.LeftSide = null;
        }

        public void ReplaceChild(ILayoutElement oldElement, ILayoutElement newElement)
        {
            if (oldElement == this.RootPanel)
                this.RootPanel = (LayoutPanel)newElement;
            else if (_floatingWindows != null && _floatingWindows.Contains(oldElement))
            {
                int index = _floatingWindows.IndexOf(oldElement as LayoutFloatingWindow);
                _floatingWindows.Remove(oldElement as LayoutFloatingWindow);
                _floatingWindows.Insert(index, newElement as LayoutFloatingWindow);
            }
            else if (_hiddenAnchorables != null && _hiddenAnchorables.Contains(oldElement))
            {
                int index = _hiddenAnchorables.IndexOf(oldElement as LayoutAnchorable);
                _hiddenAnchorables.Remove(oldElement as LayoutAnchorable);
                _hiddenAnchorables.Insert(index, newElement as LayoutAnchorable);
            }
            else if (oldElement == this.TopSide)
                this.TopSide = (LayoutAnchorSide)newElement;
            else if (oldElement == this.RightSide)
                this.RightSide = (LayoutAnchorSide)newElement;
            else if (oldElement == this.BottomSide)
                this.BottomSide = (LayoutAnchorSide)newElement;
            else if (oldElement == this.LeftSide)
                this.LeftSide = (LayoutAnchorSide)newElement;
        }

        /// <summary>Removes any empty container not directly referenced by other layout items.</summary>
        public void CollectGarbage()
        {
            bool exitFlag = true;

            #region collect empty panes

            do
            {
                exitFlag = true;

                //for each content that references via PreviousContainer a disconnected Pane set the property to null
                foreach (ILayoutPreviousContainer content in this.Descendents().OfType<ILayoutPreviousContainer>().Where(c => c.PreviousContainer != null &&
                    (c.PreviousContainer.Parent == null || c.PreviousContainer.Parent.Root != this)))
                {
                    content.PreviousContainer = null;
                }

                //for each pane that is empty
                foreach (ILayoutPane emptyPane in this.Descendents().OfType<ILayoutPane>().Where(p => p.ChildrenCount == 0))
                {
                    //...set null any reference coming from contents not yet hosted in a floating window
                    foreach (LayoutContent contentReferencingEmptyPane in this.Descendents().OfType<LayoutContent>()
                        .Where(c => ((ILayoutPreviousContainer)c).PreviousContainer == emptyPane && !c.IsFloating))
                    {
                        if (contentReferencingEmptyPane is LayoutAnchorable anchorable &&
                            !anchorable.IsVisible)
                            continue;

                        ((ILayoutPreviousContainer)contentReferencingEmptyPane).PreviousContainer = null;
                        contentReferencingEmptyPane.PreviousContainerIndex = -1;
                    }

                    //...if this pane is the only documentpane present in the layout of the main window (not floating) then skip it
                    if (emptyPane is LayoutDocumentPane &&
                         emptyPane.FindParent<LayoutDocumentFloatingWindow>() == null &&
                         this.Descendents().OfType<LayoutDocumentPane>().Count(c => c != emptyPane && c.FindParent<LayoutDocumentFloatingWindow>() == null) == 0)
                        continue;

                    //...if this empty pane is not referenced by anyone, then remove it from its parent container
                    if (!this.Descendents().OfType<ILayoutPreviousContainer>().Any(c => c.PreviousContainer == emptyPane))
                    {
                        ILayoutContainer parentGroup = emptyPane.Parent;
                        parentGroup.RemoveChild(emptyPane);
                        exitFlag = false;
                        break;
                    }
                }

                if (!exitFlag)
                {
                    //removes any empty anchorable pane group
                    foreach (LayoutAnchorablePaneGroup emptyLayoutAnchorablePaneGroup in this.Descendents().OfType<LayoutAnchorablePaneGroup>().Where(p => p.ChildrenCount == 0))
                    {
                        ILayoutContainer parentGroup = emptyLayoutAnchorablePaneGroup.Parent;
                        parentGroup.RemoveChild(emptyLayoutAnchorablePaneGroup);
                        exitFlag = false;
                        break;
                    }
                }

                if (!exitFlag)
                {
                    //removes any empty layout panel
                    foreach (LayoutPanel emptyLayoutPanel in this.Descendents().OfType<LayoutPanel>().Where(p => p.ChildrenCount == 0))
                    {
                        ILayoutContainer parentGroup = emptyLayoutPanel.Parent;
                        parentGroup.RemoveChild(emptyLayoutPanel);
                        exitFlag = false;
                        break;
                    }
                    foreach (LayoutDocumentPane emptyLayoutDocumentPane in this.Descendents().OfType<LayoutDocumentPane>().Where(p => p.ChildrenCount == 0))
                    {
                        ILayoutContainer parentGroup = emptyLayoutDocumentPane.Parent;
                        if (!(parentGroup.Parent is LayoutDocumentFloatingWindow)) continue;
                        int index = this.RootPanel.IndexOfChild(this.Descendents().OfType<LayoutDocumentPaneGroup>().First());
                        parentGroup.RemoveChild(emptyLayoutDocumentPane);
                        if (!this.Descendents().OfType<LayoutDocumentPane>().Any())
                        {
                            // Now the last Pane container is deleted, at least one is required for documents to be added.
                            // We did not want to keep an empty window floating, but add a new one to the main window
                            this.RootPanel.Children.Insert(index < 0 ? 0 : index, emptyLayoutDocumentPane);
                        }
                        exitFlag = false;
                        break;
                    }
                }

                if (!exitFlag)
                {
                    //removes any empty floating window
                    foreach (LayoutFloatingWindow emptyLayoutFloatingWindow in this.Descendents().OfType<LayoutFloatingWindow>().Where(p => p.ChildrenCount == 0))
                    {
                        ILayoutContainer parentGroup = emptyLayoutFloatingWindow.Parent;
                        parentGroup.RemoveChild(emptyLayoutFloatingWindow);
                        exitFlag = false;
                        break;
                    }
                }

                if (!exitFlag)
                {
                    //removes any empty anchor group
                    foreach (LayoutAnchorGroup emptyLayoutAnchorGroup in this.Descendents().OfType<LayoutAnchorGroup>().Where(p => p.ChildrenCount == 0))
                    {
                        if (!this.Descendents().OfType<ILayoutPreviousContainer>().Any(c => c.PreviousContainer == emptyLayoutAnchorGroup))
                        {
                            ILayoutContainer parentGroup = emptyLayoutAnchorGroup.Parent;
                            parentGroup.RemoveChild(emptyLayoutAnchorGroup);
                            exitFlag = false;
                            break;
                        }
                    }
                }
            }
            while (!exitFlag);

            #endregion collect empty panes

            #region collapse single child anchorable pane groups

            do
            {
                exitFlag = true;
                //for each pane that is empty
                foreach (LayoutAnchorablePaneGroup paneGroupToCollapse in this.Descendents().OfType<LayoutAnchorablePaneGroup>().Where(p => p.ChildrenCount == 1 && p.Children[0] is LayoutAnchorablePaneGroup).ToArray())
                {
                    LayoutAnchorablePaneGroup singleChild = paneGroupToCollapse.Children[0] as LayoutAnchorablePaneGroup;
                    paneGroupToCollapse.Orientation = singleChild.Orientation;
                    while (singleChild.ChildrenCount > 0)
                        paneGroupToCollapse.InsertChildAt(paneGroupToCollapse.ChildrenCount, singleChild.Children[0]);
                    paneGroupToCollapse.RemoveChild(singleChild);
                    exitFlag = false;
                    break;
                }
            }
            while (!exitFlag);

            #endregion collapse single child anchorable pane groups

            #region collapse single child document pane groups

            do
            {
                exitFlag = true;
                //for each pane that is empty
                foreach (LayoutDocumentPaneGroup paneGroupToCollapse in this.Descendents().OfType<LayoutDocumentPaneGroup>().Where(p => p.ChildrenCount == 1 && p.Children[0] is LayoutDocumentPaneGroup).ToArray())
                {
                    LayoutDocumentPaneGroup singleChild = paneGroupToCollapse.Children[0] as LayoutDocumentPaneGroup;
                    paneGroupToCollapse.Orientation = singleChild.Orientation;
                    while (singleChild.ChildrenCount > 0)
                        paneGroupToCollapse.InsertChildAt(paneGroupToCollapse.ChildrenCount, singleChild.Children[0]);
                    paneGroupToCollapse.RemoveChild(singleChild);
                    exitFlag = false;
                    break;
                }
            }
            while (!exitFlag);

            #endregion collapse single child document pane groups

            ////do
            ////{
            ////  exitFlag = true;
            ////  //for each panel that has only one child
            ////  foreach( var panelToCollapse in this.Descendents().OfType<LayoutPanel>().Where( p => p.ChildrenCount == 1 && p.Children[ 0 ] is LayoutPanel ).ToArray() )
            ////  {
            ////    var singleChild = panelToCollapse.Children[ 0 ] as LayoutPanel;
            ////    panelToCollapse.Orientation = singleChild.Orientation;
            ////    panelToCollapse.RemoveChild( singleChild );
            ////    ILayoutPanelElement[] singleChildChildren = new ILayoutPanelElement[ singleChild.ChildrenCount ];
            ////    singleChild.Children.CopyTo( singleChildChildren, 0 );
            ////    while( singleChild.ChildrenCount > 0 )
            ////    {
            ////      panelToCollapse.InsertChildAt(
            ////          panelToCollapse.ChildrenCount, singleChildChildren[ panelToCollapse.ChildrenCount ] );
            ////    }

            ////    exitFlag = false;
            ////    break;
            ////  }
            ////}
            ////while( !exitFlag );

            // Update ActiveContent and LastFocusedDocument properties
            UpdateActiveContentProperty();

#if DEBUG
            Debug.Assert(!this.Descendents().OfType<LayoutAnchorablePane>().Any(a => a.ChildrenCount == 0 && a.IsVisible));
            //DumpTree(true);
#if TRACE
            this.RootPanel.ConsoleDump(4);
#endif
#endif
        }

        #region IXmlSerializable interface members

        /// <inheritdoc />
        XmlSchema IXmlSerializable.GetSchema() => null;

        /// <inheritdoc />
        void IXmlSerializable.ReadXml(XmlReader reader)
        {
            reader.MoveToContent();
            if (reader.IsEmptyElement)
            {
                reader.Read();
                return;
            }

            List<ILayoutPanelElement> layoutPanelElements = ReadRootPanel(reader, out Orientation orientation, out bool canDock);
            if (layoutPanelElements != null)
            {
                this.RootPanel = new LayoutPanel { Orientation = orientation, CanDock = canDock };
                //Add all children to RootPanel
                foreach (ILayoutPanelElement panel in layoutPanelElements) this.RootPanel.Children.Add(panel);
            }

            this.TopSide = new LayoutAnchorSide();
            if (ReadElement(reader) != null) FillLayoutAnchorSide(reader, this.TopSide);
            this.RightSide = new LayoutAnchorSide();
            if (ReadElement(reader) != null) FillLayoutAnchorSide(reader, this.RightSide);
            this.LeftSide = new LayoutAnchorSide();
            if (ReadElement(reader) != null) FillLayoutAnchorSide(reader, this.LeftSide);
            this.BottomSide = new LayoutAnchorSide();
            if (ReadElement(reader) != null) FillLayoutAnchorSide(reader, this.BottomSide);

            this.FloatingWindows.Clear();
            List<object> floatingWindows = ReadElementList(reader, true);
            foreach (object floatingWindow in floatingWindows) this.FloatingWindows.Add((LayoutFloatingWindow)floatingWindow);

            this.Hidden.Clear();
            List<object> hidden = ReadElementList(reader, false);
            foreach (object hiddenObject in hidden) this.Hidden.Add((LayoutAnchorable)hiddenObject);

            //Read the closing end element of LayoutRoot
            reader.ReadEndElement();
        }

        /// <inheritdoc />
        void IXmlSerializable.WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement(nameof(this.RootPanel));
            this.RootPanel?.WriteXml(writer);
            writer.WriteEndElement();

            writer.WriteStartElement(nameof(this.TopSide));
            this.TopSide?.WriteXml(writer);
            writer.WriteEndElement();

            writer.WriteStartElement(nameof(this.RightSide));
            this.RightSide?.WriteXml(writer);
            writer.WriteEndElement();

            writer.WriteStartElement(nameof(this.LeftSide));
            this.LeftSide?.WriteXml(writer);
            writer.WriteEndElement();

            writer.WriteStartElement(nameof(this.BottomSide));
            this.BottomSide?.WriteXml(writer);
            writer.WriteEndElement();

            // Write all floating windows (can be LayoutDocumentFloatingWindow or LayoutAnchorableFloatingWindow).
            // To prevent "can not create instance of abstract type", the type is retrieved with GetType().Name
            writer.WriteStartElement(nameof(this.FloatingWindows));
            foreach (LayoutFloatingWindow layoutFloatingWindow in this.FloatingWindows)
            {
                writer.WriteStartElement(layoutFloatingWindow.GetType().Name);
                layoutFloatingWindow.WriteXml(writer);
                writer.WriteEndElement();
            }
            writer.WriteEndElement();

            writer.WriteStartElement(nameof(this.Hidden));
            foreach (LayoutAnchorable layoutAnchorable in this.Hidden)
            {
                writer.WriteStartElement(layoutAnchorable.GetType().Name);
                layoutAnchorable.WriteXml(writer);
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
        }

        #endregion IXmlSerializable interface members

        #endregion Public Methods

        #region Internal Methods

        internal static Type FindType(string name)
        {
            Assembly avalonAssembly = Assembly.GetAssembly(typeof(LayoutRoot));

            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies().OrderBy(a => a != avalonAssembly))
                try
                {
                    foreach (Type type in assembly.GetTypes())
                        if (type.Name.Equals(name))
                            return type;
                }
                catch (ReflectionTypeLoadException)
                {
                }

            return null;
        }

        internal void FireLayoutUpdated() => Updated?.Invoke(this, EventArgs.Empty);

        internal void OnLayoutElementAdded(LayoutElement element) => ElementAdded?.Invoke(this, new LayoutElementEventArgs(element));

        internal void OnLayoutElementRemoved(LayoutElement element)
        {
            if (element.Descendents().OfType<LayoutContent>().Any(c => c == this.LastFocusedDocument))
                this.LastFocusedDocument = null;
            if (element.Descendents().OfType<LayoutContent>().Any(c => c == this.ActiveContent))
                this.ActiveContent = null;
            ElementRemoved?.Invoke(this, new LayoutElementEventArgs(element));
        }

        #endregion Internal Methods

        #region Private Methods

        private void _floatingWindows_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            bool bNotifyChildren = false;

            if (e.OldItems != null && (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove || e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Replace))
            {
                foreach (LayoutFloatingWindow element in e.OldItems)
                {
                    if (element.Parent != this) continue;
                    element.Parent = null;
                    bNotifyChildren = true;
                }
            }

            if (e.NewItems != null && (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add || e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Replace))
            {
                foreach (LayoutFloatingWindow element in e.NewItems)
                {
                    element.Parent = this;
                    bNotifyChildren = true;
                }
            }

            // descendants of LayoutElement notify when their Children and ChildrenCount properties change
            // https://github.com/xceedsoftware/wpftoolkit/issues/1313
            //
            if (!bNotifyChildren) return;
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    RaisePropertyChanged(nameof(this.Children));
                    RaisePropertyChanged(nameof(this.ChildrenCount));
                    break;

                case System.Collections.Specialized.NotifyCollectionChangedAction.Replace:
                    RaisePropertyChanged(nameof(this.Children));
                    break;
            }
        }

        private void _hiddenAnchorables_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            bool bNotifyChildren = false;

            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove || e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Replace)
            {
                if (e.OldItems != null)
                {
                    foreach (LayoutAnchorable element in e.OldItems)
                    {
                        if (element.Parent != this) continue;
                        element.Parent = null;
                        bNotifyChildren = true;
                    }
                }
            }

            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add || e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Replace)
            {
                if (e.NewItems != null)
                {
                    foreach (LayoutAnchorable element in e.NewItems)
                    {
                        if (element.Parent == this) continue;
                        element.Parent?.RemoveChild(element);
                        element.Parent = this;
                        bNotifyChildren = true;
                    }
                }
            }

            // descendants of LayoutElement notify when their Children and ChildrenCount properties change
            // https://github.com/xceedsoftware/wpftoolkit/issues/1313
            //
            if (!bNotifyChildren) return;
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    RaisePropertyChanged(nameof(this.Children));
                    RaisePropertyChanged(nameof(this.ChildrenCount));
                    break;

                case System.Collections.Specialized.NotifyCollectionChangedAction.Replace:
                    RaisePropertyChanged(nameof(this.Children));
                    break;
            }
        }

        private void InternalSetActiveContent(LayoutContent currentValue, LayoutContent newActiveContent)
        {
            RaisePropertyChanging(nameof(this.ActiveContent));
            if (currentValue != null && currentValue.IsActive) currentValue.IsActive = false;
            _activeContent = new WeakReference(newActiveContent);
            currentValue = this.ActiveContent;
            if (currentValue != null && !currentValue.IsActive) currentValue.IsActive = true;
            RaisePropertyChanged(nameof(this.ActiveContent));
            _activeContentSet = currentValue != null;
            if (currentValue != null)
            {
                if (currentValue.Parent is LayoutDocumentPane || currentValue is LayoutDocument)
                    this.LastFocusedDocument = currentValue;
            }
            else
                this.LastFocusedDocument = null;
        }

        private void UpdateActiveContentProperty()
        {
            LayoutContent activeContent = this.ActiveContent;
            if (_activeContentSet && (activeContent == null || activeContent.Root != this))
            {
                _activeContentSet = false;
                InternalSetActiveContent(activeContent, null);
            }
        }

        private void FillLayoutAnchorSide(XmlReader reader, LayoutAnchorSide layoutAnchorSide)
        {
            List<LayoutAnchorGroup> result = new List<LayoutAnchorGroup>();

            while (true)
            {
                //Read all layoutAnchorSide children
                if (ReadElement(reader) is LayoutAnchorGroup element) result.Add(element);
                else if (reader.NodeType == XmlNodeType.EndElement) break;
            }

            reader.ReadEndElement();
            foreach (LayoutAnchorGroup las in result)
            {
                layoutAnchorSide.Children.Add(las);
            }
        }

        /// <summary>
        /// Reads all properties of the <see cref="LayoutPanel"/> and returns them.
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="orientation"></param>
        /// <param name="canDock"></param>
        /// <returns></returns>
        private List<ILayoutPanelElement> ReadRootPanel(XmlReader reader
            , out Orientation orientation
            , out bool canDock)
        {
            orientation = Orientation.Horizontal;
            canDock = true;

            List<ILayoutPanelElement> result = new List<ILayoutPanelElement>();
            string startElementName = reader.LocalName;
            reader.Read();
            if (reader.LocalName.Equals(startElementName) && reader.NodeType == XmlNodeType.EndElement) return null;

            while (reader.NodeType == XmlNodeType.Whitespace) reader.Read();

            if (reader.LocalName.Equals(nameof(this.RootPanel)))
            {
                orientation = (Orientation)Enum.Parse(typeof(Orientation), reader.GetAttribute(nameof(Orientation)), true);

                string canDockStr = reader.GetAttribute("CanDock");
                if (canDockStr != null)
                    canDock = bool.Parse(canDockStr);

                reader.Read();
                while (true)
                {
                    //Read all RootPanel children
                    if (ReadElement(reader) is ILayoutPanelElement element) result.Add(element);
                    else if (reader.NodeType == XmlNodeType.EndElement) break;
                }
            }

            reader.ReadEndElement();
            return result;
        }

        private List<object> ReadElementList(XmlReader reader, bool isFloatingWindow)
        {
            List<object> resultList = new List<object>();
            while (reader.NodeType == XmlNodeType.Whitespace) reader.Read();
            if (reader.NodeType == XmlNodeType.EndElement) return resultList;

            if (reader.IsEmptyElement)
            {
                reader.Read();
                return resultList;
            }

            string startElementName = reader.LocalName;
            reader.Read();
            if (reader.LocalName.Equals(startElementName) && reader.NodeType == XmlNodeType.EndElement) return null;

            while (reader.NodeType == XmlNodeType.Whitespace) reader.Read();

            while (true)
            {
                if (isFloatingWindow)
                {
                    if (!(ReadElement(reader) is LayoutFloatingWindow result)) break;
                    resultList.Add(result);
                }
                else
                {
                    if (!(ReadElement(reader) is LayoutAnchorable result)) break;
                    resultList.Add(result);
                }
            }

            reader.ReadEndElement();
            return resultList;
        }

        private object ReadElement(XmlReader reader)
        {
            while (reader.NodeType == XmlNodeType.Whitespace) reader.Read();
            if (reader.NodeType == XmlNodeType.EndElement) return null;

            Type typeToSerialize;
            switch (reader.LocalName)
            {
                case nameof(LayoutAnchorablePaneGroup):
                    typeToSerialize = typeof(LayoutAnchorablePaneGroup);
                    break;

                case nameof(LayoutAnchorablePane):
                    typeToSerialize = typeof(LayoutAnchorablePane);
                    break;

                case nameof(LayoutAnchorable):
                    typeToSerialize = typeof(LayoutAnchorable);
                    break;

                case nameof(LayoutDocumentPaneGroup):
                    typeToSerialize = typeof(LayoutDocumentPaneGroup);
                    break;

                case nameof(LayoutDocumentPane):
                    typeToSerialize = typeof(LayoutDocumentPane);
                    break;

                case nameof(LayoutDocument):
                    typeToSerialize = typeof(LayoutDocument);
                    break;

                case nameof(LayoutAnchorGroup):
                    typeToSerialize = typeof(LayoutAnchorGroup);
                    break;

                case nameof(LayoutPanel):
                    typeToSerialize = typeof(LayoutPanel);
                    break;

                case nameof(LayoutDocumentFloatingWindow):
                    typeToSerialize = typeof(LayoutDocumentFloatingWindow);
                    break;

                case nameof(LayoutAnchorableFloatingWindow):
                    typeToSerialize = typeof(LayoutAnchorableFloatingWindow);
                    break;

                case nameof(this.LeftSide):
                case nameof(this.RightSide):
                case nameof(this.TopSide):
                case nameof(this.BottomSide):
                    if (reader.IsEmptyElement)
                    {
                        reader.Read();
                        return null;
                    }
                    return reader.Read();

                default:
                    typeToSerialize = FindType(reader.LocalName);
                    if (typeToSerialize == null)
                        throw new ArgumentException("H.Controls.Dock.LayoutRoot doesn't know how to deserialize " + reader.LocalName);
                    break;
            }
            XmlSerializer serializer = XmlSerializersCache.GetSerializer(typeToSerialize);
            return serializer.Deserialize(reader);
        }

        #endregion Private Methods

        #region Diagnostic tools

#if DEBUG

        public void DumpTree(bool shortPropertyNames = false)
        {
            void DumpElement(ILayoutElement element, StringBuilder indent, int childID, bool isLastChild)
            {
                Debug.Write($"{indent}{(indent.Length > 0 ? isLastChild ? " └─ " : " ├─ " : "")}{childID:D2} 0x{element.GetHashCode():X8} " +
                                $"{element.GetType().Name} {(shortPropertyNames ? "P" : "Parent")}:0x{element.Parent?.GetHashCode() ?? 0:X8} " +
                                $"{(shortPropertyNames ? "R" : "Root")}:0x{element.Root?.GetHashCode() ?? 0:X8}");
                if (!(element is ILayoutContainer containerElement))
                {
                    Debug.WriteLine("");
                    return;
                }
                Debug.WriteLine($" {(shortPropertyNames ? "C" : "Children")}:{containerElement.ChildrenCount}");
                int nrChild = 0;
                indent.Append(isLastChild ? "   " : " │ ");
                foreach (ILayoutElement child in containerElement.Children)
                {
                    bool lastChild = nrChild == containerElement.ChildrenCount - 1;
                    DumpElement(child, indent, nrChild++, lastChild);
                }
                indent.Remove(indent.Length - 3, 3);
            }

            DumpElement(this, new StringBuilder(), 0, true);
        }

#endif

        #endregion Diagnostic tools
    }
}
