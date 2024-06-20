








using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Markup;
using System.Xml;
using System.Xml.Serialization;

namespace H.Controls.Dock.Layout
{
    /// <summary>Implements the layout model for the <see cref="Controls.LayoutDocumentFloatingWindowControl"/>.</summary>
    [ContentProperty(nameof(RootPanel))]
    [Serializable]
    public class LayoutDocumentFloatingWindow : LayoutFloatingWindow, ILayoutElementWithVisibility
    {
        #region fields

        private LayoutDocumentPaneGroup _rootPanel = null;

        [NonSerialized]
        private bool _isVisible = true;

        #endregion fields

        public event EventHandler IsVisibleChanged;

        #region Properties

        #region RootPanel

        public LayoutDocumentPaneGroup RootPanel
        {
            get => _rootPanel;
            set
            {
                if (_rootPanel == value) return;
                if (_rootPanel != null) _rootPanel.ChildrenTreeChanged -= _rootPanel_ChildrenTreeChanged;

                _rootPanel = value;
                if (_rootPanel != null) _rootPanel.Parent = this;
                if (_rootPanel != null) _rootPanel.ChildrenTreeChanged += _rootPanel_ChildrenTreeChanged;

                RaisePropertyChanged(nameof(this.RootPanel));
                RaisePropertyChanged(nameof(this.IsSinglePane));
                RaisePropertyChanged(nameof(this.SinglePane));
                RaisePropertyChanged(nameof(this.Children));
                RaisePropertyChanged(nameof(this.ChildrenCount));
                ((ILayoutElementWithVisibility)this).ComputeVisibility();
            }
        }

        private void _rootPanel_ChildrenTreeChanged(object sender, ChildrenTreeChangedEventArgs e)
        {
            RaisePropertyChanged(nameof(this.IsSinglePane));
            RaisePropertyChanged(nameof(this.SinglePane));
        }

        #endregion RootPanel

        #region IsSinglePane

        public bool IsSinglePane => this.RootPanel?.Descendents().OfType<LayoutDocumentPane>().Count(p => p.IsVisible) == 1;

        public LayoutDocumentPane SinglePane
        {
            get
            {
                if (!this.IsSinglePane) return null;
                LayoutDocumentPane singlePane = this.RootPanel.Descendents().OfType<LayoutDocumentPane>().Single(p => p.IsVisible);
                //singlePane.UpdateIsDirectlyHostedInFloatingWindow();
                return singlePane;
            }
        }

        #endregion IsSinglePane

        [XmlIgnore]
        public bool IsVisible
        {
            get => _isVisible;
            private set
            {
                if (_isVisible == value) return;
                RaisePropertyChanging(nameof(this.IsVisible));
                _isVisible = value;
                RaisePropertyChanged(nameof(this.IsVisible));
                IsVisibleChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        #endregion Properties

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
            this.RootPanel = newElement as LayoutDocumentPaneGroup;
        }

        /// <inheritdoc />
        public override int ChildrenCount => this.RootPanel == null ? 0 : 1;

        void ILayoutElementWithVisibility.ComputeVisibility() => this.IsVisible = this.RootPanel != null && this.RootPanel.IsVisible;

        /// <inheritdoc />
        public override bool IsValid => this.RootPanel != null;

        /// <inheritdoc />
        public override void ReadXml(XmlReader reader)
        {
            reader.MoveToContent();
            if (reader.IsEmptyElement)
            {
                reader.Read();
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
                if (reader.LocalName.Equals(nameof(LayoutDocument)))
                    serializer = XmlSerializersCache.GetSerializer<LayoutDocument>();
                else
                {
                    Type type = LayoutRoot.FindType(reader.LocalName);
                    if (type == null) throw new ArgumentException("H.Controls.Dock.LayoutDocumentFloatingWindow doesn't know how to deserialize " + reader.LocalName);
                    serializer = XmlSerializersCache.GetSerializer(type);
                }
                this.RootPanel = (LayoutDocumentPaneGroup)serializer.Deserialize(reader);
            }

            reader.ReadEndElement();
        }

#if TRACE
        public override void ConsoleDump(int tab)
        {
            System.Diagnostics.Trace.Write(new string(' ', tab * 4));
            System.Diagnostics.Trace.WriteLine("FloatingDocumentWindow()");

            this.RootPanel.ConsoleDump(tab + 1);
        }
#endif

        #endregion Overrides
    }
}