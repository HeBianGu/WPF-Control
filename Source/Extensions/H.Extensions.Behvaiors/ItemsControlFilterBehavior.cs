// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using H.Providers.Ioc;
using Microsoft.Xaml.Behaviors;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace H.Extensions.Behvaiors
{
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
            this.RefreshData();
        }

        public IFilter Filter
        {
            get { return (IFilter)GetValue(FilterProperty); }
            set { SetValue(FilterProperty, value); }
        }

        public static readonly DependencyProperty FilterProperty =
            DependencyProperty.Register("Filter", typeof(IFilter), typeof(ItemsControlFilterBehavior), new FrameworkPropertyMetadata(default(IFilter), (d, e) =>
            {
                ItemsControlFilterBehavior control = d as ItemsControlFilterBehavior;

                if (control == null) return;

                if (e.OldValue is IFilter o)
                {

                }

                if (e.NewValue is IFilter n)
                {

                }
                control.RefreshData();
            }));


        public IFilter Filter1
        {
            get { return (IFilter)GetValue(Filter1Property); }
            set { SetValue(Filter1Property, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Filter1Property =
            DependencyProperty.Register("Filter1", typeof(IFilter), typeof(ItemsControlFilterBehavior), new FrameworkPropertyMetadata(default(IFilter), (d, e) =>
            {
                ItemsControlFilterBehavior control = d as ItemsControlFilterBehavior;

                if (control == null) return;

                if (e.OldValue is IFilter o)
                {

                }

                if (e.NewValue is IFilter n)
                {

                }
                control.RefreshData();
            }));


        public IFilter Filter2
        {
            get { return (IFilter)GetValue(Filter2Property); }
            set { SetValue(Filter2Property, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Filter2Property =
            DependencyProperty.Register("Filter2", typeof(IFilter), typeof(ItemsControlFilterBehavior), new FrameworkPropertyMetadata(default(IFilter), (d, e) =>
            {
                ItemsControlFilterBehavior control = d as ItemsControlFilterBehavior;

                if (control == null) return;

                if (e.OldValue is IFilter o)
                {

                }

                if (e.NewValue is IFilter n)
                {

                }
                control.RefreshData();
            }));


        public IFilter Filter3
        {
            get { return (IFilter)GetValue(Filter3Property); }
            set { SetValue(Filter3Property, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Filter3Property =
            DependencyProperty.Register("Filter3", typeof(IFilter), typeof(ItemsControlFilterBehavior), new FrameworkPropertyMetadata(default(IFilter), (d, e) =>
            {
                ItemsControlFilterBehavior control = d as ItemsControlFilterBehavior;

                if (control == null) return;

                if (e.OldValue is IFilter o)
                {

                }

                if (e.NewValue is IFilter n)
                {

                }
                control.RefreshData();
            }));


        public IFilter Filter4
        {
            get { return (IFilter)GetValue(Filter4Property); }
            set { SetValue(Filter4Property, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Filter4Property =
            DependencyProperty.Register("Filter4", typeof(IFilter), typeof(ItemsControlFilterBehavior), new FrameworkPropertyMetadata(default(IFilter), (d, e) =>
            {
                ItemsControlFilterBehavior control = d as ItemsControlFilterBehavior;

                if (control == null) return;

                if (e.OldValue is IFilter o)
                {

                }

                if (e.NewValue is IFilter n)
                {

                }
                control.RefreshData();
            }));


        private IEnumerable<IFilter> GetFilters()
        {
            yield return Filter;
            yield return Filter1;
            yield return Filter2;
            yield return Filter3;
            yield return Filter4;
        }

        private void RefreshData()
        {
            if (this.ItemsSource == null)
                return;
            if (this.AssociatedObject == null)
                return;
            var filters = this.GetFilters().Where(x => x != null).ToList();
            if (filters.Count == 0)
                this.AssociatedObject.ItemsSource = this.ItemsSource;
            else
                this.AssociatedObject.ItemsSource = this.ItemsSource.OfType<object>().Where(x => filters.All(l => l.IsMatch(x)));
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

}