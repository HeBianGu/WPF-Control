// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control


using Microsoft.Xaml.Behaviors;
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace H.Controls.FilterBox
{
    public class TextBlockFiltersCountBehavior : Behavior<TextBlock>
    {
        public TextBlockFiltersCountBehavior()
        {
            this.DisplayFilters.CollectionChanged += (l, k) =>
            {
                this.RefreshData();
            };
        }
        public ObservableCollection<IDisplayFilter> DisplayFilters { get; } = new ObservableCollection<IDisplayFilter>();
        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }


        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(TextBlockFiltersCountBehavior), new FrameworkPropertyMetadata(default(IEnumerable), (d, e) =>
            {
                TextBlockFiltersCountBehavior control = d as TextBlockFiltersCountBehavior;
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
            DependencyProperty.Register("Header", typeof(string), typeof(TextBlockFiltersCountBehavior), new FrameworkPropertyMetadata("合计：", (d, e) =>
            {
                TextBlockFiltersCountBehavior control = d as TextBlockFiltersCountBehavior;

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
            DependencyProperty.Register("Format", typeof(string), typeof(TextBlockFiltersCountBehavior), new FrameworkPropertyMetadata("[{0}] {1}", (d, e) =>
            {
                TextBlockFiltersCountBehavior control = d as TextBlockFiltersCountBehavior;

                if (control == null) return;

                if (e.OldValue is string o)
                {

                }

                if (e.NewValue is string n)
                {

                }

            }));


        public ConditionOperate ConditionOperate
        {
            get { return (ConditionOperate)GetValue(ConditionOperateProperty); }
            set { SetValue(ConditionOperateProperty, value); }
        }


        public static readonly DependencyProperty ConditionOperateProperty =
            DependencyProperty.Register("ConditionOperate", typeof(ConditionOperate), typeof(TextBlockFiltersCountBehavior), new FrameworkPropertyMetadata(ConditionOperate.All, (d, e) =>
            {
                TextBlockFiltersCountBehavior control = d as TextBlockFiltersCountBehavior;

                if (control == null) return;

                if (e.OldValue is ConditionOperate o)
                {

                }

                if (e.NewValue is ConditionOperate n)
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
            if (this.DisplayFilters.Count == 0)
                return;
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(this.Header);
            System.Collections.Generic.IEnumerable<object> datas = this.ItemsSource.OfType<object>();
            int count = 0;
            if (this.ConditionOperate == ConditionOperate.All)
                count = datas.Count(x => this.DisplayFilters.All(l => l.IsMatch(x)));
            if (this.ConditionOperate == ConditionOperate.Any)
                count = datas.Count(x => this.DisplayFilters.Any(l => l.IsMatch(x)));
            if (this.ConditionOperate == ConditionOperate.AnyNot)
                count = datas.Count(x => !this.DisplayFilters.All(l => l.IsMatch(x)));
            if (this.ConditionOperate == ConditionOperate.None)
                count = datas.Count(x => !this.DisplayFilters.All(l => l.IsMatch(x)));

            string splitStr = GetConditionOperate(this.ConditionOperate);
            string display = this.DisplayFilters.Select(x => x.DisplayName).Aggregate((x, y) => $"{x} {splitStr} {y}");
            stringBuilder.Append(string.Format(this.Format, count, display));
            this.AssociatedObject.Text = stringBuilder.ToString();
        }

        public string GetConditionOperate(ConditionOperate conditionOperate)
        {
            if (this.ConditionOperate == ConditionOperate.All)
                return "且";
            if (this.ConditionOperate == ConditionOperate.Any)
                return "或";
            if (this.ConditionOperate == ConditionOperate.AnyNot)
                return "或非";
            if (this.ConditionOperate == ConditionOperate.None)
                return "且非";
            throw new NotImplementedException();
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