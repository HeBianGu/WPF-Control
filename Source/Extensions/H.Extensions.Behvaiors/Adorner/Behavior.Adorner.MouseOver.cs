// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
#if NET
#endif 
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;

namespace H.Extensions.Behvaiors
{
    [ContentProperty("Parameters")]
    [DefaultProperty("Parameters")]
    public class MouseOverAdornerBehavior : AdornerBehaviorBase
    {
        private Adorner _adorner;
        public ObservableCollection<object> Parameters { get; } = new ObservableCollection<object>();

        protected override void OnAttached()
        {
            AssociatedObject.MouseEnter += AssociatedObject_MouseEnter;
            AssociatedObject.MouseLeave += AssociatedObject_MouseLeave;
        }

        private void AssociatedObject_MouseLeave(object sender, MouseEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("AssociatedObject_MouseLeave");
            ClearAdorner();
        }

        private void AssociatedObject_MouseEnter(object sender, MouseEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("AssociatedObject_MouseEnter");

            if (AdornerType == null)
                return;

            if (AdornerVisual == null)
                AdornerVisual = AssociatedObject;

            if (_adorner != null)
                return;

            AdornerLayer layer = AdornerLayer.GetAdornerLayer(AssociatedObject);
            if (layer == null)
                return;

            IEnumerable<Adorner> adorners = layer.GetAdorners(AssociatedObject)?.Where(l => l.GetType() == AdornerType);
            if (adorners != null)
            {
                foreach (Adorner item in adorners)
                {
                    layer.Remove(item);
                }
            }
            if (IsUse)
            {
                Adorner adorner = Activator.CreateInstance(AdornerType, AdornerVisual) as Adorner;
                if (adorner == null)
                    return;
                adorner.MouseLeave -= AssociatedObject_MouseLeave;
                adorner.MouseLeave += AssociatedObject_MouseLeave;
                _adorner = adorner;
                adorner.IsHitTestVisible = false;
                layer.Add(adorner);
            }
        }

        protected override void OnDetaching()
        {
            AssociatedObject.MouseEnter -= AssociatedObject_MouseEnter;
            AssociatedObject.MouseLeave -= AssociatedObject_MouseLeave;
            ClearAdorner();
        }

        private void ClearAdorner()
        {
            if (_adorner == null)
                return;
            AdornerLayer layer = AdornerLayer.GetAdornerLayer(AdornerVisual);
            if (layer == null)
                return;
            _adorner.MouseLeave -= AssociatedObject_MouseLeave;
            layer.Remove(_adorner);
            _adorner = null;
        }
    }
}



