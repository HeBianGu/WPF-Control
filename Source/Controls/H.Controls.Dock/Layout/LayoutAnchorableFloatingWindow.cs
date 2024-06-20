








using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Markup;
using System.Xml;
using System.Xml.Serialization;

namespace H.Controls.Dock.Layout
{
    /// <summary>Implements the model for a floating window control that can host an anchorable control (tool window) in a floating window.</summary>
    [Serializable]
    [ContentProperty(nameof(RootPanel))]
    public class LayoutAnchorableFloatingWindow : LayoutFloatingWindow, ILayoutElementWithVisibility
    {
        #region fields

        private LayoutAnchorablePaneGroup _rootPanel;

        [NonSerialized]
        private bool _isVisible = true;

        #endregion fields

        #region Events

        /// <summary>Event is invoked when the visibility of this object has changed.</summary>
        public event EventHandler IsVisibleChanged;

        #endregion Events

        #region Properties

        public bool IsSinglePane => this.RootPanel != null && this.RootPanel.Descendents().OfType<ILayoutAnchorablePane>().Count(p => p.IsVisible) == 1;

        /// <summary>Gets/sets whether this object is in a state where it is visible in the UI or not.</summary>
        [XmlIgnore]
        public bool IsVisible
        {
            get => _isVisible;
            private set
            {
                if (value == _isVisible) return;
                RaisePropertyChanging(nameof(this.IsVisible));
                _isVisible = value;
                RaisePropertyChanged(nameof(this.IsVisible));
                IsVisibleChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public LayoutAnchorablePaneGroup RootPanel
        {
            get => _rootPanel;
            set
            {
                if (value == _rootPanel) return;
                RaisePropertyChanging(nameof(this.RootPanel));
                if (_rootPanel != null) _rootPanel.ChildrenTreeChanged -= _rootPanel_ChildrenTreeChanged;
                _rootPanel = value;
                if (_rootPanel != null)
                {
                    _rootPanel.Parent = this;
                    _rootPanel.ChildrenTreeChanged += _rootPanel_ChildrenTreeChanged;
                }

                RaisePropertyChanged(nameof(this.RootPanel));
                RaisePropertyChanged(nameof(this.IsSinglePane));
                RaisePropertyChanged(nameof(this.SinglePane));
                RaisePropertyChanged(nameof(this.Children));
                RaisePropertyChanged(nameof(this.ChildrenCount));
                ((ILayoutElementWithVisibility)this).ComputeVisibility();
            }
        }

        public ILayoutAnchorablePane SinglePane
        {
            get
            {
                if (!this.IsSinglePane) return null;
                LayoutAnchorablePane singlePane = this.RootPanel.Descendents().OfType<LayoutAnchorablePane>().Single(p => p.IsVisible);
                singlePane.UpdateIsDirectlyHostedInFloatingWindow();
                return singlePane;
            }
        }

        #endregion Properties

        #region ILayoutElementWithVisibility Interface

        /// <inheritdoc />
        void ILayoutElementWithVisibility.ComputeVisibility() => ComputeVisibility();

        #endregion ILayoutElementWithVisibility Interface

        #region Overrides

        /// <inheritdoc />
        public override IEnumerable<ILayoutElement> Children
        {
            get { if (this.ChildrenCount == 1) yield return this.RootPanel; }
        }

        /// <inheritdoc />
        public override void RemoveChild(ILayoutElement element)
        {
            Debug.Assert(element == this.RootPanel && element != null);
            this.RootPanel = null;
        }

        /// <inheritdoc />
        public override void ReplaceChild(ILayoutElement oldElement, ILayoutElement newElement)
        {
            Debug.Assert(oldElement == this.RootPanel && oldElement != null);
            this.RootPanel = newElement as LayoutAnchorablePaneGroup;
        }

        /// <inheritdoc />
        public override int ChildrenCount => this.RootPanel == null ? 0 : 1;

        /// <inheritdoc />
        public override bool IsValid => this.RootPanel != null;

        /// <inheritdoc />
        public override void ReadXml(XmlReader reader)
        {
            reader.MoveToContent();
            if (reader.IsEmptyElement)
            {
                reader.Read();
                ComputeVisibility();
                return;
            }

            string localName = reader.LocalName;
            reader.Read();

            while (true)
            {
                if (reader.LocalName.Equals(localName) && reader.NodeType == XmlNodeType.EndElement) break;

                if (reader.NodeType == XmlNodeType.Whitespace)
                {
                    reader.Read();
                    continue;
                }

                XmlSerializer serializer;
                if (reader.LocalName.Equals(nameof(LayoutAnchorablePaneGroup)))
                    serializer = XmlSerializersCache.GetSerializer<LayoutAnchorablePaneGroup>();
                else
                {
                    Type type = LayoutRoot.FindType(reader.LocalName);
                    if (type == null)
                        throw new ArgumentException("H.Controls.Dock.LayoutAnchorableFloatingWindow doesn't know how to deserialize " + reader.LocalName);
                    serializer = XmlSerializersCache.GetSerializer(type);
                }
                this.RootPanel = (LayoutAnchorablePaneGroup)serializer.Deserialize(reader);
            }
            reader.ReadEndElement();
        }

#if TRACE
        /// <inheritdoc />
        public override void ConsoleDump(int tab)
        {
            System.Diagnostics.Trace.Write(new string(' ', tab * 4));
            System.Diagnostics.Trace.WriteLine("FloatingAnchorableWindow()");

            this.RootPanel.ConsoleDump(tab + 1);
        }
#endif

        #endregion Overrides

        #region Private Methods

        private void _rootPanel_ChildrenTreeChanged(object sender, ChildrenTreeChangedEventArgs e)
        {
            RaisePropertyChanged(nameof(this.IsSinglePane));
            RaisePropertyChanged(nameof(this.SinglePane));
        }

        private void ComputeVisibility() => this.IsVisible = this.RootPanel != null && this.RootPanel.IsVisible;

        #endregion Private Methods
    }
}