// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


using Microsoft.Xaml.Behaviors;
using System.Collections;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace H.Controls.FilterBox
{
    public class TextBlockFilterCountBehavior : Behavior<TextBlock>
    {
        public IDisplayFilter DisplayFilter
        {
            get { return (IDisplayFilter)GetValue(DisplayFilterProperty); }
            set { SetValue(DisplayFilterProperty, value); }
        }


        public static readonly DependencyProperty DisplayFilterProperty =
            DependencyProperty.Register("DisplayFilter", typeof(IDisplayFilter), typeof(TextBlockFilterCountBehavior), new FrameworkPropertyMetadata(default(IDisplayFilter), (d, e) =>
            {
                TextBlockFilterCountBehavior control = d as TextBlockFilterCountBehavior;

                if (control == null) return;

                if (e.OldValue is IDisplayFilter o)
                {

                }

                if (e.NewValue is IDisplayFilter n)
                {

                }
                control.RefreshData();
            }));

        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }


        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(TextBlockFilterCountBehavior), new FrameworkPropertyMetadata(default(IEnumerable), (d, e) =>
            {
                TextBlockFilterCountBehavior control = d as TextBlockFilterCountBehavior;
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
            DependencyProperty.Register("Header", typeof(string), typeof(TextBlockFilterCountBehavior), new FrameworkPropertyMetadata("合计：", (d, e) =>
            {
                TextBlockFilterCountBehavior control = d as TextBlockFilterCountBehavior;

                if (control == null) return;

                if (e.OldValue is string o)
                {

                }

                if (e.NewValue is string n)
                {

                }
                control.RefreshData();
            }));

        public string Format
        {
            get { return (string)GetValue(FormatProperty); }
            set { SetValue(FormatProperty, value); }
        }


        public static readonly DependencyProperty FormatProperty =
            DependencyProperty.Register("Format", typeof(string), typeof(TextBlockFilterCountBehavior), new FrameworkPropertyMetadata("[{0}] {1}", (d, e) =>
            {
                TextBlockFilterCountBehavior control = d as TextBlockFilterCountBehavior;

                if (control == null) return;

                if (e.OldValue is string o)
                {

                }

                if (e.NewValue is string n)
                {

                }
                control.RefreshData();
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
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(this.Header);
            System.Collections.Generic.IEnumerable<object> datas = this.ItemsSource.OfType<object>();
            int count = this.DisplayFilter == null ? datas.Count() : datas.Count(x => this.DisplayFilter.IsMatch(x));
            stringBuilder.Append(string.Format(this.Format, count, this.DisplayFilter?.DisplayName));
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
}