// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using Microsoft.Xaml.Behaviors;
using System.Collections;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;

namespace H.Extensions.Behvaiors.TextBlocks;

public class TextBlockIndexOfBebavior : Behavior<TextBlock>
{
    protected override void OnAttached()
    {
        base.OnAttached();
        this.AssociatedObject.Loaded += this.AssociatedObject_Loaded;
    }

    private void AssociatedObject_Loaded(object sender, RoutedEventArgs e)
    {
        this.RefreshData();
    }

    protected override void OnDetaching()
    {
        base.OnDetaching();
        this.AssociatedObject.Loaded -= this.AssociatedObject_Loaded;
    }

    public IEnumerable Source
    {
        get { return (IEnumerable)GetValue(SourceProperty); }
        set { SetValue(SourceProperty, value); }
    }

    public static readonly DependencyProperty SourceProperty =
        DependencyProperty.Register("Source", typeof(IEnumerable), typeof(TextBlockIndexOfBebavior), new FrameworkPropertyMetadata(default(IEnumerable), (d, e) =>
        {
            TextBlockIndexOfBebavior control = d as TextBlockIndexOfBebavior;

            if (control == null) return;

            if (e.OldValue is INotifyCollectionChanged o)
                o.CollectionChanged -= control.N_CollectionChanged;

            if (e.NewValue is INotifyCollectionChanged n)
            {
                n.CollectionChanged -= control.N_CollectionChanged;
                n.CollectionChanged += control.N_CollectionChanged;
            }
            control.RefreshData();
        }));

    private void N_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        this.RefreshData();
    }

    public object Item
    {
        get { return GetValue(ItemProperty); }
        set { SetValue(ItemProperty, value); }
    }

    public static readonly DependencyProperty ItemProperty =
        DependencyProperty.Register("Item", typeof(object), typeof(TextBlockIndexOfBebavior), new FrameworkPropertyMetadata(default(object)));

    protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);
        this.RefreshData();
    }

    public int DefaultFromValue
    {
        get { return (int)GetValue(DefaultFromValueProperty); }
        set { SetValue(DefaultFromValueProperty, value); }
    }

    public static readonly DependencyProperty DefaultFromValueProperty =
        DependencyProperty.Register("DefaultFromValue", typeof(int), typeof(TextBlockIndexOfBebavior), new FrameworkPropertyMetadata(1));


    public string Format
    {
        get { return (string)GetValue(FormatProperty); }
        set { SetValue(FormatProperty, value); }
    }

    public static readonly DependencyProperty FormatProperty =
        DependencyProperty.Register("Format", typeof(string), typeof(TextBlockIndexOfBebavior), new FrameworkPropertyMetadata("[{0}/{1}]"));

    protected virtual void RefreshData()
    {
        int total = this.Source?.Cast<object>().Count() ?? 0;
        int index = this.Source?.Cast<object>().ToList().IndexOf(this.Item) ?? 0;
        if (this.AssociatedObject == null)
            return;
        this.AssociatedObject.Text = string.Format(this.Format, index + this.DefaultFromValue, total);
    }
}
