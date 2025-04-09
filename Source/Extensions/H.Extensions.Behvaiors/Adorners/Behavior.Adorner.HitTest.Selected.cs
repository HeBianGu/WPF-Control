// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


using System.Collections.ObjectModel;
#if NET
#endif
using System.Windows;
using System.Windows.Input;

namespace H.Extensions.Behvaiors.Adorners;

public class SelectedHitTestAdornerBehavior : HitTestAdornerBehavior
{
    public ObservableCollection<object> Parameters { get; } = new ObservableCollection<object>();

    public static bool GetIsSelected(DependencyObject obj)
    {
        return (bool)obj.GetValue(IsSelectedProperty);
    }

    public static void SetIsSelected(DependencyObject obj, bool value)
    {
        obj.SetValue(IsSelectedProperty, value);
    }


    public static readonly DependencyProperty IsSelectedProperty =
        DependencyProperty.RegisterAttached("IsSelected", typeof(bool), typeof(SelectedHitTestAdornerBehavior), new PropertyMetadata(default(bool), OnIsSelectedChanged));

    static public void OnIsSelectedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        DependencyObject control = d as DependencyObject;

        bool n = (bool)e.NewValue;

        bool o = (bool)e.OldValue;
    }



    protected override void OnAttached()
    {
        this.AssociatedObject.AddHandler(UIElement.MouseLeftButtonDownEvent, new MouseButtonEventHandler(AssociatedObject_MouseLeftButtonDown), true);
    }
    protected override void OnDetaching()
    {
        this.AssociatedObject.RemoveHandler(UIElement.MouseLeftButtonDownEvent, new MouseButtonEventHandler(AssociatedObject_MouseLeftButtonDown));
        this.Clear();
    }

    private void AssociatedObject_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (this.AdornerType == null)
            return;
        if (this.AdornerVisual == null)
            this.AdornerVisual = this.AssociatedObject;
        Point point = e.GetPosition(this.AssociatedObject);
        var visualHit = this.AssociatedObject.HitTest<UIElement>(point, x => SelectedHitTestAdornerBehavior.GetIsHitTest(x));
        if (visualHit == null)
        {
            this.Clear();
            if (_temp != null)
                SetIsSelected(_temp, false);
        }
        else
        {
            if (visualHit != _temp)
                this.Clear();
            this.AddAdorner(visualHit);
            SelectedHitTestAdornerBehavior.SetIsSelected(visualHit, true);
            //Cattach.SetIsSelected(visualHit, true);
        }
        _temp = visualHit;
    }
}



