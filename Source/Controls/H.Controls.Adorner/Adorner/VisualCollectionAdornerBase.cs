// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System.Windows;
using System.Windows.Media;

namespace H.Controls.Adorner
{
    public abstract class VisualCollectionAdornerBase : AdornerBase
    {
        protected VisualCollection _visualCollection;

        public VisualCollectionAdornerBase(UIElement adornedElement) : base(adornedElement)
        {
            _visualCollection = new VisualCollection(this);
        }

        protected override Visual GetVisualChild(int index)
        {
            return _visualCollection[index];
        }
        protected override int VisualChildrenCount
        {
            get
            {
                return _visualCollection.Count;
            }
        }
    }
}
