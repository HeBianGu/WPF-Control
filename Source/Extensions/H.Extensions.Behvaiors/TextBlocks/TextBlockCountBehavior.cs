// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using Microsoft.Xaml.Behaviors;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace H.Extensions.Behvaiors.TextBlocks;

/// <summary>
/// 统计数据源中，指定属性值的数量
/// </summary>
public class TextBlockCountBehavior : Behavior<TextBlock>
{
    public string PropertyName
    {
        get { return (string)GetValue(PropertyNameProperty); }
        set { SetValue(PropertyNameProperty, value); }
    }


    public static readonly DependencyProperty PropertyNameProperty =
        DependencyProperty.Register("PropertyName", typeof(string), typeof(TextBlockCountBehavior), new FrameworkPropertyMetadata(default(string), (d, e) =>
        {
            TextBlockCountBehavior control = d as TextBlockCountBehavior;

            if (control == null) return;

            if (e.OldValue is string o)
            {

            }

            if (e.NewValue is string n)
            {

            }
            control.RefreshData();
        }));


    public object Value
    {
        get { return GetValue(ValueProperty); }
        set { SetValue(ValueProperty, value); }
    }


    public static readonly DependencyProperty ValueProperty =
        DependencyProperty.Register("Value", typeof(object), typeof(TextBlockCountBehavior), new FrameworkPropertyMetadata(default, (d, e) =>
        {
            TextBlockCountBehavior control = d as TextBlockCountBehavior;

            if (control == null) return;

            if (e.OldValue is object o)
            {

            }

            if (e.NewValue is object n)
            {

            }
            control.RefreshData();
        }));

    public Type Type
    {
        get { return (Type)GetValue(TypeProperty); }
        set { SetValue(TypeProperty, value); }
    }


    public static readonly DependencyProperty TypeProperty =
        DependencyProperty.Register("Type", typeof(Type), typeof(TextBlockCountBehavior), new FrameworkPropertyMetadata(default(Type), (d, e) =>
        {
            TextBlockCountBehavior control = d as TextBlockCountBehavior;

            if (control == null) return;

            if (e.OldValue is Type o)
            {

            }

            if (e.NewValue is Type n)
            {

            }

        }));

    public IEnumerable ItemsSource
    {
        get { return (IEnumerable)GetValue(ItemsSourceProperty); }
        set { SetValue(ItemsSourceProperty, value); }
    }


    public static readonly DependencyProperty ItemsSourceProperty =
        DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(TextBlockCountBehavior), new FrameworkPropertyMetadata(default(IEnumerable), (d, e) =>
        {
            TextBlockCountBehavior control = d as TextBlockCountBehavior;
            if (control == null)
                return;
            if (e.OldValue is INotifyCollectionChanged o)
                o.CollectionChanged -= control.CollectionChanged;
            if (e.NewValue is INotifyCollectionChanged n)
                n.CollectionChanged += control.CollectionChanged;
            control.RefreshData();
        }));


    public string Header
    {
        get { return (string)GetValue(HeaderProperty); }
        set { SetValue(HeaderProperty, value); }
    }


    public static readonly DependencyProperty HeaderProperty =
        DependencyProperty.Register("Header", typeof(string), typeof(TextBlockCountBehavior), new FrameworkPropertyMetadata("合计：", (d, e) =>
        {
            TextBlockCountBehavior control = d as TextBlockCountBehavior;

            if (control == null) return;

            if (e.OldValue is string o)
            {

            }

            if (e.NewValue is string n)
            {

            }

        }));


    public string Format
    {
        get { return (string)GetValue(FormatProperty); }
        set { SetValue(FormatProperty, value); }
    }


    public static readonly DependencyProperty FormatProperty =
        DependencyProperty.Register("Format", typeof(string), typeof(TextBlockCountBehavior), new FrameworkPropertyMetadata("[{0}] {1}={2}", (d, e) =>
        {
            TextBlockCountBehavior control = d as TextBlockCountBehavior;

            if (control == null) return;

            if (e.OldValue is string o)
            {

            }

            if (e.NewValue is string n)
            {

            }

        }));


    private void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        this.RefreshData();
    }

    private void RefreshData()
    {
        if (this.ItemsSource == null)
            return;
        if (this.AssociatedObject == null)
            return;
        Type cType = this.ItemsSource.GetType();
        if (!cType.IsGenericType && this.Type == null)
            return;

        Type argType = cType.GenericTypeArguments.FirstOrDefault();
        Type type = this.Type ?? argType;
        if (type == null)
            return;
        if (string.IsNullOrWhiteSpace(this.PropertyName))
            return;

        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append(this.Header);
        PropertyInfo p = type.GetProperty(this.PropertyName);
        if (p == null)
            return;
        int count = this.ItemsSource.OfType<object>().Count(x => p.GetValue(x)?.Equals(this.Value) == true);
        DisplayAttribute display = p.GetCustomAttribute<DisplayAttribute>();
        stringBuilder.Append(string.Format(this.Format, count, display?.Name ?? p.Name, this.Value));
        this.AssociatedObject.Text = stringBuilder.ToString();
    }

    protected override void OnAttached()
    {
        this.AssociatedObject.Loaded += AssociatedObject_Loaded;
    }

    private void AssociatedObject_Loaded(object sender, RoutedEventArgs e)
    {
        this.RefreshData();
    }

    protected override void OnDetaching()
    {
        this.AssociatedObject.Loaded -= AssociatedObject_Loaded;
    }
}