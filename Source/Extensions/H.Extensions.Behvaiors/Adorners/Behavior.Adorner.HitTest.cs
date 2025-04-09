// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


#if NET
#endif 
using System.Windows;
using System.Windows.Documents;

namespace H.Extensions.Behvaiors.Adorners;

public abstract class HitTestAdornerBehavior : AdornerBehaviorBase
{
    public static Type GetHitTestAdornerType(DependencyObject obj)
    {
        return (Type)obj.GetValue(HitTestAdornerTypeProperty);
    }

    public static void SetHitTestAdornerType(DependencyObject obj, Type value)
    {
        obj.SetValue(HitTestAdornerTypeProperty, value);
    }


    public static readonly DependencyProperty HitTestAdornerTypeProperty =
        DependencyProperty.RegisterAttached("HitTestAdornerType", typeof(Type), typeof(HitTestAdornerBehavior), new PropertyMetadata(default(Type), OnHitTestAdornerTypeChanged));

    public static void OnHitTestAdornerTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        DependencyObject control = d;

        Type n = (Type)e.NewValue;

        Type o = (Type)e.OldValue;
    }


    protected UIElement _temp = null;
    //protected UIElement _visualHit = null;

    public static bool GetIsHitTest(DependencyObject obj)
    {
        return (bool)obj.GetValue(IsHitTestProperty);
    }

    public static void SetIsHitTest(DependencyObject obj, bool value)
    {
        obj.SetValue(IsHitTestProperty, value);
    }


    public static readonly DependencyProperty IsHitTestProperty =
        DependencyProperty.RegisterAttached("IsHitTest", typeof(bool), typeof(HitTestAdornerBehavior), new PropertyMetadata(default(bool), OnIsHitTestChanged));

    public static void OnIsHitTestChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        DependencyObject control = d;

        bool n = (bool)e.NewValue;

        bool o = (bool)e.OldValue;
    }

    protected virtual void Clear()
    {
        _temp?.ClearAdorner(x => x.GetType() == this.AdornerType);
        if (_temp != null)
            MouseOverHitTestAdornerBehavior.SetIsMouseOver(_temp, false);
        if (_temp.GetDataContext() is IGetDropAdorner drop)
            drop.RemoveDropAdorner(_temp);
    }

    //HitTestResultBehavior HitTestCallBack(HitTestResult result)
    //{
    //    if (HitTestAdornerBehavior.GetIsHitTest(result.VisualHit))
    //    {
    //        return HitTestResultBehavior.Stop;
    //    }
    //    return HitTestResultBehavior.Continue;
    //}

    //HitTestFilterBehavior HitTestFilter(DependencyObject obj)
    //{
    //    Type type = obj.GetType();
    //    if (type.Name == this.GetType().Name)
    //        return HitTestFilterBehavior.ContinueSkipSelf;
    //    if (HitTestAdornerBehavior.GetIsHitTest(obj))
    //    {
    //        _visualHit = obj as UIElement;
    //    }

    //    return HitTestFilterBehavior.Continue;
    //}


    protected virtual void AddAdorner(UIElement elment)
    {
        if (_temp == elment)
            return;
        if (this.AdornerType == null)
            return;

        if (CheckAdorner(elment) == false)
            return;

        if (this.IsUse)
        {
            Adorner adorner = GetAdorner(elment);
            if (adorner == null)
                return;
            adorner.IsHitTestVisible = this.IsHitTestVisible;

            System.Diagnostics.Debug.WriteLine("IsHitTestVisible:" + adorner.IsHitTestVisible);

            elment.AddAdorner(adorner);
        }
    }

    protected virtual bool CheckAdorner(UIElement elment)
    {
        Type custom = GetHitTestAdornerType(elment);
        if (custom != null)
            return elment.GetAdorner(x => x.GetType() == custom) == null;
        return elment.GetAdorner(x => x.GetType() == this.AdornerType) == null;
    }

    protected virtual Adorner GetAdorner(UIElement elment)
    {
        Type custom = GetHitTestAdornerType(elment);
        if (custom != null)
            return Activator.CreateInstance(custom, elment) as Adorner;
        return Activator.CreateInstance(this.AdornerType, elment) as Adorner;
    }

    protected override void OnDetaching()
    {
        Clear();
    }
}



