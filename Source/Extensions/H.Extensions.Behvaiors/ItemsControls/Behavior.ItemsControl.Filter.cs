// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using H.Common.Interfaces.Where;
using Microsoft.Xaml.Behaviors;
using System.Collections;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;

namespace H.Extensions.Behvaiors.ItemsControls;

public class ItemsControlFilterBehavior : Behavior<ItemsControl>
{
    public IEnumerable ItemsSource
    {
        get { return (IEnumerable)GetValue(ItemsSourceProperty); }
        set { SetValue(ItemsSourceProperty, value); }
    }

    public static readonly DependencyProperty ItemsSourceProperty =
        DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(ItemsControlFilterBehavior), new FrameworkPropertyMetadata(default(IEnumerable), (d, e) =>
        {
            ItemsControlFilterBehavior control = d as ItemsControlFilterBehavior;
            if (control == null)
                return;
            if (e.OldValue is INotifyCollectionChanged o)
                o.CollectionChanged -= control.CollectionChanged;
            if (e.NewValue is INotifyCollectionChanged n)
                n.CollectionChanged += control.CollectionChanged;
            control.RefreshData();
        }));

    private void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        RefreshData();
    }

    public IFilterable Filter
    {
        get { return (IFilterable)GetValue(FilterProperty); }
        set { SetValue(FilterProperty, value); }
    }

    public static readonly DependencyProperty FilterProperty =
        DependencyProperty.Register("Filter", typeof(IFilterable), typeof(ItemsControlFilterBehavior), new FrameworkPropertyMetadata(default(IFilterable), (d, e) =>
        {
            ItemsControlFilterBehavior control = d as ItemsControlFilterBehavior;

            if (control == null) return;

            if (e.OldValue is IFilterable o)
            {

            }

            if (e.NewValue is IFilterable n)
            {

            }
            control.RefreshData();
        }));


    public IFilterable Filter1
    {
        get { return (IFilterable)GetValue(Filter1Property); }
        set { SetValue(Filter1Property, value); }
    }


    public static readonly DependencyProperty Filter1Property =
        DependencyProperty.Register("Filter1", typeof(IFilterable), typeof(ItemsControlFilterBehavior), new FrameworkPropertyMetadata(default(IFilterable), (d, e) =>
        {
            ItemsControlFilterBehavior control = d as ItemsControlFilterBehavior;

            if (control == null) return;

            if (e.OldValue is IFilterable o)
            {

            }

            if (e.NewValue is IFilterable n)
            {

            }
            control.RefreshData();
        }));


    public IFilterable Filter2
    {
        get { return (IFilterable)GetValue(Filter2Property); }
        set { SetValue(Filter2Property, value); }
    }


    public static readonly DependencyProperty Filter2Property =
        DependencyProperty.Register("Filter2", typeof(IFilterable), typeof(ItemsControlFilterBehavior), new FrameworkPropertyMetadata(default(IFilterable), (d, e) =>
        {
            ItemsControlFilterBehavior control = d as ItemsControlFilterBehavior;

            if (control == null) return;

            if (e.OldValue is IFilterable o)
            {

            }

            if (e.NewValue is IFilterable n)
            {

            }
            control.RefreshData();
        }));


    public IFilterable Filter3
    {
        get { return (IFilterable)GetValue(Filter3Property); }
        set { SetValue(Filter3Property, value); }
    }


    public static readonly DependencyProperty Filter3Property =
        DependencyProperty.Register("Filter3", typeof(IFilterable), typeof(ItemsControlFilterBehavior), new FrameworkPropertyMetadata(default(IFilterable), (d, e) =>
        {
            ItemsControlFilterBehavior control = d as ItemsControlFilterBehavior;

            if (control == null) return;

            if (e.OldValue is IFilterable o)
            {

            }

            if (e.NewValue is IFilterable n)
            {

            }
            control.RefreshData();
        }));


    public IDisplayFilter Filter4
    {
        get { return (IDisplayFilter)GetValue(Filter4Property); }
        set { SetValue(Filter4Property, value); }
    }


    public static readonly DependencyProperty Filter4Property =
        DependencyProperty.Register("Filter4", typeof(IDisplayFilter), typeof(ItemsControlFilterBehavior), new FrameworkPropertyMetadata(default(IDisplayFilter), (d, e) =>
        {
            ItemsControlFilterBehavior control = d as ItemsControlFilterBehavior;

            if (control == null) return;

            if (e.OldValue is IFilterable o)
            {

            }

            if (e.NewValue is IFilterable n)
            {

            }
            control.RefreshData();
        }));


    public IFilterable Filter5
    {
        get { return (IFilterable)GetValue(Filter5Property); }
        set { SetValue(Filter5Property, value); }
    }


    public static readonly DependencyProperty Filter5Property =
        DependencyProperty.Register("Filter5", typeof(IFilterable), typeof(ItemsControlFilterBehavior), new FrameworkPropertyMetadata(default(IFilterable), (d, e) =>
        {
            ItemsControlFilterBehavior control = d as ItemsControlFilterBehavior;

            if (control == null) return;

            if (e.OldValue is IFilterable o)
            {

            }

            if (e.NewValue is IFilterable n)
            {

            }
            control.RefreshData();
        }));


    public IFilterable Filter6
    {
        get { return (IFilterable)GetValue(Filter6Property); }
        set { SetValue(Filter6Property, value); }
    }


    public static readonly DependencyProperty Filter6Property =
        DependencyProperty.Register("Filter6", typeof(IFilterable), typeof(ItemsControlFilterBehavior), new FrameworkPropertyMetadata(default(IFilterable), (d, e) =>
        {
            ItemsControlFilterBehavior control = d as ItemsControlFilterBehavior;

            if (control == null) return;

            if (e.OldValue is IFilterable o)
            {

            }

            if (e.NewValue is IFilterable n)
            {

            }
            control.RefreshData();
        }));


    public IFilterable Filter7
    {
        get { return (IFilterable)GetValue(Filter7Property); }
        set { SetValue(Filter7Property, value); }
    }


    public static readonly DependencyProperty Filter7Property =
        DependencyProperty.Register("Filter7", typeof(IFilterable), typeof(ItemsControlFilterBehavior), new FrameworkPropertyMetadata(default(IFilterable), (d, e) =>
        {
            ItemsControlFilterBehavior control = d as ItemsControlFilterBehavior;

            if (control == null) return;

            if (e.OldValue is IFilterable o)
            {

            }

            if (e.NewValue is IFilterable n)
            {

            }
            control.RefreshData();
        }));


    public IFilterable Filter8
    {
        get { return (IFilterable)GetValue(Filter8Property); }
        set { SetValue(Filter8Property, value); }
    }


    public static readonly DependencyProperty Filter8Property =
        DependencyProperty.Register("Filter8", typeof(IFilterable), typeof(ItemsControlFilterBehavior), new FrameworkPropertyMetadata(default(IFilterable), (d, e) =>
        {
            ItemsControlFilterBehavior control = d as ItemsControlFilterBehavior;

            if (control == null) return;

            if (e.OldValue is IFilterable o)
            {

            }

            if (e.NewValue is IFilterable n)
            {

            }
            control.RefreshData();
        }));


    public IFilterable Filter9
    {
        get { return (IFilterable)GetValue(Filter9Property); }
        set { SetValue(Filter9Property, value); }
    }


    public static readonly DependencyProperty Filter9Property =
        DependencyProperty.Register("Filter9", typeof(IFilterable), typeof(ItemsControlFilterBehavior), new FrameworkPropertyMetadata(default(IFilterable), (d, e) =>
        {
            ItemsControlFilterBehavior control = d as ItemsControlFilterBehavior;

            if (control == null) return;

            if (e.OldValue is IFilterable o)
            {

            }

            if (e.NewValue is IFilterable n)
            {

            }
            control.RefreshData();
        }));



    public IOrderWhereable Order
    {
        get { return (IOrderWhereable)GetValue(OrderProperty); }
        set { SetValue(OrderProperty, value); }
    }


    public static readonly DependencyProperty OrderProperty =
        DependencyProperty.Register("Order", typeof(IOrderWhereable), typeof(ItemsControlFilterBehavior), new FrameworkPropertyMetadata(default(IOrderWhereable), (d, e) =>
        {
            ItemsControlFilterBehavior control = d as ItemsControlFilterBehavior;

            if (control == null) return;

            if (e.OldValue is IOrderWhereable o)
            {

            }

            if (e.NewValue is IOrderWhereable n)
            {

            }
            control.RefreshData();
        }));


    public IOrderWhereable Order2
    {
        get { return (IOrderWhereable)GetValue(Order2Property); }
        set { SetValue(Order2Property, value); }
    }


    public static readonly DependencyProperty Order2Property =
        DependencyProperty.Register("Order2", typeof(IOrderWhereable), typeof(ItemsControlFilterBehavior), new FrameworkPropertyMetadata(default(IOrderWhereable), (d, e) =>
        {
            ItemsControlFilterBehavior control = d as ItemsControlFilterBehavior;

            if (control == null) return;

            if (e.OldValue is IOrderWhereable o)
            {

            }

            if (e.NewValue is IOrderWhereable n)
            {

            }
            control.RefreshData();
        }));


    public IOrderWhereable Order1
    {
        get { return (IOrderWhereable)GetValue(Order1Property); }
        set { SetValue(Order1Property, value); }
    }


    public static readonly DependencyProperty Order1Property =
        DependencyProperty.Register("Order1", typeof(IOrderWhereable), typeof(ItemsControlFilterBehavior), new FrameworkPropertyMetadata(default(IOrderWhereable), (d, e) =>
        {
            ItemsControlFilterBehavior control = d as ItemsControlFilterBehavior;

            if (control == null) return;

            if (e.OldValue is IOrderWhereable o)
            {

            }

            if (e.NewValue is IOrderWhereable n)
            {

            }
            control.RefreshData();
        }));


    public IOrderWhereable Order3
    {
        get { return (IOrderWhereable)GetValue(Order3Property); }
        set { SetValue(Order3Property, value); }
    }


    public static readonly DependencyProperty Order3Property =
        DependencyProperty.Register("Order3", typeof(IOrderWhereable), typeof(ItemsControlFilterBehavior), new FrameworkPropertyMetadata(default(IOrderWhereable), (d, e) =>
        {
            ItemsControlFilterBehavior control = d as ItemsControlFilterBehavior;

            if (control == null) return;

            if (e.OldValue is IOrderWhereable o)
            {

            }

            if (e.NewValue is IOrderWhereable n)
            {

            }
            control.RefreshData();
        }));


    public IOrderWhereable Order4
    {
        get { return (IOrderWhereable)GetValue(Order4Property); }
        set { SetValue(Order4Property, value); }
    }


    public static readonly DependencyProperty Order4Property =
        DependencyProperty.Register("Order4", typeof(IOrderWhereable), typeof(ItemsControlFilterBehavior), new FrameworkPropertyMetadata(default(IOrderWhereable), (d, e) =>
        {
            ItemsControlFilterBehavior control = d as ItemsControlFilterBehavior;

            if (control == null) return;

            if (e.OldValue is IOrderWhereable o)
            {

            }

            if (e.NewValue is IOrderWhereable n)
            {

            }
            control.RefreshData();
        }));


    public IOrderWhereable Order5
    {
        get { return (IOrderWhereable)GetValue(Order5Property); }
        set { SetValue(Order5Property, value); }
    }


    public static readonly DependencyProperty Order5Property =
        DependencyProperty.Register("Order5", typeof(IOrderWhereable), typeof(ItemsControlFilterBehavior), new FrameworkPropertyMetadata(default(IOrderWhereable), (d, e) =>
        {
            ItemsControlFilterBehavior control = d as ItemsControlFilterBehavior;

            if (control == null) return;

            if (e.OldValue is IOrderWhereable o)
            {

            }

            if (e.NewValue is IOrderWhereable n)
            {

            }
            control.RefreshData();
        }));



    private IEnumerable<IFilterable> GetFilters()
    {
        yield return this.Filter;
        yield return this.Filter1;
        yield return this.Filter2;
        yield return this.Filter3;
        yield return this.Filter4;
        yield return this.Filter5;
        yield return this.Filter6;
        yield return this.Filter7;
        yield return this.Filter8;
        yield return this.Filter9;
    }
    private IEnumerable<IOrderWhereable> GetOrders()
    {
        yield return this.Order;
        yield return this.Order1;
        yield return this.Order2;
        yield return this.Order3;
        yield return this.Order4;
        yield return this.Order5;
    }

    private void RefreshData()
    {
        if (this.ItemsSource == null)
            return;
        if (this.AssociatedObject == null)
            return;
        IEnumerable<IFilterable> filters = GetFilters().Where(x => x != null);
        IEnumerable source = filters.Count() == 0 ? this.ItemsSource
            : this.ItemsSource.OfType<object>().Where(x => filters.All(l => l.IsMatch(x))).ToList();
        List<IOrderWhereable> orders = GetOrders().ToList();
        foreach (IOrderWhereable item in orders)
        {
            if (item == null)
                continue;
            source = item.Where(source);
        }
        this.AssociatedObject.ItemsSource = source;
    }

    protected override void OnAttached()
    {
        this.AssociatedObject.Loaded += AssociatedObject_Loaded;
    }

    private void AssociatedObject_Loaded(object sender, RoutedEventArgs e)
    {
        RefreshData();
    }

    protected override void OnDetaching()
    {
        this.AssociatedObject.Loaded -= AssociatedObject_Loaded;
    }
}