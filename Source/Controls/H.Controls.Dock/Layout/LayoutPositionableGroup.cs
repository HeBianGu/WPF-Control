








using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace H.Controls.Dock.Layout
{
    /// <summary>Provides a base class for other layout panel models that support a specific class of panel.</summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public abstract class LayoutPositionableGroup<T> : LayoutGroup<T>, ILayoutPositionableElementWithActualSize where T : class, ILayoutElement
    {
        #region fields

        private static GridLengthConverter _gridLengthConverter = new GridLengthConverter();

        // DockWidth fields
        private GridLength _dockWidth = new GridLength(1.0, GridUnitType.Star);

        private double? _resizableAbsoluteDockWidth;

        // DockHeight fields
        private GridLength _dockHeight = new GridLength(1.0, GridUnitType.Star);

        private double? _resizableAbsoluteDockHeight;

        private bool _allowDuplicateContent = true;
        private bool _canRepositionItems = true;

        private double _dockMinWidth = 25.0;
        private double _dockMinHeight = 25.0;
        private double _floatingWidth = 0.0;
        private double _floatingHeight = 0.0;
        private double _floatingLeft = 0.0;
        private double _floatingTop = 0.0;

        private bool _isMaximized = false;

        [NonSerialized]
        private double _actualWidth;

        [NonSerialized]
        private double _actualHeight;

        #endregion fields

        #region Events

        /// <summary>
        /// Event fired when floating properties were updated.
        /// </summary>
        public event EventHandler FloatingPropertiesUpdated;

        #endregion Events

        #region Properties

        #region DockWidth

        public GridLength DockWidth
        {
            get => _dockWidth.IsAbsolute && _resizableAbsoluteDockWidth < _dockWidth.Value && _resizableAbsoluteDockWidth.HasValue ?
                        new GridLength(_resizableAbsoluteDockWidth.Value) : _dockWidth;
            set
            {
                if (value == _dockWidth || !(value.Value > 0)) return;
                if (value.IsAbsolute) _resizableAbsoluteDockWidth = value.Value;
                RaisePropertyChanging(nameof(this.DockWidth));
                _dockWidth = value;
                RaisePropertyChanged(nameof(this.DockWidth));
                OnDockWidthChanged();
            }
        }

        public double FixedDockWidth => _dockWidth.IsAbsolute && _dockWidth.Value >= _dockMinWidth ? _dockWidth.Value : _dockMinWidth;

        public double ResizableAbsoluteDockWidth
        {
            get => _resizableAbsoluteDockWidth ?? 0;
            set
            {
                if (!_dockWidth.IsAbsolute) return;
                if (value <= _dockWidth.Value && value > 0)
                {
                    RaisePropertyChanging(nameof(this.DockWidth));
                    _resizableAbsoluteDockWidth = value;
                    RaisePropertyChanged(nameof(this.DockWidth));
                    OnDockWidthChanged();
                }
                else if (value > _dockWidth.Value && _resizableAbsoluteDockWidth < _dockWidth.Value)
                    _resizableAbsoluteDockWidth = _dockWidth.Value;
            }
        }

        #endregion DockWidth

        #region DockHeight

        public GridLength DockHeight
        {
            get => _dockHeight.IsAbsolute && _resizableAbsoluteDockHeight < _dockHeight.Value && _resizableAbsoluteDockHeight.HasValue ?
                        new GridLength(_resizableAbsoluteDockHeight.Value) : _dockHeight;
            set
            {
                if (_dockHeight == value || !(value.Value > 0)) return;
                if (value.IsAbsolute) _resizableAbsoluteDockHeight = value.Value;
                RaisePropertyChanging(nameof(this.DockHeight));
                _dockHeight = value;
                RaisePropertyChanged(nameof(this.DockHeight));
                OnDockHeightChanged();
            }
        }

        public double FixedDockHeight => _dockHeight.IsAbsolute && _dockHeight.Value >= _dockMinHeight ? _dockHeight.Value : _dockMinHeight;

        public double ResizableAbsoluteDockHeight
        {
            get => _resizableAbsoluteDockHeight ?? 0;
            set
            {
                if (!_dockHeight.IsAbsolute) return;
                if (value < _dockHeight.Value && value > 0)
                {
                    RaisePropertyChanging(nameof(this.DockHeight));
                    _resizableAbsoluteDockHeight = value;
                    RaisePropertyChanged(nameof(this.DockHeight));
                    OnDockHeightChanged();
                }
                else if (value > _dockHeight.Value && _resizableAbsoluteDockHeight < _dockHeight.Value)
                    _resizableAbsoluteDockHeight = _dockHeight.Value;
                else if (value == 0) _resizableAbsoluteDockHeight = this.DockMinHeight;
            }
        }

        #endregion DockHeight

        /// <summary>
        /// Gets or sets the AllowDuplicateContent property.
        /// When this property is true, then the LayoutDocumentPane or LayoutAnchorablePane allows dropping
        /// duplicate content (according to its Title and ContentId). When this dependency property is false,
        /// then the LayoutDocumentPane or LayoutAnchorablePane hides the OverlayWindow.DropInto button to prevent dropping of duplicate content.
        /// </summary>
        public bool AllowDuplicateContent
        {
            get => _allowDuplicateContent;
            set
            {
                if (value == _allowDuplicateContent) return;
                RaisePropertyChanging(nameof(this.AllowDuplicateContent));
                _allowDuplicateContent = value;
                RaisePropertyChanged(nameof(this.AllowDuplicateContent));
            }
        }

        public bool CanRepositionItems
        {
            get => _canRepositionItems;
            set
            {
                if (value == _canRepositionItems) return;
                RaisePropertyChanging(nameof(this.CanRepositionItems));
                _canRepositionItems = value;
                RaisePropertyChanged(nameof(this.CanRepositionItems));
            }
        }

        public double CalculatedDockMinWidth()
        {
            double childrenDockMinWidth = 0.0;
            System.Collections.Generic.List<ILayoutPositionableElement> visibleChildren = this.Children.OfType<ILayoutPositionableElement>().Where(child => child.IsVisible).ToList();
            if (this is ILayoutOrientableGroup orientableGroup && visibleChildren.Any())
            {
                childrenDockMinWidth = orientableGroup.Orientation == Orientation.Vertical ?
                    visibleChildren.Max(child => child.CalculatedDockMinWidth())
                  : visibleChildren.Sum(child => child.CalculatedDockMinWidth() + ((this.Root?.Manager?.GridSplitterWidth ?? 0) * (visibleChildren.Count - 1)));
            }
            return Math.Max(this._dockMinWidth, childrenDockMinWidth);
        }

        /// <summary>
        /// Defines the smallest available width that can be applied to a deriving element.
        ///
        /// The system ensures the minimum width by blocking/limiting <see cref="GridSplitter"/>
        /// movement when the user resizes a deriving element or resizes the main window.
        /// </summary>
        public double DockMinWidth
        {
            get => _dockMinWidth;
            set
            {
                if (value == _dockMinWidth) return;
                MathHelper.AssertIsPositiveOrZero(value);
                RaisePropertyChanging(nameof(this.DockMinWidth));
                _dockMinWidth = value;
                RaisePropertyChanged(nameof(this.DockMinWidth));
            }
        }

        public double CalculatedDockMinHeight()
        {
            double childrenDockMinHeight = 0.0;
            System.Collections.Generic.List<ILayoutPositionableElement> visibleChildren = this.Children.OfType<ILayoutPositionableElement>().Where(child => child.IsVisible).ToList();
            if (this is ILayoutOrientableGroup orientableGroup && visibleChildren.Any())
            {
                childrenDockMinHeight = orientableGroup.Orientation == Orientation.Vertical ?
                    visibleChildren.Sum(child => child.CalculatedDockMinHeight() + ((this.Root?.Manager?.GridSplitterHeight ?? 0) * (visibleChildren.Count - 1)))
                  : visibleChildren.Max(child => child.CalculatedDockMinHeight());
            }

            return Math.Max(this._dockMinHeight, childrenDockMinHeight);
        }

        /// <summary>
        /// Defines the smallest available height that can be applied to a deriving element.
        ///
        /// The system ensures the minimum height by blocking/limiting <see cref="GridSplitter"/>
        /// movement when the user resizes a deriving element or resizes the main window.
        /// </summary>
        public double DockMinHeight
        {
            get => _dockMinHeight;
            set
            {
                if (value == _dockMinHeight) return;
                MathHelper.AssertIsPositiveOrZero(value);
                RaisePropertyChanging(nameof(this.DockMinHeight));
                _dockMinHeight = value;
                RaisePropertyChanged(nameof(this.DockMinHeight));
            }
        }

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

        public double FloatingHeight
        {
            get => _floatingHeight;
            set
            {
                if (_floatingHeight == value) return;
                RaisePropertyChanging(nameof(this.FloatingHeight));
                _floatingHeight = value;
                RaisePropertyChanged(nameof(this.FloatingHeight));
            }
        }

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

        public bool IsMaximized
        {
            get => _isMaximized;
            set
            {
                if (value == _isMaximized) return;
                _isMaximized = value;
                RaisePropertyChanged(nameof(this.IsMaximized));
            }
        }

        double ILayoutPositionableElementWithActualSize.ActualWidth
        {
            get => _actualWidth;
            set => _actualWidth = value;
        }

        double ILayoutPositionableElementWithActualSize.ActualHeight
        {
            get => _actualHeight;
            set => _actualHeight = value;
        }

        #endregion Properties

        #region Internal Methods

        void ILayoutElementForFloatingWindow.RaiseFloatingPropertiesUpdated() => FloatingPropertiesUpdated?.Invoke(this, EventArgs.Empty);

        #endregion Internal Methods

        #region Overrides

        public override void WriteXml(System.Xml.XmlWriter writer)
        {
            if (this.DockWidth.Value != 1.0 || !this.DockWidth.IsStar)
                writer.WriteAttributeString(nameof(this.DockWidth), _gridLengthConverter.ConvertToInvariantString(this.DockWidth.IsAbsolute ? new GridLength(this.FixedDockWidth) : this.DockWidth));
            if (this.DockHeight.Value != 1.0 || !this.DockHeight.IsStar)
                writer.WriteAttributeString(nameof(this.DockHeight), _gridLengthConverter.ConvertToInvariantString(this.DockHeight.IsAbsolute ? new GridLength(this.FixedDockHeight) : this.DockHeight));
            if (this.DockMinWidth != 25.0) writer.WriteAttributeString(nameof(this.DockMinWidth), this.DockMinWidth.ToString(CultureInfo.InvariantCulture));
            if (this.DockMinHeight != 25.0) writer.WriteAttributeString(nameof(this.DockMinHeight), this.DockMinHeight.ToString(CultureInfo.InvariantCulture));
            if (this.FloatingWidth != 0.0) writer.WriteAttributeString(nameof(this.FloatingWidth), this.FloatingWidth.ToString(CultureInfo.InvariantCulture));
            if (this.FloatingHeight != 0.0) writer.WriteAttributeString(nameof(this.FloatingHeight), this.FloatingHeight.ToString(CultureInfo.InvariantCulture));
            if (this.FloatingLeft != 0.0) writer.WriteAttributeString(nameof(this.FloatingLeft), this.FloatingLeft.ToString(CultureInfo.InvariantCulture));
            if (this.FloatingTop != 0.0) writer.WriteAttributeString(nameof(this.FloatingTop), this.FloatingTop.ToString(CultureInfo.InvariantCulture));
            if (this.IsMaximized) writer.WriteAttributeString(nameof(this.IsMaximized), this.IsMaximized.ToString());
            base.WriteXml(writer);
        }

        public override void ReadXml(System.Xml.XmlReader reader)
        {
            if (reader.MoveToAttribute(nameof(this.DockWidth))) _dockWidth = (GridLength)_gridLengthConverter.ConvertFromInvariantString(reader.Value);
            if (reader.MoveToAttribute(nameof(this.DockHeight))) _dockHeight = (GridLength)_gridLengthConverter.ConvertFromInvariantString(reader.Value);
            if (reader.MoveToAttribute(nameof(this.DockMinWidth))) _dockMinWidth = double.Parse(reader.Value, CultureInfo.InvariantCulture);
            if (reader.MoveToAttribute(nameof(this.DockMinHeight))) _dockMinHeight = double.Parse(reader.Value, CultureInfo.InvariantCulture);
            if (reader.MoveToAttribute(nameof(this.FloatingWidth))) _floatingWidth = double.Parse(reader.Value, CultureInfo.InvariantCulture);
            if (reader.MoveToAttribute(nameof(this.FloatingHeight))) _floatingHeight = double.Parse(reader.Value, CultureInfo.InvariantCulture);
            if (reader.MoveToAttribute(nameof(this.FloatingLeft))) _floatingLeft = double.Parse(reader.Value, CultureInfo.InvariantCulture);
            if (reader.MoveToAttribute(nameof(this.FloatingTop))) _floatingTop = double.Parse(reader.Value, CultureInfo.InvariantCulture);
            if (reader.MoveToAttribute(nameof(this.IsMaximized))) _isMaximized = bool.Parse(reader.Value);
            base.ReadXml(reader);
        }

        protected virtual void OnDockWidthChanged()
        {
        }

        protected virtual void OnDockHeightChanged()
        {
        }

        #endregion Overrides
    }
}