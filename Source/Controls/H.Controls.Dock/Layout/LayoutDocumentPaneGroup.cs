








using System;
using System.Windows.Controls;
using System.Windows.Markup;

namespace H.Controls.Dock.Layout
{
    /// <summary>
    /// Implements an element in the layout model that can contain and organize multiple
    /// <see cref="LayoutDocumentPane"/> elements, which in turn contain <see cref="LayoutDocument"/> elements.
    /// </summary>
    [ContentProperty(nameof(Children))]
    [Serializable]
    public class LayoutDocumentPaneGroup : LayoutPositionableGroup<ILayoutDocumentPane>, ILayoutDocumentPane, ILayoutOrientableGroup
    {
        #region fields

        private Orientation _orientation;

        #endregion fields

        #region Constructors

        /// <summary>Class constructor</summary>
        public LayoutDocumentPaneGroup()
        {
        }

        /// <summary>Class constructor from <paramref name="documentPane"/> that is added into the children collection of this object.</summary>
        public LayoutDocumentPaneGroup(LayoutDocumentPane documentPane)
        {
            this.Children.Add(documentPane);
        }

        #endregion Constructors

        #region Properties

        /// <summary>Gets/sets the (Horizontal, Vertical) <see cref="System.Windows.Controls.Orientation"/> of this group.</summary>
        public Orientation Orientation
        {
            get => _orientation;
            set
            {
                if (value == _orientation) return;
                RaisePropertyChanging(nameof(this.Orientation));
                _orientation = value;
                RaisePropertyChanged(nameof(this.Orientation));
            }
        }

        #endregion Properties

        #region Overrides

        /// <inheritdoc />
        protected override bool GetVisibility() => true;

        /// <inheritdoc />
        public override void WriteXml(System.Xml.XmlWriter writer)
        {
            writer.WriteAttributeString(nameof(this.Orientation), this.Orientation.ToString());
            base.WriteXml(writer);
        }

        /// <inheritdoc />
        public override void ReadXml(System.Xml.XmlReader reader)
        {
            if (reader.MoveToAttribute(nameof(this.Orientation))) this.Orientation = (Orientation)Enum.Parse(typeof(Orientation), reader.Value, true);
            base.ReadXml(reader);
        }

#if TRACE
        /// <inheritdoc />
        public override void ConsoleDump(int tab)
        {
            System.Diagnostics.Trace.Write(new string(' ', tab * 4));
            System.Diagnostics.Trace.WriteLine(string.Format("DocumentPaneGroup({0})", this.Orientation));

            foreach (LayoutElement child in this.Children)
                child.ConsoleDump(tab + 1);
        }
#endif

        #endregion Overrides
    }
}