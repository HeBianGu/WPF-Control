// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


using System.Collections.ObjectModel;
using System.ComponentModel;
#if NET
#endif 
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;

namespace H.Extensions.Behvaiors.Adorners;

[ContentProperty("Parameters")]
[DefaultProperty("Parameters")]
public class MouseOverAdornerBehavior : AdornerBehaviorBase
{
    private Adorner _adorner;
    public ObservableCollection<object> Parameters { get; } = new ObservableCollection<object>();

    protected override void OnAttached()
    {
        this.AssociatedObject.MouseEnter += AssociatedObject_MouseEnter;
        this.AssociatedObject.MouseLeave += AssociatedObject_MouseLeave;
    }

    private void AssociatedObject_MouseLeave(object sender, MouseEventArgs e)
    {
        System.Diagnostics.Debug.WriteLine("AssociatedObject_MouseLeave");
        ClearAdorner();
    }

    private void AssociatedObject_MouseEnter(object sender, MouseEventArgs e)
    {
        System.Diagnostics.Debug.WriteLine("AssociatedObject_MouseEnter");

        if (this.AdornerType == null)
            return;

        if (this.AdornerVisual == null)
            this.AdornerVisual = this.AssociatedObject;

        if (_adorner != null)
            return;

        AdornerLayer layer = AdornerLayer.GetAdornerLayer(this.AssociatedObject);
        if (layer == null)
            return;

        IEnumerable<Adorner> adorners = layer.GetAdorners(this.AssociatedObject)?.Where(l => l.GetType() == this.AdornerType);
        if (adorners != null)
        {
            foreach (Adorner item in adorners)
            {
                layer.Remove(item);
            }
        }
        if (this.IsUse)
        {
            Adorner adorner = Activator.CreateInstance(this.AdornerType, this.AdornerVisual) as Adorner;
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
        this.AssociatedObject.MouseEnter -= AssociatedObject_MouseEnter;
        this.AssociatedObject.MouseLeave -= AssociatedObject_MouseLeave;
        ClearAdorner();
    }

    private void ClearAdorner()
    {
        if (_adorner == null)
            return;
        AdornerLayer layer = AdornerLayer.GetAdornerLayer(this.AdornerVisual);
        if (layer == null)
            return;
        _adorner.MouseLeave -= AssociatedObject_MouseLeave;
        layer.Remove(_adorner);
        _adorner = null;
    }
}



