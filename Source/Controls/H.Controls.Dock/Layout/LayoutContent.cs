








using H.Controls.Dock.Controls;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media;
using System.Xml.Serialization;

namespace H.Controls.Dock.Layout
{
    /// <summary>
    /// Provides an abstract base class for common properties and methods of
    /// the <see cref="LayoutAnchorable"/> and <see cref="LayoutDocument"/> classes.
    /// </summary>
    [ContentProperty(nameof(Content))]
    [Serializable]
    public abstract class LayoutContent : LayoutElement, IXmlSerializable, ILayoutElementForFloatingWindow, IComparable<LayoutContent>, ILayoutPreviousContainer
    {
        #region Constructors

        /// <summary>
        /// Class constructor
        /// </summary>
        internal LayoutContent()
        {
        }

        #endregion Constructors

        #region Events

        /// <summary>Event fired when the content is closed (i.e. removed definitely from the layout).</summary>
        public event EventHandler Closed;

        /// <summary>
        /// Event fired when the content is about to be closed (i.e. removed definitely from the layout)
        /// </summary>
        /// <remarks>Please note that <see cref="LayoutAnchorable"/> also can be hidden. Usually user hide anchorables when click the 'X' button. To completely close
        /// an anchorable the user should click the 'Close' menu item from the context menu. When an <see cref="LayoutAnchorable"/> is hidden its visibility changes to false and
        /// <see cref="LayoutAnchorable.IsHidden"/> property is set to true.
        /// Handle the Hiding event for the <see cref="LayoutAnchorable"/> to cancel the hide operation.</remarks>
        public event EventHandler<CancelEventArgs> Closing;

        /// <summary>
        /// Event fired when floating properties were updated.
        /// </summary>
        public event EventHandler FloatingPropertiesUpdated;

        #endregion Events

        #region Properties

        #region Title

        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(nameof(Title), typeof(string), typeof(LayoutContent), new UIPropertyMetadata(null, OnTitlePropertyChanged, CoerceTitleValue));

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        private static object CoerceTitleValue(DependencyObject obj, object value)
        {
            LayoutContent lc = (LayoutContent)obj;
            if ((string)value != lc.Title) lc.RaisePropertyChanging(TitleProperty.Name);
            return value;
        }

        private static void OnTitlePropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args) => ((LayoutContent)obj).RaisePropertyChanged(TitleProperty.Name);

        #endregion Title

        #region Content

        [NonSerialized]
        private object _content = null;

        [XmlIgnore]
        public object Content
        {
            get => _content;
            set
            {
                if (value == _content) return;
                RaisePropertyChanging(nameof(this.Content));
                _content = value;
                RaisePropertyChanged(nameof(this.Content));
                if (this.ContentId == null) SetContentIdFromContent();
            }
        }

        #endregion Content

        #region ContentId

        public static readonly DependencyProperty ContentIdProperty = DependencyProperty.Register(nameof(ContentId), typeof(string), typeof(LayoutContent), new UIPropertyMetadata(null, OnContentIdPropertyChanged));

        public string ContentId
        {
            get
            {
                string value = (string)GetValue(ContentIdProperty);
                if (!string.IsNullOrWhiteSpace(value)) return value;
                // #83 - if Content.Name is empty at setting content and will be set later, ContentId will stay null.
                SetContentIdFromContent();
                return (string)GetValue(ContentIdProperty);
            }
            set => SetValue(ContentIdProperty, value);
        }

        private static void OnContentIdPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            if (obj is LayoutContent layoutContent) layoutContent.OnContentIdPropertyChanged((string)args.OldValue, (string)args.NewValue);
        }

        private void OnContentIdPropertyChanged(string oldValue, string newValue)
        {
            if (oldValue != newValue) RaisePropertyChanged(nameof(this.ContentId));
        }

        private void SetContentIdFromContent()
        {
            FrameworkElement contentAsControl = _content as FrameworkElement;
            if (!string.IsNullOrWhiteSpace(contentAsControl?.Name)) SetCurrentValue(ContentIdProperty, contentAsControl.Name);
        }

        #endregion ContentId

        #region IsSelected

        private bool _isSelected = false;

        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                if (value == _isSelected) return;
                bool oldValue = _isSelected;
                RaisePropertyChanging(nameof(this.IsSelected));
                _isSelected = value;
                if (this.Parent is ILayoutContentSelector parentSelector) parentSelector.SelectedContentIndex = _isSelected ? parentSelector.IndexOf(this) : -1;
                OnIsSelectedChanged(oldValue, value);
                RaisePropertyChanged(nameof(this.IsSelected));
                LayoutAnchorableTabItem.CancelMouseLeave();
            }
        }

        /// <summary>
        /// Provides derived classes an opportunity to handle changes to the <see cref="IsSelected"/> property.
        /// </summary>
        protected virtual void OnIsSelectedChanged(bool oldValue, bool newValue) => IsSelectedChanged?.Invoke(this, EventArgs.Empty);

        public event EventHandler IsSelectedChanged;

        #endregion IsSelected

        #region IsActive

        [field: NonSerialized]
        private bool _isActive = false;

        [XmlIgnore]
        public bool IsActive
        {
            get => _isActive;
            set
            {
                if (value == _isActive) return;
                RaisePropertyChanging(nameof(this.IsActive));
                bool oldValue = _isActive;
                _isActive = value;
                ILayoutRoot root = this.Root;
                if (root != null)
                {
                    if (root.ActiveContent != this && value) this.Root.ActiveContent = this;
                    if (_isActive && root.ActiveContent != this) root.ActiveContent = this;
                }
                if (_isActive) this.IsSelected = true;
                OnIsActiveChanged(oldValue, value);
                RaisePropertyChanged(nameof(this.IsActive));
            }
        }

        /// <summary>
        /// Provides derived classes an opportunity to handle changes to the <see cref="IsActive"/> property.
        /// </summary>
        protected virtual void OnIsActiveChanged(bool oldValue, bool newValue)
        {
            if (newValue) this.LastActivationTimeStamp = DateTime.Now;
            IsActiveChanged?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler IsActiveChanged;

        #endregion IsActive

        #region IsLastFocusedDocument

        private bool _isLastFocusedDocument = false;

        public bool IsLastFocusedDocument
        {
            get => _isLastFocusedDocument;
            internal set
            {
                if (value == _isLastFocusedDocument) return;
                RaisePropertyChanging(nameof(this.IsLastFocusedDocument));
                _isLastFocusedDocument = value;
                RaisePropertyChanged(nameof(this.IsLastFocusedDocument));
            }
        }

        #endregion IsLastFocusedDocument

        #region PreviousContainer

        [field: NonSerialized]
        private ILayoutContainer _previousContainer = null;

        [XmlIgnore]
        ILayoutContainer ILayoutPreviousContainer.PreviousContainer
        {
            get => _previousContainer;
            set
            {
                if (value == _previousContainer) return;
                _previousContainer = value;
                RaisePropertyChanged(nameof(this.PreviousContainer));
                if (_previousContainer is ILayoutPaneSerializable paneSerializable && paneSerializable.Id == null)
                    paneSerializable.Id = Guid.NewGuid().ToString();
            }
        }

        protected ILayoutContainer PreviousContainer
        {
            get => ((ILayoutPreviousContainer)this).PreviousContainer;
            set => ((ILayoutPreviousContainer)this).PreviousContainer = value;
        }

        [XmlIgnore]
        string ILayoutPreviousContainer.PreviousContainerId { get; set; }

        protected string PreviousContainerId
        {
            get => ((ILayoutPreviousContainer)this).PreviousContainerId;
            set => ((ILayoutPreviousContainer)this).PreviousContainerId = value;
        }

        #endregion PreviousContainer

        #region PreviousContainerIndex

        [field: NonSerialized]
        private int _previousContainerIndex = -1;

        [XmlIgnore]
        public int PreviousContainerIndex
        {
            get => _previousContainerIndex;
            set
            {
                if (value == _previousContainerIndex) return;
                _previousContainerIndex = value;
                RaisePropertyChanged(nameof(this.PreviousContainerIndex));
            }
        }

        #endregion PreviousContainerIndex

        #region LastActivationTimeStamp

        private DateTime? _lastActivationTimeStamp = null;

        public DateTime? LastActivationTimeStamp
        {
            get => _lastActivationTimeStamp;
            set
            {
                if (value == _lastActivationTimeStamp) return;
                _lastActivationTimeStamp = value;
                RaisePropertyChanged(nameof(this.LastActivationTimeStamp));
            }
        }

        #endregion LastActivationTimeStamp

        #region FloatingWidth

        private double _floatingWidth = 0.0;

        public double FloatingWidth
        {
            get => _floatingWidth;
            set
            {
                if (value == _floatingWidth) return;
                RaisePropertyChanging(nameof(this.FloatingWidth));
                _floatingWidth = value;
                RaisePropertyChanged(nameof(this.FloatingWidth));
            }
        }

        #endregion FloatingWidth

        #region FloatingHeight

        private double _floatingHeight = 0.0;

        public double FloatingHeight
        {
            get => _floatingHeight;
            set
            {
                if (value == _floatingHeight) return;
                RaisePropertyChanging(nameof(this.FloatingHeight));
                _floatingHeight = value;
                RaisePropertyChanged(nameof(this.FloatingHeight));
            }
        }

        #endregion FloatingHeight

        #region FloatingLeft

        private double _floatingLeft = 0.0;

        public double FloatingLeft
        {
            get => _floatingLeft;
            set
            {
                if (value == _floatingLeft) return;
                RaisePropertyChanging(nameof(this.FloatingLeft));
                _floatingLeft = value;
                RaisePropertyChanged(nameof(this.FloatingLeft));
            }
        }

        #endregion FloatingLeft

        #region FloatingTop

        private double _floatingTop = 0.0;

        public double FloatingTop
        {
            get => _floatingTop;
            set
            {
                if (value == _floatingTop) return;
                RaisePropertyChanging(nameof(this.FloatingTop));
                _floatingTop = value;
                RaisePropertyChanged(nameof(this.FloatingTop));
            }
        }

        #endregion FloatingTop

        #region IsMaximized

        private bool _isMaximized = false;

        public bool IsMaximized
        {
            get => _isMaximized;
            set
            {
                if (value == _isMaximized) return;
                RaisePropertyChanging(nameof(this.IsMaximized));
                _isMaximized = value;
                RaisePropertyChanged(nameof(this.IsMaximized));
            }
        }

        #endregion IsMaximized

        #region ToolTip

        private object _toolTip = null;

        public object ToolTip
        {
            get => _toolTip;
            set
            {
                if (value == _toolTip) return;
                _toolTip = value;
                RaisePropertyChanged(nameof(this.ToolTip));
            }
        }

        #endregion ToolTip

        /// <summary>Gets whether the content is currently floating or not.</summary>
        [Bindable(true), Description("Gets whether the content is currently floating or not."), Category("Other")]
        public bool IsFloating => this.FindParent<LayoutFloatingWindow>() != null;

        #region IconSource

        private ImageSource _iconSource = null;

        public ImageSource IconSource
        {
            get => _iconSource;
            set
            {
                if (value == _iconSource) return;
                _iconSource = value;
                RaisePropertyChanged(nameof(this.IconSource));
            }
        }

        #endregion IconSource

        #region CanClose

        // BD: 14.08.2020 added _canCloseDefault to properly implement inverting _canClose default value in inheritors (e.g. LayoutAnchorable)
        //     Thus CanClose property will be serialized only when not equal to its default for given class
        //     With previous code it was not possible to serialize CanClose if set to true for LayoutAnchorable instance
        internal bool _canClose = true, _canCloseDefault = true;

        public bool CanClose
        {
            get => _canClose;
            set
            {
                if (_canClose == value) return;
                _canClose = value;
                RaisePropertyChanged(nameof(this.CanClose));
            }
        }

        #endregion CanClose

        #region CanFloat

        private bool _canFloat = true;

        public bool CanFloat
        {
            get => _canFloat;
            set
            {
                if (value == _canFloat) return;
                _canFloat = value;
                RaisePropertyChanged(nameof(this.CanFloat));
            }
        }

        #endregion CanFloat

        #region CanShowOnHover

        private bool _canShowOnHover = true;

        /// <summary>
        /// Set to false to disable the behavior of auto-showing
        /// a <see cref="LayoutAnchorableControl"/> on mouse over.
        /// When true, hovering the mouse over an anchorable tab 
        /// will cause the anchorable to show itself.
        /// </summary>
        /// <remarks>Defaults to true</remarks>
        public bool CanShowOnHover
        {
            get => _canShowOnHover;
            set
            {
                if (value == _canShowOnHover) return;
                _canShowOnHover = value;
                RaisePropertyChanged(nameof(this.CanShowOnHover));
            }
        }

        #endregion CanShowOnHover

        #region IsEnabled

        private bool _isEnabled = true;

        public bool IsEnabled
        {
            get => _isEnabled;
            set
            {
                if (value == _isEnabled) return;
                _isEnabled = value;
                RaisePropertyChanged(nameof(this.IsEnabled));
            }
        }

        #endregion IsEnabled

        #region TabItem

        public LayoutDocumentTabItem TabItem { get; internal set; }

        #endregion TabItem

        #endregion Properties

        #region Public Methods

        /// <summary>Close the content</summary>
        /// <remarks>Note that the anchorable is only hidden (not closed). By default when user click the X button it only hides the content.</remarks>
        public abstract void Close();

        /// <inheritdoc />
        public System.Xml.Schema.XmlSchema GetSchema() => null;

        /// <inheritdoc />
        public virtual void ReadXml(System.Xml.XmlReader reader)
        {
            if (reader.MoveToAttribute(nameof(this.Title)))
                this.Title = reader.Value;
            //if (reader.MoveToAttribute("IconSource"))
            //    IconSource = new Uri(reader.Value, UriKind.RelativeOrAbsolute);

            if (reader.MoveToAttribute(nameof(this.IsSelected)))
                this.IsSelected = bool.Parse(reader.Value);
            if (reader.MoveToAttribute(nameof(this.ContentId)))
                this.ContentId = reader.Value;
            if (reader.MoveToAttribute(nameof(this.IsLastFocusedDocument)))
                this.IsLastFocusedDocument = bool.Parse(reader.Value);
            if (reader.MoveToAttribute(nameof(this.PreviousContainerId)))
                this.PreviousContainerId = reader.Value;
            if (reader.MoveToAttribute(nameof(this.PreviousContainerIndex)))
                this.PreviousContainerIndex = int.Parse(reader.Value);

            if (reader.MoveToAttribute(nameof(this.FloatingLeft)))
                this.FloatingLeft = double.Parse(reader.Value, CultureInfo.InvariantCulture);
            if (reader.MoveToAttribute(nameof(this.FloatingTop)))
                this.FloatingTop = double.Parse(reader.Value, CultureInfo.InvariantCulture);
            if (reader.MoveToAttribute(nameof(this.FloatingWidth)))
                this.FloatingWidth = double.Parse(reader.Value, CultureInfo.InvariantCulture);
            if (reader.MoveToAttribute(nameof(this.FloatingHeight)))
                this.FloatingHeight = double.Parse(reader.Value, CultureInfo.InvariantCulture);
            if (reader.MoveToAttribute(nameof(this.IsMaximized)))
                this.IsMaximized = bool.Parse(reader.Value);
            if (reader.MoveToAttribute(nameof(this.CanClose)))
                this.CanClose = bool.Parse(reader.Value);
            if (reader.MoveToAttribute(nameof(this.CanFloat)))
                this.CanFloat = bool.Parse(reader.Value);
            if (reader.MoveToAttribute(nameof(this.LastActivationTimeStamp)))
                this.LastActivationTimeStamp = DateTime.Parse(reader.Value, CultureInfo.InvariantCulture);
            if (reader.MoveToAttribute(nameof(this.CanShowOnHover)))
                this.CanShowOnHover = bool.Parse(reader.Value);

            reader.Read();
        }

        /// <inheritdoc />
        public virtual void WriteXml(System.Xml.XmlWriter writer)
        {
            if (!string.IsNullOrWhiteSpace(this.Title))
                writer.WriteAttributeString(nameof(this.Title), this.Title);

            //if (IconSource != null)
            //    writer.WriteAttributeString("IconSource", IconSource.ToString());

            if (this.IsSelected)
                writer.WriteAttributeString(nameof(this.IsSelected), this.IsSelected.ToString());

            if (this.IsLastFocusedDocument)
                writer.WriteAttributeString(nameof(this.IsLastFocusedDocument), this.IsLastFocusedDocument.ToString());

            if (!string.IsNullOrWhiteSpace(this.ContentId))
                writer.WriteAttributeString(nameof(this.ContentId), this.ContentId);

            if (this.ToolTip is string toolTip && !string.IsNullOrWhiteSpace(toolTip))
                writer.WriteAttributeString(nameof(this.ToolTip), toolTip);

            if (this.FloatingLeft != 0.0) writer.WriteAttributeString(nameof(this.FloatingLeft), this.FloatingLeft.ToString(CultureInfo.InvariantCulture));
            if (this.FloatingTop != 0.0) writer.WriteAttributeString(nameof(this.FloatingTop), this.FloatingTop.ToString(CultureInfo.InvariantCulture));
            if (this.FloatingWidth != 0.0) writer.WriteAttributeString(nameof(this.FloatingWidth), this.FloatingWidth.ToString(CultureInfo.InvariantCulture));
            if (this.FloatingHeight != 0.0) writer.WriteAttributeString(nameof(this.FloatingHeight), this.FloatingHeight.ToString(CultureInfo.InvariantCulture));

            if (this.IsMaximized) writer.WriteAttributeString(nameof(this.IsMaximized), this.IsMaximized.ToString());
            // BD: 14.08.2020 changed to check CanClose value against the default in _canCloseDefault
            //     thus CanClose property will be serialized only when not equal to its default for given class
            //     With previous code it was not possible to serialize CanClose if set to true for LayoutAnchorable instance
            if (this.CanClose != _canCloseDefault) writer.WriteAttributeString(nameof(this.CanClose), this.CanClose.ToString());
            if (!this.CanFloat) writer.WriteAttributeString(nameof(this.CanFloat), this.CanFloat.ToString());

            if (this.LastActivationTimeStamp != null) writer.WriteAttributeString(nameof(this.LastActivationTimeStamp), this.LastActivationTimeStamp.Value.ToString(CultureInfo.InvariantCulture));

            if (!this.CanShowOnHover) writer.WriteAttributeString(nameof(this.CanShowOnHover), this.CanShowOnHover.ToString());

            if (_previousContainer is ILayoutPaneSerializable paneSerializable)
            {
                writer.WriteAttributeString("PreviousContainerId", paneSerializable.Id);
                writer.WriteAttributeString("PreviousContainerIndex", _previousContainerIndex.ToString());
            }
        }

        public int CompareTo(LayoutContent other)
        {
            if (this.Content is IComparable contentAsComparable)
                return contentAsComparable.CompareTo(other.Content);
            return string.Compare(this.Title, other.Title);
        }

        /// <summary>Float the content in a popup window</summary>
        public void Float()
        {
            if (this.PreviousContainer != null && this.PreviousContainer.FindParent<LayoutFloatingWindow>() != null)
            {
                ILayoutPane currentContainer = this.Parent as ILayoutPane;
                int currentContainerIndex = (currentContainer as ILayoutGroup).IndexOfChild(this);
                ILayoutGroup previousContainerAsLayoutGroup = this.PreviousContainer as ILayoutGroup;

                if (this.PreviousContainerIndex < previousContainerAsLayoutGroup.ChildrenCount)
                    previousContainerAsLayoutGroup.InsertChildAt(this.PreviousContainerIndex, this);
                else
                    previousContainerAsLayoutGroup.InsertChildAt(previousContainerAsLayoutGroup.ChildrenCount, this);

                this.PreviousContainer = currentContainer;
                this.PreviousContainerIndex = currentContainerIndex;
                this.IsSelected = true;
                this.IsActive = true;
                this.Root.CollectGarbage();
            }
            else
            {
                this.Root.Manager.StartDraggingFloatingWindowForContent(this, false);
                this.IsSelected = true;
                this.IsActive = true;
            }

            // BD: 14.08.2020 raise IsFloating property changed
            RaisePropertyChanged(nameof(this.IsFloating));
        }

        /// <summary>Dock the content as document.</summary>
        public void DockAsDocument()
        {
            if (!(this.Root is LayoutRoot root)) throw new InvalidOperationException();

            if (this.PreviousContainer is LayoutDocumentPane)
            {
                Dock();
                return;
            }

            LayoutDocumentPane newParentPane;
            if (root.LastFocusedDocument != null)
                newParentPane = root.LastFocusedDocument.Parent as LayoutDocumentPane;
            else
                newParentPane = root.Descendents().OfType<LayoutDocumentPane>().FirstOrDefault();

            if (newParentPane != null)
            {
                newParentPane.Children.Add(this);
                root.CollectGarbage();
            }
            this.IsSelected = true;
            this.IsActive = true;

            // BD: 14.08.2020 raise IsFloating property changed
            RaisePropertyChanged(nameof(this.IsFloating));
        }

        /// <summary>Re-dock the content to its previous container</summary>
        public void Dock()
        {
            if (this.PreviousContainer != null)
            {
                ILayoutContainer currentContainer = this.Parent;
                int currentContainerIndex = currentContainer is ILayoutGroup ? (currentContainer as ILayoutGroup).IndexOfChild(this) : -1;
                ILayoutGroup previousContainerAsLayoutGroup = this.PreviousContainer as ILayoutGroup;

                if (this.PreviousContainerIndex < previousContainerAsLayoutGroup.ChildrenCount)
                    previousContainerAsLayoutGroup.InsertChildAt(this.PreviousContainerIndex, this);
                else
                    previousContainerAsLayoutGroup.InsertChildAt(previousContainerAsLayoutGroup.ChildrenCount, this);

                if (currentContainerIndex > -1)
                {
                    this.PreviousContainer = currentContainer;
                    this.PreviousContainerIndex = currentContainerIndex;
                }
                else
                {
                    this.PreviousContainer = null;
                    this.PreviousContainerIndex = 0;
                }

                this.IsSelected = true;
                this.IsActive = true;
            }
            else
                InternalDock();

            this.Root.CollectGarbage();

            // BD: 14.08.2020 raise IsFloating property changed
            RaisePropertyChanged(nameof(this.IsFloating));
        }

        #endregion Public Methods

        #region Overrides

        /// <inheritdoc />
        protected override void OnParentChanging(ILayoutContainer oldValue, ILayoutContainer newValue)
        {
            if (oldValue != null) this.IsSelected = false;

            base.OnParentChanging(oldValue, newValue);
        }

        /// <inheritdoc />
        protected override void OnParentChanged(ILayoutContainer oldValue, ILayoutContainer newValue)
        {
            if (this.IsSelected && this.Parent is ILayoutContentSelector)
            {
                ILayoutContentSelector parentSelector = this.Parent as ILayoutContentSelector;
                parentSelector.SelectedContentIndex = parentSelector.IndexOf(this);
            }

            base.OnParentChanged(oldValue, newValue);
        }

        #endregion Overrides

        #region Internal Methods

        /// <summary>Test if the content can be closed. </summary>
        /// <returns></returns>
        internal bool TestCanClose()
        {
            CancelEventArgs args = new CancelEventArgs();
            OnClosing(args);
            return !args.Cancel;
        }

        internal void CloseInternal()
        {
            ILayoutRoot root = this.Root;
            ILayoutContainer parentAsContainer = this.Parent;

            if (this.PreviousContainer == null)
            {
                ILayoutGroup parentAsGroup = this.Parent as ILayoutGroup;
                this.PreviousContainer = parentAsContainer;
                this.PreviousContainerIndex = parentAsGroup.IndexOfChild(this);

                if (parentAsGroup is ILayoutPaneSerializable layoutPaneSerializable)
                {
                    this.PreviousContainerId = layoutPaneSerializable.Id;
                    // This parentAsGroup will be removed in the GarbageCollection below
                    if (parentAsGroup.Children.Count() == 1 && parentAsGroup.Parent != null && this.Root.Manager != null)
                    {
                        this.Parent = this.Root.Manager.Layout;
                        this.PreviousContainer = parentAsGroup.Parent;
                        this.PreviousContainerIndex = -1;

                        if (parentAsGroup.Parent is ILayoutPaneSerializable paneSerializable)
                            this.PreviousContainerId = paneSerializable.Id;
                        else
                            this.PreviousContainerId = null;
                    }
                }
            }

            parentAsContainer.RemoveChild(this);
            root?.CollectGarbage();
            OnClosed();
        }

        protected virtual void OnClosed() => Closed?.Invoke(this, EventArgs.Empty);

        protected virtual void OnClosing(CancelEventArgs args) => Closing?.Invoke(this, args);

        protected virtual void InternalDock()
        {
        }

        void ILayoutElementForFloatingWindow.RaiseFloatingPropertiesUpdated() => FloatingPropertiesUpdated?.Invoke(this, EventArgs.Empty);

        #endregion Internal Methods
    }
}
